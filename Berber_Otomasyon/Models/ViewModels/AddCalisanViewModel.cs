using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models.ViewModels
{
    public class AddCalisanViewModel
    {
        [Required]
        public ICollection<IslemTuru>? IslemTurleri { get; set; }

        [Required]
        public ICollection<Randevu>? Randevular { get; set; }

    }
}
