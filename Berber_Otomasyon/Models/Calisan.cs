using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
	public class Calisan : IdentityUser
	{

		[Display(Name = "Çalışan Adı")]
		[Required(ErrorMessage = "Lütfen çalışan adını giriniz!")]
		[StringLength(50, ErrorMessage = "Lütfen çalışan adını 50 karakterden fazla girmeyiniz!")]
		private string calisanAd;

		[Display(Name = "Çalışan Soyadı")]
		[Required(ErrorMessage = "Lütfen çalışan soyadını giriniz!")]
		[StringLength(50, ErrorMessage = "Lütfen çalışan soyadını 50 karakterden fazla girmeyiniz!")]
		private string calisanSoyad;

		[Display(Name = "Çalışan Unvanı")]
		[Required(ErrorMessage = "Lütfen çalışan unvanını giriniz!")]
		private string calisanUnvan;

		[Display(Name = "Çalışan E-Maili")]
		[Required(ErrorMessage = "Lütfen çalışan e-maili giriniz!")]
		[EmailAddress(ErrorMessage = "Lütfen e-mail adresini doğru formatta giriniz!")]
		private string calisanMail;

	}
}
