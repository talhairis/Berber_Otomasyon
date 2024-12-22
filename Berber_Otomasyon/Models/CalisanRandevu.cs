using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class CalisanRandevu
    {

        [Key]
        public int CalisanRandevuId { get; set; }

        public string CalisanId { get; set; }
        public Kullanici Calisan { get; set; }

        public int RandevuId { get; set; }
        public Randevu Randevu { get; set; }

        public ICollection<MusteriRandevu> MusteriRandevular { get; set; }

    }
}
