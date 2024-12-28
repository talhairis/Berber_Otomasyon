using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models;
using Berber_Otomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Berber_Otomasyon.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RestApiController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RestApiController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public List<KullaniciApiDto> Get()
        {
            var sonuc = from user in _applicationDbContext.Kullanicilar
                            join userRole in _applicationDbContext.UserRoles on user.Id equals userRole.UserId
                            join role in _applicationDbContext.Roles on userRole.RoleId equals role.Id
                            select new KullaniciApiDto
                            {
                                Id = user.Id,
                                Rol = role.Name,
                                KullaniciAdi = user.KullaniciAdi,
                                KullaniciSoyadi = user.KullaniciSoyadi,
                                CalisanUnvan = user.CalisanUnvan,
                                Email = user.Email,
                            };

            var kullanicilar = sonuc.ToList();


            return kullanicilar;
        }
    }
}
