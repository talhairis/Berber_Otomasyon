using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models;
using Berber_Otomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuKisit(RandevuKisitModel randevuKisitModel)
        {
            if(randevuKisitModel == null)
            {
                return RedirectToAction(nameof(RandevuKisit));
            }

            var islemTuruIdList = randevuKisitModel.SelectedIslemTurleri.ToList();

            // islemTuruIdList'teki ID'lere göre IslemTurleri tablosunu filtrele
            List<IslemTuru> islemTurleriListesi = _applicationDbContext.IslemTurleri
                .Where(islem => islemTuruIdList.Contains(islem.IslemTuruId))
                .ToList();

            // ToplamSure hesaplama
            int toplamSure = islemTurleriListesi.Sum(islem => islem.Sure);

            // ToplamUcret hesaplama
            decimal toplamUcret = islemTurleriListesi.Sum(islem => islem.Fiyat);

            var calisanlar = _applicationDbContext.CalisanIslemler
                .GroupBy(ci => ci.CalisanId) // Çalışanlara göre gruplandır
                .Where(group => islemTuruIdList.All(islemId => group.Select(ci => ci.IslemTuruId).Contains(islemId))) // Tüm işlem türlerini kontrol et
                .Select(group => group.Key) // ÇalışanId'yi seç
                .Distinct() // Tekrarlayanları kaldır
                .Join(
                    _applicationDbContext.Kullanicilar, // Çalışanları kullanıcı tablosu ile birleştir
                    calisanId => calisanId, // ÇalışanId'yi eşleştir
                    calisan => calisan.Id,
                    (calisanId, calisan) => calisan // Kullanıcı nesnelerini döndür
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

            // Sorguyu oluştur
            var joinedRandevular = filtrelenmisCalisanRandevular
                .Join(
                    _applicationDbContext.Randevular, // Randevular tablosu
                    calisanRandevu => calisanRandevu.RandevuId, // Birleştirme koşulu (CalisanRandevu'daki RandevuId)
                    randevu => randevu.RandevuId, // Randevular tablosundaki RandevuId
                    (calisanRandevu, randevu) => new // Sonuç
                    {
                        CalisanRandevu = calisanRandevu,
                        Randevu = randevu
                    }
                )
                .ToList();

            int secimSayisi = (toplamSure / 60) + 1;

            // Listeyi CalisanRandevularViewModel türüne dönüştür
            List<CalisanRandevularViewModel> joinedRandevularTurn = joinedRandevular
                .Select(j => new CalisanRandevularViewModel
                {
                    calisanRandevu = j.CalisanRandevu,
                    randevu = j.Randevu
                })
                .ToList();

            RandevuAlModel randevuAlModel = new RandevuAlModel
            {
                SecimSayisi = secimSayisi,
                ToplamUcret = toplamUcret,
                ToplamSure = toplamSure,
                IslemTurleri = islemTurleriListesi,
                RandevuTarih = randevuKisitModel.RandevuTarih,
                Calisanlar = calisanlar,
                CalisanRandevularViewModeller = joinedRandevularTurn
            };

            TempData["RandevuAlModel"] = System.Text.Json.JsonSerializer.Serialize(randevuAlModel, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true // Gerekirse düzenli yazım için
            });

            return RedirectToAction(nameof(RandevuIstek));
        }

        public IActionResult RandevuIstek()
        {
            if (TempData["RandevuAlModel"] is string randevuAlModelJson)
            {
                var randevuAlModel = System.Text.Json.JsonSerializer.Deserialize<RandevuAlModel>(
                    randevuAlModelJson,
                    new JsonSerializerOptions
                    {
                        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                    });

                // RandevuBitirModel'in null olmadığından emin olun
                if (randevuAlModel.RandevuBitirModel == null)
                {
                    randevuAlModel.RandevuBitirModel = new RandevuBitirModel
                    {
                        IslemTurleri = new List<IslemTuru>(), // Varsayılan olarak boş listeler oluştur
                        CalisanRandevular = new List<CalisanRandevu>()
                    };
                }

                return View(randevuAlModel);
            }

            return RedirectToAction(nameof(RandevuKisit));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuIstek(RandevuAlModel randevuAlModel)
        {
            var FoundMusteriId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(FoundMusteriId))
            {
                return Unauthorized("Kullanıcı kimlik bilgisi bulunamadı.");
            }

            if (randevuAlModel.RandevuBitirModel.RandevuTarih == null)
            {
                return BadRequest("Randevu tarihi boş olamaz.");
            }

            if (randevuAlModel.RandevuBitirModel.CalisanRandevular == null || !randevuAlModel.RandevuBitirModel.CalisanRandevular.Any())
            {                
                return BadRequest("Çalışan randevular listesi boş olamaz.");
            }

            /*string x = "";
            foreach (var item in randevuAlModel.RandevuBitirModel.CalisanRandevular)
            {
                 x += item.CalisanRandevuId;
            }
            return BadRequest(x);*/

            List<MusteriRandevu> musteriRandevular = new List<MusteriRandevu>();

            foreach (var calisanRandevu in randevuAlModel.RandevuBitirModel.CalisanRandevular)
            {
                MusteriRandevu musteriRandevu = new MusteriRandevu
                {
                    RandevuTarih = randevuAlModel.RandevuBitirModel.RandevuTarih,
                    ToplamSure = randevuAlModel.RandevuBitirModel.ToplamSure,
                    ToplamUcret = randevuAlModel.RandevuBitirModel.ToplamUcret,
                    MusteriId = FoundMusteriId,
                    CalisanRandevuId = calisanRandevu.CalisanRandevuId,
                    IslemSepetleri = randevuAlModel.RandevuBitirModel.IslemTurleri?.Select(cr => new IslemSepeti
                    {
                        IslemTuruId = cr.IslemTuruId,
                    }).ToList() ?? new List<IslemSepeti>(),
                };

                musteriRandevular.Add(musteriRandevu);
            }

            _applicationDbContext.MusteriRandevular.AddRange(musteriRandevular);
            _applicationDbContext.SaveChanges();

            TempData["SuccessMessage"] = "Randevu çalışanın onayına iletilmiştir.";
            return RedirectToAction("Index", "Home");

        }

    }
}
