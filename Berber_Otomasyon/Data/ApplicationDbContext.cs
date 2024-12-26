using Berber_Otomasyon.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Berber_Otomasyon.Data
{
	public class ApplicationDbContext : IdentityDbContext<Kullanici>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
		public DbSet<Randevu> Randevular { get; set;}
		public DbSet<CalisanRandevu> CalisanRandevular { get; set; }
        public DbSet<MusteriRandevu> MusteriRandevular { get; set; }
    }
}
