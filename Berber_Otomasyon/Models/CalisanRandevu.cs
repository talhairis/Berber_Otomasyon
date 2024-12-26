using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class CalisanRandevu
    {

        [Key]
        public int calisanRandevuId;

        public int calisanId { get; set; }
        public Calisan calisan { get; set; }

        public int randevuId { get; set; }
        public Randevu randevu { get; set; }

    }
}
