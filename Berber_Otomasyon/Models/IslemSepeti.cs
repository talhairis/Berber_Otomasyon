using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class IslemSepeti
    {

        [Key]
        public int IslemSepetiId { get; set; }

        [Display(Name = "Toplam Ücret")]
        public decimal ToplamUcret { get; set; }

        [Display(Name = "Toplam Süre")]
        public int ToplamSure { get; set; }

        public int IslemTuruId { get; set; }
        public IslemTuru IslemTuru { get; set; }

        public int MusteriRandevuId { get; set; }
        public MusteriRandevu MusteriRandevu { get; set; }

    }
}
