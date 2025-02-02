﻿using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace Berber_Otomasyon.Controllers
{
    [Authorize(Roles = "musteri")]
    public class MusteriController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MusteriController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult OnayliRandevular()
        {
            var FoundMusteriId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var calisanlar = (from user in _applicationDbContext.Kullanicilar
                              join userRole in _applicationDbContext.UserRoles
                                  on user.Id equals userRole.UserId
                              join role in _applicationDbContext.Roles
                                  on userRole.RoleId equals role.Id
                              where role.Name == "calisan"
                              select user).ToList();

            var onayliRandevular = (from calisan in calisanlar
                                    join calisanRandevu in _applicationDbContext.CalisanRandevular
                                        on calisan.Id equals calisanRandevu.CalisanId
                                    join musteriRandevu in _applicationDbContext.MusteriRandevular
                                        on calisanRandevu.CalisanRandevuId equals musteriRandevu.CalisanRandevuId
                                    join islemSepeti in _applicationDbContext.IslemSepetleri
                                        on musteriRandevu.MusteriRandevuId equals islemSepeti.MusteriRandevuId
                                    join islemTuru in _applicationDbContext.IslemTurleri
                                        on islemSepeti.IslemTuruId equals islemTuru.IslemTuruId
                                    join randevu in _applicationDbContext.Randevular
                                        on calisanRandevu.RandevuId equals randevu.RandevuId
                                    where musteriRandevu.MusteriId == FoundMusteriId && musteriRandevu.OnayliRandevu
                                    select new
                                    {
                                        CalisanAdi = calisan.KullaniciAdi,
                                        CalisanSoyadi = calisan.KullaniciSoyadi,
                                        BaslangicSaati = randevu.BaslangicSaati,
                                        BitisSaati = randevu.BitisSaati,
                                        RandevuTarih = musteriRandevu.RandevuTarih,
                                        ToplamSure = musteriRandevu.ToplamSure,
                                        ToplamUcret = musteriRandevu.ToplamUcret,
                                        islemTuru = islemTuru,
                                    }).ToList();


            // Sorgu sonucunu MusteriOnayliViewModel türüne dönüştürme
            List<MusteriOnayliViewModel> musteriOnayliListesi = onayliRandevular
                .GroupBy(r => new
                {
                    r.CalisanAdi,
                    r.CalisanSoyadi,
                    r.BaslangicSaati,
                    r.BitisSaati,
                    r.RandevuTarih,
                    r.ToplamSure,
                    r.ToplamUcret
                })
                .Select(group => new MusteriOnayliViewModel
                {
                    CalisanAdi = group.Key.CalisanAdi,
                    CalisanSoyadi = group.Key.CalisanSoyadi,
                    BaslangicSaati = group.Key.BaslangicSaati,
                    BitisSaati = group.Key.BitisSaati,
                    RandevuTarih = group.Key.RandevuTarih,
                    ToplamSure = group.Key.ToplamSure,
                    ToplamUcret = group.Key.ToplamUcret,
                    IslemTurleri = group.Select(g => g.islemTuru).ToList()
                })
                .ToList();


            return View(musteriOnayliListesi);
        }

        public IActionResult OnaysizRandevular()
        {
            var FoundMusteriId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var calisanlar = (from user in _applicationDbContext.Kullanicilar
                              join userRole in _applicationDbContext.UserRoles
                                  on user.Id equals userRole.UserId
                              join role in _applicationDbContext.Roles
                                  on userRole.RoleId equals role.Id
                              where role.Name == "calisan"
                              select user).ToList();

            var onaysizRandevular = (from calisan in calisanlar
                                    join calisanRandevu in _applicationDbContext.CalisanRandevular
                                        on calisan.Id equals calisanRandevu.CalisanId
                                    join musteriRandevu in _applicationDbContext.MusteriRandevular
                                        on calisanRandevu.CalisanRandevuId equals musteriRandevu.CalisanRandevuId
                                    join islemSepeti in _applicationDbContext.IslemSepetleri
                                        on musteriRandevu.MusteriRandevuId equals islemSepeti.MusteriRandevuId
                                    join islemTuru in _applicationDbContext.IslemTurleri
                                        on islemSepeti.IslemTuruId equals islemTuru.IslemTuruId
                                    join randevu in _applicationDbContext.Randevular
                                        on calisanRandevu.RandevuId equals randevu.RandevuId
                                    where musteriRandevu.MusteriId == FoundMusteriId && !musteriRandevu.OnayliRandevu
                                    select new
                                    {
                                        CalisanAdi = calisan.KullaniciAdi,
                                        CalisanSoyadi = calisan.KullaniciSoyadi,
                                        BaslangicSaati = randevu.BaslangicSaati,
                                        BitisSaati = randevu.BitisSaati,
                                        RandevuTarih = musteriRandevu.RandevuTarih,
                                        ToplamSure = musteriRandevu.ToplamSure,
                                        ToplamUcret = musteriRandevu.ToplamUcret,
                                        islemTuru = islemTuru,
                                    }).ToList();


            // Sorgu sonucunu MusteriOnayliViewModel türüne dönüştürme
            List<MusteriOnayliViewModel> musteriOnaysizListesi = onaysizRandevular
                .GroupBy(r => new
                {
                    r.CalisanAdi,
                    r.CalisanSoyadi,
                    r.BaslangicSaati,
                    r.BitisSaati,
                    r.RandevuTarih,
                    r.ToplamSure,
                    r.ToplamUcret
                })
                .Select(group => new MusteriOnayliViewModel
                {
                    CalisanAdi = group.Key.CalisanAdi,
                    CalisanSoyadi = group.Key.CalisanSoyadi,
                    BaslangicSaati = group.Key.BaslangicSaati,
                    BitisSaati = group.Key.BitisSaati,
                    RandevuTarih = group.Key.RandevuTarih,
                    ToplamSure = group.Key.ToplamSure,
                    ToplamUcret = group.Key.ToplamUcret,
                    IslemTurleri = group.Select(g => g.islemTuru).ToList()
                })
                .ToList();


            return View(musteriOnaysizListesi);
        }
    }
}
