using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models;
using Berber_Otomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Berber_Otomasyon.Controllers
{
    [Authorize(Roles = "admin")]
    public class RestConsumeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RestConsumeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<KullaniciApiDto> kullanicilar = new List<KullaniciApiDto>();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:5000/api/RestApi/");
            var jsondata = await response.Content.ReadAsStringAsync();
            kullanicilar = JsonConvert.DeserializeObject<List<KullaniciApiDto>>(jsondata);
            return View(kullanicilar);
        }
    }
}
