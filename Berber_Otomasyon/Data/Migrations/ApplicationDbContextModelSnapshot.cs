﻿// <auto-generated />
using System;
using Berber_Otomasyon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Berber_Otomasyon.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Berber_Otomasyon.Models.CalisanIslem", b =>
                {
                    b.Property<int>("CalisanIslemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalisanIslemId"));

                    b.Property<string>("CalisanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IslemTuruId")
                        .HasColumnType("int");

                    b.HasKey("CalisanIslemId");

                    b.HasIndex("CalisanId");

                    b.HasIndex("IslemTuruId");

                    b.ToTable("CalisanIslemler");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.CalisanRandevu", b =>
                {
                    b.Property<int>("CalisanRandevuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalisanRandevuId"));

                    b.Property<string>("CalisanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("OnayliRandevu")
                        .HasColumnType("bit");

                    b.Property<int>("RandevuId")
                        .HasColumnType("int");

                    b.HasKey("CalisanRandevuId");

                    b.HasIndex("CalisanId");

                    b.HasIndex("RandevuId");

                    b.ToTable("CalisanRandevular");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.IslemSepeti", b =>
                {
                    b.Property<int>("IslemSepetiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IslemSepetiId"));

                    b.Property<int>("IslemTuruId")
                        .HasColumnType("int");

                    b.Property<int>("MusteriRandevuId")
                        .HasColumnType("int");

                    b.HasKey("IslemSepetiId");

                    b.HasIndex("IslemTuruId");

                    b.HasIndex("MusteriRandevuId");

                    b.ToTable("IslemSepetleri");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.IslemTuru", b =>
                {
                    b.Property<int>("IslemTuruId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IslemTuruId"));

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Fiyat")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Sure")
                        .HasColumnType("int");

                    b.HasKey("IslemTuruId");

                    b.ToTable("IslemTurleri");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.Kullanici", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("KullaniciSoyadi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("Kullanici");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.MusteriRandevu", b =>
                {
                    b.Property<int>("MusteriRandevuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MusteriRandevuId"));

                    b.Property<int>("CalisanRandevuId")
                        .HasColumnType("int");

                    b.Property<string>("MusteriId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MusteriRandevuId");

                    b.HasIndex("CalisanRandevuId")
                        .IsUnique();

                    b.HasIndex("MusteriId");

                    b.ToTable("MusteriRandevular");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.Randevu", b =>
                {
                    b.Property<int>("RandevuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RandevuId"));

                    b.Property<TimeSpan>("BaslangicSaati")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("BitisSaati")
                        .HasColumnType("time");

                    b.HasKey("RandevuId");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.Calisan", b =>
                {
                    b.HasBaseType("Berber_Otomasyon.Models.Kullanici");

                    b.Property<string>("CalisanUnvan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Calisan");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.Musteri", b =>
                {
                    b.HasBaseType("Berber_Otomasyon.Models.Kullanici");

                    b.HasDiscriminator().HasValue("Musteri");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.CalisanIslem", b =>
                {
                    b.HasOne("Berber_Otomasyon.Models.Calisan", "Calisan")
                        .WithMany("CalisanIslemler")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Berber_Otomasyon.Models.IslemTuru", "IslemTuru")
                        .WithMany("CalisanIslemler")
                        .HasForeignKey("IslemTuruId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("IslemTuru");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.CalisanRandevu", b =>
                {
                    b.HasOne("Berber_Otomasyon.Models.Calisan", "Calisan")
                        .WithMany("CalisanRandevular")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Berber_Otomasyon.Models.Randevu", "Randevu")
                        .WithMany("CalisanRandevular")
                        .HasForeignKey("RandevuId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Randevu");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.IslemSepeti", b =>
                {
                    b.HasOne("Berber_Otomasyon.Models.IslemTuru", "IslemTuru")
                        .WithMany("IslemSepetleri")
                        .HasForeignKey("IslemTuruId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Berber_Otomasyon.Models.MusteriRandevu", "MusteriRandevu")
                        .WithMany("IslemSepetleri")
                        .HasForeignKey("MusteriRandevuId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("IslemTuru");

                    b.Navigation("MusteriRandevu");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.MusteriRandevu", b =>
                {
                    b.HasOne("Berber_Otomasyon.Models.CalisanRandevu", "CalisanRandevu")
                        .WithOne("MusteriRandevu")
                        .HasForeignKey("Berber_Otomasyon.Models.MusteriRandevu", "CalisanRandevuId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Berber_Otomasyon.Models.Musteri", "Musteri")
                        .WithMany("MusteriRandevular")
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CalisanRandevu");

                    b.Navigation("Musteri");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Berber_Otomasyon.Models.Kullanici", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Berber_Otomasyon.Models.Kullanici", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Berber_Otomasyon.Models.Kullanici", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Berber_Otomasyon.Models.Kullanici", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.CalisanRandevu", b =>
                {
                    b.Navigation("MusteriRandevu")
                        .IsRequired();
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.IslemTuru", b =>
                {
                    b.Navigation("CalisanIslemler");

                    b.Navigation("IslemSepetleri");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.MusteriRandevu", b =>
                {
                    b.Navigation("IslemSepetleri");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.Randevu", b =>
                {
                    b.Navigation("CalisanRandevular");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.Calisan", b =>
                {
                    b.Navigation("CalisanIslemler");

                    b.Navigation("CalisanRandevular");
                });

            modelBuilder.Entity("Berber_Otomasyon.Models.Musteri", b =>
                {
                    b.Navigation("MusteriRandevular");
                });
#pragma warning restore 612, 618
        }
    }
}
