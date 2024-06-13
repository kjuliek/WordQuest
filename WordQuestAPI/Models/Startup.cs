using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WordQuestAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;



namespace WordQuest
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<WordQuestContext>();

            // Ajoutez CORS à la collection de services
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin5500",
                    builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:5500") // Remplacez par l'origine que vous souhaitez autoriser
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Configure Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Gestion des erreurs d'authentification JWT
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
        
            // Ajouter les services de session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
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

            // Activer CORS globalement
            app.UseCors("AllowSpecificOrigin5500");


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

