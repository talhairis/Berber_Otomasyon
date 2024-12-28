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

        public DbSet<Kullanici> Kullanicilar { get; set; }
		public DbSet<Randevu> Randevular { get; set;}
        public DbSet<IslemTuru> IslemTurleri { get; set; }
        public DbSet<CalisanRandevu> CalisanRandevular { get; set; }
        public DbSet<MusteriRandevu> MusteriRandevular { get; set; }
        public DbSet<CalisanIslem> CalisanIslemler { get; set; }
        public DbSet<IslemSepeti> IslemSepetleri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //CalisanRandevu tablosunda RandevuTarih alanı için veri tipi ayarlaması
            modelBuilder.Entity<CalisanRandevu>()
                .Property(cr => cr.RandevuTarih)
                .HasColumnType("date");

            //IslemTuru tablosunda Fiyat alanı için veri tipi ayarlaması
            modelBuilder.Entity<IslemTuru>()
                .Property(i => i.Fiyat)
                .HasColumnType("decimal(18, 2)");

            // CalisanRandevu için yapılandırma
            modelBuilder.Entity<CalisanRandevu>()
                .HasKey(cr => cr.CalisanRandevuId);

            modelBuilder.Entity<CalisanRandevu>()
                .HasOne(cr => cr.Calisan)
                .WithMany(c => c.CalisanRandevular)
                .HasForeignKey(cr => cr.CalisanId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CalisanRandevu>()
                .HasOne(cr => cr.Randevu)
                .WithMany(r => r.CalisanRandevular)
                .HasForeignKey(cr => cr.RandevuId)
                .OnDelete(DeleteBehavior.NoAction);

            // MusteriRandevu için yapılandırma
            modelBuilder.Entity<MusteriRandevu>()
                .HasKey(mr => mr.MusteriRandevuId);

            modelBuilder.Entity<MusteriRandevu>()
                .HasOne(mr => mr.Musteri)
                .WithMany(m => m.MusteriRandevular)
                .HasForeignKey(mr => mr.MusteriId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MusteriRandevu>()
                .HasOne(mr => mr.CalisanRandevu)
                .WithOne(cr => cr.MusteriRandevu)
                .HasForeignKey<MusteriRandevu>(mr => mr.CalisanRandevuId)
                .OnDelete(DeleteBehavior.NoAction);

            // CalisanIslem için yapılandırma
            modelBuilder.Entity<CalisanIslem>()
                .HasKey(ci => ci.CalisanIslemId);

            modelBuilder.Entity<CalisanIslem>()
                .HasOne(ci => ci.Calisan)
                .WithMany(c => c.CalisanIslemler)
                .HasForeignKey(ci => ci.CalisanId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CalisanIslem>()
                .HasOne(ci => ci.IslemTuru)
                .WithMany(i => i.CalisanIslemler)
                .HasForeignKey(ci => ci.IslemTuruId)
                .OnDelete(DeleteBehavior.NoAction);

            // IslemSepeti için yapılandırma
            modelBuilder.Entity<IslemSepeti>()
                .HasKey(iss => iss.IslemSepetiId);

            modelBuilder.Entity<IslemSepeti>()
                .HasOne(iss => iss.IslemTuru)
                .WithMany(i => i.IslemSepetleri)
                .HasForeignKey(iss => iss.IslemTuruId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<IslemSepeti>()
                .HasOne(iss => iss.MusteriRandevu)
                .WithMany(mr => mr.IslemSepetleri)
                .HasForeignKey(iss => iss.MusteriRandevuId)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
