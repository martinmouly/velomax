using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection _myCon;
        public MainWindow()
        {
            InitializeComponent();
            _myCon=App.OpenConnection();
            string isRupture = (App.RequeteRetour("select count(*) from piece where quantite=0"));
            if (isRupture != "0")
            {
                WarningLogo.Text = "⚠ " + isRupture + " pièce(s) en rupture de stock!";
            }
            

        }

        private void VoirCommande(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)Commande.SelectedItem;
            string value = typeItem.Content.ToString();
            string sqlvalue = "";
            switch (value)
            {
                case "Ajouter/Supprimer Commande":
                    sqlvalue = "commande";
                    var voirPage = new voirPage("Commande", sqlvalue);
                    voirPage.Show();
                    break;
                case "Historique des Commandes":
                    sqlvalue = "histocommande";
                    var voirPage2 = new voirPage("Commande", sqlvalue);
                    voirPage2.Show();
                        break;
                default:
                    break;
            }
            
        }
        private void VoirStock(object sender, RoutedEventArgs e)
        {
            
            ComboBoxItem typeItem = (ComboBoxItem)Stock.SelectedItem;
            string value = typeItem.Content.ToString();
            string sqlvalue = "";
            switch (value)
            {
                case "Modèle":
                    sqlvalue = "modele";
                    var voirModelePage = new voirPage("Modèle", sqlvalue);
                    voirModelePage.Show();
                    break;
                case "Pièce":
                    sqlvalue = "piece";
                    var voirPiecePage = new voirPage("Pièce", sqlvalue);
                    voirPiecePage.Show();
                    break;
                default:
                    break;
            }

        }
        private void VoirCF(object sender, RoutedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)CF.SelectedItem;
            string value = typeItem.Content.ToString();
            string sqlvalue = "";
            switch (value)
            {
                case "Fournisseur":
                    sqlvalue = "fournisseur";
                    var voirFPage = new voirPage(value, sqlvalue);
                    voirFPage.Show();
                    break;
                case "Clientèle Entreprise":
                    sqlvalue = "cliententreprise";
                    var voirCEPage = new voirPage(value, sqlvalue);
                    voirCEPage.Show();
                    break;
                case "Clientèle Particulier":
                    sqlvalue = "clientparticulier";
                    var voirCPPage = new voirPage(value, sqlvalue);
                    voirCPPage.Show();
                    break;
                default:
                    break;
            }

        }


        private void StatsButton(object sender, RoutedEventArgs e)
        {
            string value="";
            if (Qte.IsChecked == true)
            {
                value = "Quantités vendues"; 
            }
            if (Membres.IsChecked == true)
            {
                value = "Liste des membres";
            }
            if (Best_Clients.IsChecked == true)
            {
                value = "Meilleurs clients";
            }
            if (Analyse.IsChecked == true)
            {
                value = "Analyse";
            }
            var statsPage = new StatsPage(value);
            statsPage.Show();

        }
        private void exportXML(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string CmdString = "select * from piece";
                MySqlCommand cmd;
                MySqlDataAdapter sda;
                DataTable dt;
                
                    cmd = new MySqlCommand(CmdString, _myCon);
                    dt = new DataTable("Piece");
                    sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);
                    dt.WriteXml("Piece.xml");
                    Process.Start("Piece.xml");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void exportJSON(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * from Fournisseur", _myCon);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "FournisseurTable");
                string json = JsonConvert.SerializeObject(ds, new Newtonsoft.Json.Formatting());
                System.IO.File.WriteAllText(@"C:\Users\Martin\source\repos\Velomax\Velomax\bin\Debug\fournisseur.json", json);
                Process.Start("fournisseur.json");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            App.CloseConnection();
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }
    }
}
