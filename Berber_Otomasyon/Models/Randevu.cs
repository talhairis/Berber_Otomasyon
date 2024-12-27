using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class Randevu
    {

        [Key]
        public int RandevuId { get; set; }

        [Display(Name = "Randevu Başlangıç Saati")]
        [Required(ErrorMessage = "Lütfen randevu başlangıç saatini giriniz!")]
        public TimeSpan BaslangicSaati { get; set; }

        [Display(Name = "Randevu Bitiş Saati")]
        [Required(ErrorMessage = "Lütfen randevu bitiş saatini giriniz!")]
        public TimeSpan BitisSaati { get; set; }

        public ICollection<CalisanRandevu>? CalisanRandevular { get; set; }

    }
}
