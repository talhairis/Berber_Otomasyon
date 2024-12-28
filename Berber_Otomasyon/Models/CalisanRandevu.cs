using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class CalisanRandevu
    {

        [Key]
        public int CalisanRandevuId { get; set; }

        [Display(Name = "Randevu Tarihi")]
        [Required(ErrorMessage = "Lütfen randevu tarihini seçiniz!")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly RandevuTarih { get; set; }

        [Display(Name = "Randevu Onayı")]
        public bool OnayliRandevu { get; set; }

        public string CalisanId { get; set; }
        public Calisan Calisan { get; set; }

        public int RandevuId { get; set; }
        public Randevu Randevu { get; set; }

        public MusteriRandevu MusteriRandevu { get; set; }

    }
}
