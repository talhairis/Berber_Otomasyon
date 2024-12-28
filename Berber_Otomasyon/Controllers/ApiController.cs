using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Berber_Otomasyon.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ApiController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

    }
}
