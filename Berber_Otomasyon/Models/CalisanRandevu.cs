using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
	public class CalisanRandevu
	{
		[Key]
		private long calisanaRandevuId;

		public long calisanId { get; set; }
		public Calisan calisan { get; set; }
	}
}
