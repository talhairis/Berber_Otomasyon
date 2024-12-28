namespace Berber_Otomasyon.Models.ViewModels
{
    public class CalisanOnayliViewModel
    {
        public int MusteriRandevuId { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriSoyadi { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }
        public DateOnly RandevuTarih { get; set; }
        public int ToplamSure { get; set; }
        public decimal ToplamUcret { get; set; }
        public List<IslemTuru> IslemTurleri { get; set; }
    }
}
