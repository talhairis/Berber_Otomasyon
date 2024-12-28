using Berber_Otomasyon.Data;
using Berber_Otomasyon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Berber_Otomasyon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<Kullanici, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors(policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                await SeedRoles(serviceProvider, builder.Configuration);
                await SeedUsers(serviceProvider);
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        public static async Task SeedRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Rolleri appsettings.json'dan oku
            var roles = configuration.GetSection("Roles").Get<string[]>();

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }

        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Kullanici>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Kullanýcý bilgileri
            var users = new[]
            {
                new { Email = "g221210002@sakarya.edu.tr", KullaniciAdi = "Talha", KullaniciSoyadi = "Ýris", Password = "sau", Role = "admin" },
                new { Email = "g221210076@sakarya.edu.tr", KullaniciAdi = "Cengizhan", KullaniciSoyadi = "Keyfli", Password = "sau", Role = "admin" }
            };

            foreach (var userInfo in users)
            {
                // Kullanýcý var mý kontrol et
                var user = await userManager.FindByEmailAsync(userInfo.Email);
                if (user == null)
                {
                    // Yeni kullanýcý oluþtur
                    user = new Kullanici
                    {
                        UserName = userInfo.Email,
                        Email = userInfo.Email,
                        KullaniciAdi = userInfo.KullaniciAdi,
                        KullaniciSoyadi = userInfo.KullaniciSoyadi
                    };

                    var result = await userManager.CreateAsync(user, userInfo.Password);
                    if (result.Succeeded)
                    {
                        // Role atama
                        if (await roleManager.RoleExistsAsync(userInfo.Role))
                        {
                            await userManager.AddToRoleAsync(user, userInfo.Role);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Kullanýcý oluþturulamadý: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine($"Kullanýcý zaten mevcut: {userInfo.Email}");
                }
            }
        }
    }
}
