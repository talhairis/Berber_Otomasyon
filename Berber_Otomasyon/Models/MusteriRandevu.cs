using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class MusteriRandevu
    {

        [Key]
        public int MusteriRandevuId { get; set; }

        public string MusteriId { get; set; }
        public Musteri Musteri { get; set; }

        public int CalisanRandevuId { get; set; }
        public CalisanRandevu CalisanRandevu { get; set; }

        public ICollection<IslemSepeti>? IslemSepetleri { get; set; }

    }
}
