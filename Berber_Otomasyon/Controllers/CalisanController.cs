using Berber_Otomasyon.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Berber_Otomasyon.Controllers
{
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CalisanController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // GET: IslemTuru
        public async Task<IActionResult> Index()
        {
            var calisanId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var musteriKullanicilar = _applicationDbContext.Users
                .Join(
                    _applicationDbContext.UserRoles, // AspNetUserRoles tablosu
                    user => user.Id,                // Kullanıcı tablosundaki Id
                    userRole => userRole.UserId,    // UserRoles tablosundaki UserId
                    (user, userRole) => new { User = user, UserRole = userRole }
                )
                .Join(
                    _applicationDbContext.Roles,    // AspNetRoles tablosu
                    joined => joined.UserRole.RoleId, // UserRoles tablosundaki RoleId
                    role => role.Id,                 // AspNetRoles tablosundaki Id
                    (joined, role) => new { joined.User, Role = role }
                )
                .Where(result => result.Role.Name == "musteri") // Sadece "Müşteri" rolüne sahip kullanıcılar
                .Select(result => result.User)                 // Sonuç olarak Kullanıcı nesnesini seç
                .ToList();


            var randevular = _applicationDbContext.CalisanRandevular
                .Where(calisanRandevu => calisanRandevu.CalisanId == calisanId) // İlk önce filtrele
                .Join(
                    _applicationDbContext.MusteriRandevular,
                    calisanRandevu => calisanRandevu.CalisanRandevuId,
                    musteriRandevu => musteriRandevu.CalisanRandevuId,
                    (calisanRandevu, musteriRandevu) => new { calisanRandevu, musteriRandevu }
                )
                .Join(
                    musteriKullanicilar,
                    joined => joined.musteriRandevu.MusteriId,
                    musteri => musteri.Id,
                    (joined, musteri) => new { joined.calisanRandevu, joined.musteriRandevu, musteri }
                )
                .ToList();

            return View(randevular);
        }
    }
}
