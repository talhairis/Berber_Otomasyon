using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Berber_Otomasyon.Models;
using Berber_Otomasyon.Models.ViewModels;
using Berber_Otomasyon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Berber_Otomasyon.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Kullanici> _signInManager;

        public AccountController(ApplicationDbContext applicationDbContext, UserManager<Kullanici> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Kullanici> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RegisterM()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterM(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new Kullanici { KullaniciAdi = registerViewModel.KullaniciAdi, KullaniciSoyadi = registerViewModel.KullaniciSoyadi, UserName = registerViewModel.Email, Email = registerViewModel.Email };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    // Kullanıcıya "Müşteri" rolünü ata
                    if (!await _roleManager.RoleExistsAsync("musteri"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("musteri"));
                    }

                    await _userManager.AddToRoleAsync(user, "musteri");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(registerViewModel);
        }

        [Authorize(Roles = "admin, yonetici")]
        [HttpGet]
        public IActionResult RegisterC()
        {
            RegisterObjectModel registerObjectModel = new RegisterObjectModel();
            registerObjectModel.addCalisanViewModel.Randevular = _applicationDbContext.Randevular.ToList();
            registerObjectModel.addCalisanViewModel.IslemTurleri = _applicationDbContext.IslemTurleri.ToList();
            return View(registerObjectModel);
        }

        [Authorize(Roles = "admin, yonetici")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterC(RegisterObjectModel registerObjectModel)
        {
            if (!ModelState.IsValid) { Console.WriteLine("Model başarılı değil"); }

            var user = new Kullanici
            {
                KullaniciAdi = registerObjectModel.registerViewModel.KullaniciAdi,
                KullaniciSoyadi = registerObjectModel.registerViewModel.KullaniciSoyadi,
                CalisanUnvan = registerObjectModel.registerViewModel.CalisanUnvan,
                UserName = registerObjectModel.registerViewModel.Email,
                Email = registerObjectModel.registerViewModel.Email,
            };

            var result = await _userManager.CreateAsync(user, registerObjectModel.registerViewModel.Password);

            if (result.Succeeded)
            {
                // Seçilen İşlem Türlerini Al ve Kaydet
                if (registerObjectModel.selectedArrays.SelectedIslemTurleri != null)
                {
                    foreach (var islemId in registerObjectModel.selectedArrays.SelectedIslemTurleri)
                    {
                        var calisanIslem = new CalisanIslem
                        {
                            CalisanId = user.Id,
                            IslemTuruId = int.Parse(islemId) // string -> int dönüşümü
                        };
                        _applicationDbContext.CalisanIslemler.Add(calisanIslem);
                    }
                }
            }

            if (result.Succeeded)
            {
                // Seçilen İşlem Türlerini Al ve Kaydet
                if (registerObjectModel.selectedArrays.SelectedRandevular != null)
                {
                    foreach (var randevuId in registerObjectModel.selectedArrays.SelectedRandevular)
                    {
                        var calisanRandevu = new CalisanRandevu
                        {
                            CalisanId = user.Id,
                            RandevuId = int.Parse(randevuId) // string -> int dönüşümü
                        };
                        _applicationDbContext.CalisanRandevular.Add(calisanRandevu);
                    }
                }
            }

            if (result.Succeeded)
            {
                // Kullanıcıya "Müşteri" rolünü ata
                if (!await _roleManager.RoleExistsAsync("calisan"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("calisan"));
                }

                await _userManager.AddToRoleAsync(user, "calisan");
                await _applicationDbContext.SaveChangesAsync();

                ViewData["BasariMessage"] = "Kayıt başarılı.";
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);

            return RedirectToAction(nameof(RegisterC));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}
