﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Berber_Otomasyon.Models
{
	public class Musteri : Kullanici
	{

        public ICollection<MusteriRandevu>? MusteriRandevular { get; set; }

    }
}
