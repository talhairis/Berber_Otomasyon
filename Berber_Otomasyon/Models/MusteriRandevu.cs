using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class MusteriRandevu
    {

        [Key]
        public int musteriRandevuId;

        public int musteriId { get; set; }
        public Musteri musteri { get; set; }

        public int calisanRandevuId { get; set; }
        public CalisanRandevu calisanRandevu { get; set; }

    }
}
