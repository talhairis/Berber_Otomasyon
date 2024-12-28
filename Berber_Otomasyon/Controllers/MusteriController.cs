using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Berber_Otomasyon.Controllers
{
    public class MusteriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusteriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Randevu
        public async Task<IActionResult> Index()
        {
            return View(await _context.Musteriler.ToListAsync());
        }

        // GET: Randevu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KullaniciAdi,KullaniciSoyadi,Email,Password,ConfirmPassword")] Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musteri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musteri);
        }
    }
}
