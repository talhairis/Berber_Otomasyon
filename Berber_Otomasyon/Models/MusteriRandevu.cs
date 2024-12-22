using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class MusteriRandevu
    {

        [Key]
        public int MusteriRandevuId { get; set; }

        [Display(Name = "Randevu Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly RandevuTarih { get; set; }

        [Display(Name = "Randevu Onayı")]
        public bool OnayliRandevu { get; set; }

        [Display(Name = "Toplam Ücret")]
        public decimal ToplamUcret { get; set; }

        [Display(Name = "Toplam Süre")]
        public int ToplamSure { get; set; }

        public string MusteriId { get; set; }
        public Kullanici Musteri { get; set; }

        public int CalisanRandevuId { get; set; }
        public CalisanRandevu CalisanRandevu { get; set; }

        public ICollection<IslemSepeti>? IslemSepetleri { get; set; }

    }
}
