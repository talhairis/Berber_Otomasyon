using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Berber_Otomasyon.Models;
using Berber_Otomasyon.Models.ViewModels;
using Berber_Otomasyon.Data;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult RegisterM()
        {
            return View();
        }

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

        [HttpGet]
        public IActionResult RegisterC(AddCalisanViewModel addCalisanViewModel)
        {
            RegisterObjectModel registerObjectModel = new RegisterObjectModel();
            registerObjectModel.addCalisanViewModel.Randevular = _applicationDbContext.Randevular.ToList();
            registerObjectModel.addCalisanViewModel.IslemTurleri = _applicationDbContext.IslemTurleri.ToList();
            
            return View(registerObjectModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterC(RegisterObjectModel registerObjectModel)
        {
            if (ModelState.IsValid)
            {
                var user = new Kullanici { KullaniciAdi = registerObjectModel.registerViewModel.KullaniciAdi, 
                    KullaniciSoyadi = registerObjectModel.registerViewModel.KullaniciSoyadi, 
                    CalisanUnvan = registerObjectModel.registerViewModel.CalisanUnvan, 
                    UserName = registerObjectModel.registerViewModel.Email,
                    Email = registerObjectModel.registerViewModel.Email,
                    CalisanRandevular = registerObjectModel.addCalisanViewModel.Randevular.Select(cr => new CalisanRandevu
                    {
                        RandevuId = cr.RandevuId
                    }).ToList(),
                    CalisanIslemler = registerObjectModel.addCalisanViewModel.IslemTurleri.Select(ci => new CalisanIslem
                    {
                        IslemTuruId = ci.IslemTuruId
                    }).ToList()
                };
                var result = await _userManager.CreateAsync(user, registerObjectModel.registerViewModel.Password);

                if (result.Succeeded)
                {
                    // Kullanıcıya "Müşteri" rolünü ata
                    if (!await _roleManager.RoleExistsAsync("calisan"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("calisan"));
                    }

                    await _userManager.AddToRoleAsync(user, "calisan");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _applicationDbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(registerObjectModel);
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
