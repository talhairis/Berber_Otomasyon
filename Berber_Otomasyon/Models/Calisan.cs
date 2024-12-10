using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
	public class Calisan
	{
		[Key]
		private long calisanId;

		[Required]
		[MaxLength(30)]
		[Display(Name = "Calışan Adı")]
		private string calisanAd;

		[Required]
		[MaxLength(30)]
		[Display(Name = "Calışan Soyadı")]
		private string calisanSoyad;

		[Required]
		[EmailAddress(ErrorMessage = "Calışanın e-postasını uygun formatta giriniz!")]
		[Display(Name = "Calışan E-postası")]
		private string calisanMail;

		public ICollection<CalisanRandevu>? CalisanRandevulari { get; set; }


	}
}
