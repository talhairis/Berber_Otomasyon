using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class CalisanRandevu
    {

        [Key]
        public int CalisanRandevuId { get; set; }

        [Display(Name = "Randevu Onayı")]
        public bool OnayliRandevu { get; set; } = false;

        public string CalisanId { get; set; }
        public Calisan Calisan { get; set; }

        public int RandevuId { get; set; }
        public Randevu Randevu { get; set; }

        public MusteriRandevu MusteriRandevu { get; set; }

    }
}
