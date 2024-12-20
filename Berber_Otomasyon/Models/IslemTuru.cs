using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Berber_Otomasyon.Models
{
    public class IslemTuru
    {

        [Key]
        public int IslemTuruId { get; set; }

        [Display(Name = "İşlem Türünün İsmi")]
        [Required(ErrorMessage = "Lütfen işlem türünün ismini giriniz!")]
        [StringLength(50, ErrorMessage = "Lütfen işlem türünün ismini 50 karakterden fazla girmeyiniz!")]
        public string Isim { get; set; }

        [Display(Name = "İşlem Türünün Açıklaması")]
        [StringLength(150, ErrorMessage = "Lütfen işlem türünün açıklamasını 150 karakterden fazla girmeyiniz!")]
        public string Aciklama { get; set; }

        [Display(Name = "İşlem Türünün Fiyatı")]
        [Required(ErrorMessage = "Lütfen işlem türünün fiyatını giriniz!")]
        public decimal Fiyat { get; set; }

        [Display(Name = "İşlem Türünün Süresi")]
        [Required(ErrorMessage = "Lütfen işlem türünün süresini giriniz!")]
        [Range(0, 60, ErrorMessage = "Lütfen işlem türünün süresini uygun aralıkta(0-60) giriniz!")]
        public int Sure { get; set; }

        public ICollection<CalisanIslem>? CalisanIslemler { get; set; }

        public ICollection<IslemSepeti>? IslemSepetleri { get; set; }

        [NotMapped] // Veritabanına kaydedilmeyecek
        public bool IsSelected { get; set; }
    }
}
