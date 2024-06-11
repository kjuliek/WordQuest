
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
        MySQLConnector dbConnection = new MySQLConnector();
        try
        {
            // Ouvrir la connexion à la base de données
            dbConnection.OpenConnection();

            // Utiliser la connexion pour effectuer des opérations sur la base de données
            // Par exemple, vous pouvez exécuter des requêtes SQL ici
            // dbConnection.ExecuteQuery("SELECT * FROM table");

            // Fermer la connexion à la base de données une fois que vous avez terminé
            dbConnection.CloseConnection();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
        }

        using (var client = new HttpClient())
        {
            try
            {
                // Spécifier l'URL de votre endpoint
                string url = "https://votre-api.com/api/users";

                // Envoyer une requête GET pour récupérer tous les utilisateurs
                HttpResponseMessage response = await client.GetAsync(url);

                // Vérifier si la requête a réussi (code de statut 200)
                if (response.IsSuccessStatusCode)
                {
                    // Lire le contenu de la réponse (liste des utilisateurs)
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"La requête a échoué avec le code de statut : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }
        }
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<WordQuest.Startup>();
            });
}