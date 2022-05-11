using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string connexionString= "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=VeloMax;" +
                                         "UID=root;PASSWORD=password";
        public static MySqlConnection maConnexion = new MySqlConnection(connexionString);
        public static MySqlConnection OpenConnection()
        {
            maConnexion.Open();
            return maConnexion;
        }
        public static void CloseConnection()
        {
            maConnexion.Close();
        }
        public static void Requete(string insertString)
        {
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = insertString;
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static string RequeteRetour(string insertString)
        {
            string x;
            MySqlCommand cmd = new MySqlCommand(insertString, maConnexion);
            x = cmd.ExecuteScalar().ToString();
            return x;
        }
        
    }
}
