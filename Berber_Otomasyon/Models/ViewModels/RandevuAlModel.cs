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

        public List<IslemTuru> IslemTurleri { get; set; }

        public List<Kullanici> Calisanlar { get; set; }

        public List<CalisanRandevularViewModel> CalisanRandevularViewModeller { get; set; }

        public RandevuBitirModel RandevuBitirModel { get; set; }
    }
}
