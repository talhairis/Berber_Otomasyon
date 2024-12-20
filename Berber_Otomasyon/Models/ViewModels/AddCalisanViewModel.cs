using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models.ViewModels
{
    public class AddCalisanViewModel
    {
        public ICollection<IslemTuru> IslemTurleri { get; set; }

        public ICollection<Randevu> Randevular { get; set; }
    }
}
