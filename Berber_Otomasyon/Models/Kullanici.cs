using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class Kullanici : IdentityUser
    {
        [Display(Name = "Kullanıcı Soyadı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adını giriniz!")]
        [StringLength(50, ErrorMessage = "Lütfen kullanıcı adını 50 karakterden fazla girmeyiniz!")]
        public string KullaniciAdi { get; set; }

        [Display(Name = "Kullanıcı Soyadı")]
        [Required(ErrorMessage = "Lütfen kullanıcı soyadını giriniz!")]
        [StringLength(50, ErrorMessage = "Lütfen kullanıcı soyadını 50 karakterden fazla girmeyiniz!")]
        public string KullaniciSoyadi { get; set; }

        [Display(Name = "Çalışan Unvanı")]
        public string? CalisanUnvan { get; set; }

        public ICollection<CalisanRandevu>? CalisanRandevular { get; set; }

        public ICollection<CalisanIslem>? CalisanIslemler { get; set; }

        public ICollection<MusteriRandevu>? MusteriRandevular { get; set; }
    }
}
