using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models;
using Berber_Otomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            // MusteriRandevular ile calisanRandevular'ı birleştir
            var eslesenRandevular = calisanRandevular
                .Join(
                    _applicationDbContext.MusteriRandevular, // İkinci tablo
                    calisanRandevu => calisanRandevu.CalisanRandevuId, // Birleştirme koşulu (örneğin Id üzerinden)
                    musteriRandevu => musteriRandevu.CalisanRandevuId, // MusteriRandevular'da ilgili alan
                    (calisanRandevu, musteriRandevu) => new { calisanRandevu, musteriRandevu.RandevuTarih } // RandevuTarih'i al
                )
                .Where(joined => joined.RandevuTarih == randevuKisitModel.RandevuTarih) // Eşleşen RandevuTarih'e göre filtrele
                .Select(joined => joined.calisanRandevu) // Eşleşen calisanRandevu nesnelerini seç
                .ToList();

            // calisanRandevular listesinden eşleşenleri çıkart
            var filtrelenmisCalisanRandevular = calisanRandevular
                .Where(calisanRandevu => !eslesenRandevular.Contains(calisanRandevu)) // Eşleşenleri hariç tut
                .ToList();

            RandevuAlModel randevuAlModel = new RandevuAlModel
            {
                ToplamUcret = randevuKisitModel.ToplamUcret,
                ToplamSure = randevuKisitModel.ToplamSure,
                IslemTurleri = randevuKisitModel.IslemTurleri,
                RandevuTarih = randevuKisitModel.RandevuTarih,
                Calisanlar = calisanlar,
                CalisanRandevular = filtrelenmisCalisanRandevular
            };

            return RedirectToAction(nameof(RandevuIstek), new { model = randevuAlModel });
        }

        public IActionResult RandevuIstek(RandevuAlModel randevuAlModel)
        {
            return View(randevuAlModel);
        }

        public IActionResult RandevuIstek(RandevuBitirModel randevuBitirModel)
        {
            var FoundMusteriId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(FoundMusteriId))
            {
                return Unauthorized("Kullanıcı kimlik bilgisi bulunamadı.");
            }

            if (randevuBitirModel.RandevuTarih == null)
            {
                return BadRequest("Randevu tarihi boş olamaz.");
            }

            if (randevuBitirModel.CalisanRandevular == null || !randevuBitirModel.CalisanRandevular.Any())
            {
                return BadRequest("Çalışan randevular listesi boş olamaz.");
            }

            List<MusteriRandevu> musteriRandevular = new List<MusteriRandevu>();

            foreach (var calisanRandevu in randevuBitirModel.CalisanRandevular)
            {
                MusteriRandevu musteriRandevu = new MusteriRandevu
                {
                    RandevuTarih = randevuBitirModel.RandevuTarih,
                    MusteriId = FoundMusteriId,
                    CalisanRandevuId = calisanRandevu.CalisanRandevuId,
                    IslemSepetleri = randevuBitirModel.IslemTurleri?.Select(cr => new IslemSepeti
                    {
                        IslemTuruId = cr.IslemTuruId,
                    }).ToList() ?? new List<IslemSepeti>(),
                };

                musteriRandevular.Add(musteriRandevu);
            }

            _applicationDbContext.MusteriRandevular.AddRange(musteriRandevular);
            _applicationDbContext.SaveChanges();

            return View();
        }

    }
}
