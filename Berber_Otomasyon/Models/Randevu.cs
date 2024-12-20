using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Berber_Otomasyon.Models
{
    public class Randevu
    {

        [Key]
        public int RandevuId { get; set; }

        [Display(Name = "Randevu Başlangıç Saati")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Lütfen randevu başlangıç saatini giriniz!")]
        public TimeSpan BaslangicSaati { get; set; }

        [Display(Name = "Randevu Bitiş Saati")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Lütfen randevu bitiş saatini giriniz!")]
        public TimeSpan BitisSaati { get; set; }

        public ICollection<CalisanRandevu>? CalisanRandevular { get; set; }

        [NotMapped] // Veritabanına kaydedilmeyecek
        public bool IsSelected { get; set; }

    }
}
