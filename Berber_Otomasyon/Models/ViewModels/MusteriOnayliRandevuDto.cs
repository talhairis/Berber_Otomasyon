namespace Berber_Otomasyon.Models.ViewModels
{
    public class MusteriOnayliRandevuDto
    {
        public string CalisanAdi { get; set; }
        public string CalisanSoyadi { get; set; }
        public string BaslangicSaati { get; set; }
        public string BitisSaati { get; set; }
        public DateOnly RandevuTarih { get; set; }
        public int ToplamSure { get; set; }
        public decimal ToplamUcret { get; set; }
        public List<IslemTuruDto> IslemTurleri { get; set; }
    }

    public class IslemTuruDto
    {
        public string Isim { get; set; }
        public int Sure { get; set; }
        public decimal Fiyat { get; set; }
    }

}
