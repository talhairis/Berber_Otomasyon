using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
	public class Calisan : Kullanici
	{

		[Display(Name = "Çalışan Unvanı")]
		[Required(ErrorMessage = "Lütfen çalışan unvanını giriniz!")]
        public string calisanUnvan;

	}
}
