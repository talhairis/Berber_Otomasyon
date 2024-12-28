using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Berber_Otomasyon.Models;
using Berber_Otomasyon.Models.ViewModels;

namespace Berber_Otomasyon.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;

        public AccountController(UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var musteri = new Musteri
                {
                    KullaniciAdi = registerViewModel.KullaniciAdi,
                    KullaniciSoyadi = registerViewModel.KullaniciSoyadi,
                    Email = registerViewModel.Email,
                };

                // Kullanıcıyı oluştur ve şifreyi ata
                var result = await _userManager.CreateAsync(musteri, registerViewModel.Password);

                if (result.Succeeded)
                {
                    // Kullanıcıyı "Müşteri" rolüne ekle
                    var roleResult = await _userManager.AddToRoleAsync(musteri, "musteri");

                    if (!roleResult.Succeeded)
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        // Eğer rol eklenemezse kullanıcıyı silin
                        await _userManager.DeleteAsync(musteri);
                        return View(registerViewModel);
                    }

                    // Kullanıcıyı giriş yaptır (isteğe bağlı)
                    await _signInManager.SignInAsync(musteri, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                // Hataları ekle
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerViewModel);
        }

    }
}
