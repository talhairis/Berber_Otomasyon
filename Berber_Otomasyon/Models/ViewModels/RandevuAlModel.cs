using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models.ViewModels
{
    public class RandevuAlModel
    {
        public int SecimSayisi { get; set; }

        [Display(Name = "Toplam Ücret")]
        public decimal ToplamUcret { get; set; }

        [Display(Name = "Toplam Süre")]
        public int ToplamSure { get; set; }

        [Display(Name = "Randevu Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly RandevuTarih { get; set; }

        public ICollection<IslemTuru> IslemTurleri { get; set; }

        public ICollection<Kullanici> Calisanlar { get; set; }

        public ICollection<CalisanRandevularViewModel> CalisanRandevularViewModeller { get; set; }
    }
}
