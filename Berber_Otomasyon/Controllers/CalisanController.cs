using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models.ViewModels;
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

        public IActionResult OnayliRandevular()
        {
            var FoundCalisanId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var musteriler = (from user in _applicationDbContext.Kullanicilar
                              join userRole in _applicationDbContext.UserRoles
                                  on user.Id equals userRole.UserId
                              join role in _applicationDbContext.Roles
                                  on userRole.RoleId equals role.Id
                              where role.Name == "musteri"
                              select user).ToList();

            var calisanRandevular = (from musteri in musteriler
                                     join musteriRandevu in _applicationDbContext.MusteriRandevular
                                         on musteri.Id equals musteriRandevu.MusteriId
                                     join calisanRandevu in _applicationDbContext.CalisanRandevular
                                         on musteriRandevu.CalisanRandevuId equals calisanRandevu.CalisanRandevuId
                                     join islemSepeti in _applicationDbContext.IslemSepetleri
                                         on musteriRandevu.MusteriRandevuId equals islemSepeti.MusteriRandevuId
                                     join islemTuru in _applicationDbContext.IslemTurleri
                                         on islemSepeti.IslemTuruId equals islemTuru.IslemTuruId
                                     join randevu in _applicationDbContext.Randevular
                                         on calisanRandevu.RandevuId equals randevu.RandevuId
                                     where calisanRandevu.CalisanId == FoundCalisanId && musteriRandevu.OnayliRandevu
                                     select new
                                     {
                                         MusteriRandevuId = musteriRandevu.MusteriRandevuId,
                                         MusteriAdi = musteri.UserName,
                                         MusteriSoyadi = musteri.Email, // Örnek olarak email alındı
                                         BaslangicSaati = randevu.BaslangicSaati,
                                         BitisSaati = randevu.BitisSaati,
                                         RandevuTarih = musteriRandevu.RandevuTarih,
                                         ToplamSure = musteriRandevu.ToplamSure,
                                         ToplamUcret = musteriRandevu.ToplamUcret,
                                         IslemTuru = islemTuru
                                     }).ToList();

            // Sorgu sonucunu CalisanRandevuViewModel türüne dönüştürme
            List<CalisanOnayliViewModel> calisanRandevuListesi = calisanRandevular
                .GroupBy(r => new
                {
                    r.MusteriRandevuId,
                    r.MusteriAdi,
                    r.MusteriSoyadi,
                    r.BaslangicSaati,
                    r.BitisSaati,
                    r.RandevuTarih,
                    r.ToplamSure,
                    r.ToplamUcret
                })
                .Select(group => new CalisanOnayliViewModel
                {
                    MusteriRandevuId = group.Key.MusteriRandevuId,
                    MusteriAdi = group.Key.MusteriAdi,
                    MusteriSoyadi = group.Key.MusteriSoyadi,
                    BaslangicSaati = group.Key.BaslangicSaati,
                    BitisSaati = group.Key.BitisSaati,
                    RandevuTarih = group.Key.RandevuTarih,
                    ToplamSure = group.Key.ToplamSure,
                    ToplamUcret = group.Key.ToplamUcret,
                    IslemTurleri = group.Select(g => g.IslemTuru).ToList()
                })
                .ToList();

            return View(calisanRandevuListesi);
        }

        public IActionResult OnaysizRandevular()
        {
            var FoundCalisanId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var musteriler = (from user in _applicationDbContext.Kullanicilar
                              join userRole in _applicationDbContext.UserRoles
                                  on user.Id equals userRole.UserId
                              join role in _applicationDbContext.Roles
                                  on userRole.RoleId equals role.Id
                              where role.Name == "musteri"
                              select user).ToList();

            var calisanRandevular = (from musteri in musteriler
                                     join musteriRandevu in _applicationDbContext.MusteriRandevular
                                         on musteri.Id equals musteriRandevu.MusteriId
                                     join calisanRandevu in _applicationDbContext.CalisanRandevular
                                         on musteriRandevu.CalisanRandevuId equals calisanRandevu.CalisanRandevuId
                                     join islemSepeti in _applicationDbContext.IslemSepetleri
                                         on musteriRandevu.MusteriRandevuId equals islemSepeti.MusteriRandevuId
                                     join islemTuru in _applicationDbContext.IslemTurleri
                                         on islemSepeti.IslemTuruId equals islemTuru.IslemTuruId
                                     join randevu in _applicationDbContext.Randevular
                                         on calisanRandevu.RandevuId equals randevu.RandevuId
                                     where calisanRandevu.CalisanId == FoundCalisanId && !musteriRandevu.OnayliRandevu
                                     select new
                                     {
                                         MusteriRandevuId = musteriRandevu.MusteriRandevuId,
                                         MusteriAdi = musteri.UserName,
                                         MusteriSoyadi = musteri.Email, // Örnek olarak email alındı
                                         BaslangicSaati = randevu.BaslangicSaati,
                                         BitisSaati = randevu.BitisSaati,
                                         RandevuTarih = musteriRandevu.RandevuTarih,
                                         ToplamSure = musteriRandevu.ToplamSure,
                                         ToplamUcret = musteriRandevu.ToplamUcret,
                                         IslemTuru = islemTuru
                                     }).ToList();

            // Sorgu sonucunu CalisanRandevuViewModel türüne dönüştürme
            List<CalisanOnayliViewModel> calisanRandevuListesi = calisanRandevular
                .GroupBy(r => new
                {
                    r.MusteriRandevuId,
                    r.MusteriAdi,
                    r.MusteriSoyadi,
                    r.BaslangicSaati,
                    r.BitisSaati,
                    r.RandevuTarih,
                    r.ToplamSure,
                    r.ToplamUcret
                })
                .Select(group => new CalisanOnayliViewModel
                {
                    MusteriRandevuId = group.Key.MusteriRandevuId,
                    MusteriAdi = group.Key.MusteriAdi,
                    MusteriSoyadi = group.Key.MusteriSoyadi,
                    BaslangicSaati = group.Key.BaslangicSaati,
                    BitisSaati = group.Key.BitisSaati,
                    RandevuTarih = group.Key.RandevuTarih,
                    ToplamSure = group.Key.ToplamSure,
                    ToplamUcret = group.Key.ToplamUcret,
                    IslemTurleri = group.Select(g => g.IslemTuru).ToList()
                })
                .ToList();

            return View(calisanRandevuListesi);
        }

        [HttpPost]
        public IActionResult RandevuOnayla(int musteriRandevuId)
        {
            var randevu = _applicationDbContext.MusteriRandevular
                .FirstOrDefault(r => r.MusteriRandevuId == musteriRandevuId);

            if (randevu != null)
            {
                randevu.OnayliRandevu = true;
                _applicationDbContext.SaveChanges();
                TempData["SuccessMessage"] = "Randevu başarıyla onaylandı.";
            }
            else
            {
                TempData["ErrorMessage"] = "Randevu bulunamadı.";
            }

            return RedirectToAction("OnayliRandevular");
        }

    }
}
