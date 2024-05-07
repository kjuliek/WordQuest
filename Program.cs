
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main(string[] args)
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
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<WordQuest.Startup>();
            });
}