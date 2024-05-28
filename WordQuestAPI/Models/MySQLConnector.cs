using System;
using MySql.Data.MySqlClient;

public class MySQLConnector
{
    private MySqlConnection connection;
    private string? server;
    private string? database;
    private string? port;
    private string? user;
    private string? password;

    // Constructeur
    public MySQLConnector()
    {
        server = Environment.GetEnvironmentVariable("DB_SERVER");
        database = Environment.GetEnvironmentVariable("DB_DATABASE");
        port = Environment.GetEnvironmentVariable("DB_PORT");
        user = Environment.GetEnvironmentVariable("DB_USER");
        password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        
        if (server == null) { server = "localhost"; }
        if (database == null) { database = "wordquest"; }
        if (port == null) { port = "3306"; }
        if (user == null) { user = "root"; }
        if (password == null) { password = ""; }

        string connectionString;
        connectionString = $"SERVER={server};USER={user};PORT={port};PASSWORD={password};DATABASE={database};";

        connection = new MySqlConnection(connectionString);
    }

    public string GetConnectionString(){
        return connection.ConnectionString;
    }

    // Ouvrir la connexion à la base de données
    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            Console.WriteLine("Connecté à la base de données MySQL.");
            return true;
        }
        catch (MySqlException ex)
        {
            // Gestion des erreurs de connexion
            switch (ex.Number)
            {
                case 0:
                    Console.WriteLine("Impossible de se connecter au serveur.");
                    break;
                case 1045:
                    Console.WriteLine("Nom d'utilisateur ou mot de passe incorrect, veuillez réessayer.");
                    break;
                default:
                    Console.WriteLine("Erreur de connexion à la base de données : " + ex.Message);
                    break;
            }
            return false;
        }
    }

    // Fermer la connexion à la base de données
    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Erreur lors de la fermeture de la connexion à la base de données : " + ex.Message);
            return false;
        }
    }

    // Exécuter une requête SELECT
    public void Select(string query)
    {
        if (this.OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            // Boucle à travers les données
            while (dataReader.Read())
            {
                // Traitez chaque ligne de données ici
            }

            // Fermer DataReader
            dataReader.Close();

            // Fermer la connexion
            this.CloseConnection();
        }
    }

    // Exécuter une requête INSERT, UPDATE, DELETE
    public void Execute(string query)
    {
        if (this.OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            // Exécuter la commande
            cmd.ExecuteNonQuery();

            // Fermer la connexion
            this.CloseConnection();
        }
    }
}
