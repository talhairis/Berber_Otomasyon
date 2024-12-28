using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string? KullaniciAdi { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Soyadı")]
        public string? KullaniciSoyadi { get; set; }

        [Display(Name = "Çalışan Unvanı")]
        public string? CalisanUnvan { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Şifre en az {2}, en fazla {1} karakter uzunluğunda olmalı!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrarla")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor!")]
        public string? ConfirmPassword { get; set; }
    }
}
