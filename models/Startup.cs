using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WordQuest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Ajoute le support des fichiers statiques
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Active le middleware de fichiers statiques pour servir les fichiers du dossier wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            // Définit une route par défaut pour servir le fichier index.html
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapFallbackToFile("/index.html");
            });
        }
    }
}

