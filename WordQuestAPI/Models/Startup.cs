using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WordQuestAPI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;



namespace WordQuest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Ajoute le support des fichiers statiques
            services.AddControllersWithViews();

            services.AddSwaggerGen();

            // Configure database connection
            var connector = new MySQLConnector();
            services.AddDbContext<WordQuestContext>(options =>
                options.UseMySql(connector.GetConnectionString(), ServerVersion.AutoDetect(connector.GetConnectionString())));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                // Configuration du hachage de mot de passe
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 6;
                options.Password.RequiredUniqueChars = 1;

                // Configuration du salage
                //options.Password.SaltSize = 16; // Taille du salage en octets
                //options.Password.Salt = new byte[] { 0x01, 0x02, 0x03, 0x04 }; // Salage personnalisé (facultatif)
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<WordQuestContext>();


            // Configure Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login";
                    options.LogoutPath = "/account/logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });
            
            // Ajouter les services de session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée d'inactivité avant expiration de la session
                options.Cookie.HttpOnly = true; // Sécurité du cookie
                options.Cookie.IsEssential = true; // Nécessaire pour GDPR
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            // Gestion des erreurs
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Middleware de fichiers statiques
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Middleware de session
            app.UseSession();

            // Middleware de routage
            app.UseRouting();

            // Middleware d'authentification
            app.UseAuthentication();

            // Middleware d'autorisation
            app.UseAuthorization();

            // Gestion des points de terminaison
            app.UseEndpoints(endpoints =>
            {
                // Configuration des points de terminaison MVC
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            /*
            // Active le middleware de fichiers statiques pour servir les fichiers du dossier wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            // Définit une route par défaut pour servir le fichier index.html
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapFallbackToFile("/index.html");
            });
            */
        }
    }
}

