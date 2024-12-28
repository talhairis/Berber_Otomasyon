using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models;

namespace Berber_Otomasyon.Controllers
{
    public class IslemTuruController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IslemTuruController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IslemTuru
        public async Task<IActionResult> Index()
        {
            return View(await _context.IslemTurleri.ToListAsync());
        }

        // GET: IslemTuru/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islemTuru = await _context.IslemTurleri
                .FirstOrDefaultAsync(m => m.IslemTuruId == id);
            if (islemTuru == null)
            {
                return NotFound();
            }

            return View(islemTuru);
        }

        // GET: IslemTuru/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IslemTuru/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IslemTuruId,Isim,Aciklama,Fiyat,Sure")] IslemTuru islemTuru)
        {
            if (ModelState.IsValid)
            {
                _context.Add(islemTuru);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(islemTuru);
        }

        // GET: IslemTuru/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islemTuru = await _context.IslemTurleri.FindAsync(id);
            if (islemTuru == null)
            {
                return NotFound();
            }
            return View(islemTuru);
        }

        // POST: IslemTuru/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IslemTuruId,Isim,Aciklama,Fiyat,Sure")] IslemTuru islemTuru)
        {
            if (id != islemTuru.IslemTuruId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(islemTuru);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslemTuruExists(islemTuru.IslemTuruId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(islemTuru);
        }

        // GET: IslemTuru/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islemTuru = await _context.IslemTurleri
                .FirstOrDefaultAsync(m => m.IslemTuruId == id);
            if (islemTuru == null)
            {
                return NotFound();
            }

            return View(islemTuru);
        }

        // POST: IslemTuru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var islemTuru = await _context.IslemTurleri.FindAsync(id);
            if (islemTuru != null)
            {
                _context.IslemTurleri.Remove(islemTuru);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IslemTuruExists(int id)
        {
            return _context.IslemTurleri.Any(e => e.IslemTuruId == id);
        }
    }
}
