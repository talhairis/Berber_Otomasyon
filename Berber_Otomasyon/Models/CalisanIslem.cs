using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
    public class CalisanIslem
    {

        [Key]
        public int CalisanIslemId { get; set; }

        public string CalisanId { get; set; }
        public Calisan Calisan { get; set; }

        public int IslemTuruId { get; set; }
        public IslemTuru IslemTuru { get; set; }

    }
}
