using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models;
using Berber_Otomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Berber_Otomasyon.Controllers
{
    public class RandevuAlController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RandevuAlController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult RandevuKisit()
        {
            RandevuKisitModel randevuKisitModel = new RandevuKisitModel();
            randevuKisitModel.IslemTurleri = _applicationDbContext.IslemTurleri.ToList();

            return View(randevuKisitModel);
        }

        public IActionResult RandevuKisit(RandevuKisitModel randevuKisitModel)
        {
            if(randevuKisitModel == null)
            {
                return RedirectToAction(nameof(RandevuKisit));
            }

            var islemTuruIdList = randevuKisitModel.IslemTurleri.Select(it => it.IslemTuruId).ToList();

            var calisanlar = _applicationDbContext.CalisanIslemler
                .Join(
                    _applicationDbContext.IslemTurleri, // İkinci tablo
                    calisanIslem => calisanIslem.IslemTuruId, // Birleştirme koşulu
                    islemTuru => islemTuru.IslemTuruId,
                    (calisanIslem, islemTuru) => new { calisanIslem.CalisanId, islemTuru.IslemTuruId } // Sonuç
                )
                .Where(x => islemTuruIdList.Contains(x.IslemTuruId)) // Verilen işlem türü ID'lerine göre filtrele
                .Select(x => x.CalisanId) // Yalnızca CalisanId alanını seç
                .Distinct() // Tekrarlayan CalisanId'leri kaldır
                .Join(
                    _applicationDbContext.Kullanicilar, // Calisan tablosunu birleştir
                    calisanId => calisanId, // CalisanId eşleştirme koşulu
                    calisan => calisan.Id,
                    (calisanId, calisan) => calisan // Sonuç olarak Calisan nesnesi
                )
                .ToList();

            var calisanRandevular = calisanlar
                .Join(
                    _applicationDbContext.CalisanRandevular, // İkinci tablo
                    calisan => calisan.Id, // Birleştirme koşulu
                    calisanRandevu => calisanRandevu.CalisanId,
                    (calisan, calisanRandevu) => calisanRandevu // Sonuç olarak sadece CalisanRandevu nesnesi
                )
                .ToList();

            RandevuAlModel randevuAlModel = new RandevuAlModel
            {
                ToplamUcret = randevuKisitModel.ToplamUcret,
                ToplamSure = randevuKisitModel.ToplamSure,
                IslemTurleri = randevuKisitModel.IslemTurleri,
                RandevuTarih = randevuKisitModel.RandevuTarih,
                Calisanlar = calisanlar,
                CalisanRandevular = calisanRandevular
            };

            return RedirectToAction(nameof(RandevuIstek), new { model = randevuAlModel });
        }

        public IActionResult RandevuIstek(RandevuAlModel randevuAlModel)
        {


            return View();
        }
    }
}
