using Berber_Otomasyon.Data;
using Microsoft.AspNetCore.Mvc;

namespace Berber_Otomasyon.Controllers
{
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalisanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CalisanEkle()
        {
            return View();
        }
    }
}
