using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
	public class Musteri : IdentityUser
	{

		[Display(Name = "Müşteri Adı")]
		[Required(ErrorMessage = "Lütfen müşteri adını giriniz!")]
		[StringLength(50, ErrorMessage = "Lütfen müşteri adını 50 karakterden fazla girmeyiniz!")]
		private string musteriAd;

		[Display(Name = "Müşteri Soyadı")]
		[Required(ErrorMessage = "Lütfen müşteri soyadını giriniz!")]
		[StringLength(50, ErrorMessage = "Lütfen müşteri soyadını 50 karakterden fazla girmeyiniz!")]
		private string musteriSoyad;

		[Display(Name = "Müşteri E-Maili")]
		[Required(ErrorMessage = "Lütfen müşteri e-maili giriniz!")]
		[EmailAddress(ErrorMessage = "Lütfen e-mail adresini doğru formatta giriniz!")]
		private string musteriMail;

	}
}
