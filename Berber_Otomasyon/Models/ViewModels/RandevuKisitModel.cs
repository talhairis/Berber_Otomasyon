using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models.ViewModels
{
    public class RandevuKisitModel
    {

        [Display(Name = "Toplam Ücret")]
        public decimal ToplamUcret { get; set; }

        [Display(Name = "Toplam Süre")]
        public int ToplamSure { get; set; }

        [Display(Name = "Randevu Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly RandevuTarih { get; set; }

        public int[] SelectedIslemTurleri { get; set; }

        public ICollection<IslemTuru> IslemTurleri { get; set; } = new HashSet<IslemTuru>();

    }
}
