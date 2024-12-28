namespace Berber_Otomasyon.Models.ViewModels
{
    public class MusteriOnayliViewModel
    {
        public string CalisanAdi {  get; set; }
        public string CalisanSoyadi { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }
        public DateOnly RandevuTarih { get; set; }
        public int ToplamSure { get; set; }
        public decimal ToplamUcret { get; set; }

        public List<IslemSepeti> IslemSepetleri { get; set; }

    }
}
