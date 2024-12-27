using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Berber_Otomasyon.Models
{
	public class Calisan : Kullanici
	{

		[Display(Name = "Çalışan Unvanı")]
		[Required(ErrorMessage = "Lütfen çalışan unvanını giriniz!")]
        public string CalisanUnvan { get; set; }

        public ICollection<CalisanRandevu>? CalisanRandevular { get; set; }

        public ICollection<CalisanIslem>? CalisanIslemler { get; set; }

    }
}
