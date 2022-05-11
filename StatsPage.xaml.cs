using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Shapes;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour StatsPage.xaml
    /// </summary>
    public partial class StatsPage : Window
    {
        string _value;
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        public StatsPage(string value)
        {
            _value = value;
            InitializeComponent();
            Titre.Content = "  Vélomax - " + _value;
            piece.Visibility = Visibility.Hidden;
            modele.Visibility = Visibility.Hidden;
            clp.Visibility = Visibility.Hidden;
            cle.Visibility = Visibility.Hidden;
            Analyse.Visibility = Visibility.Hidden;
            FillGrid();
        }

        public void FillGrid()
        {
            conn.Open();
            string txt;
            switch (_value)
            {
                case ("Liste des membres"):
                    txt = "Select clientparticulier.id_clientparticulier as IdClient,clientparticulier.Nom as Nom,clientparticulier.prenom as Prénom,fidelio.Num_Programme as Fidélio,clientparticulier.Date_Adhesion as DateAdhésion from clientparticulier natural join fidelio order by fidelio.Num_Programme DESC";
                    break;
                case ("Quantités vendues"):
                    piece.Visibility = Visibility.Visible;
                    modele.Visibility = Visibility.Visible;
                    txt = "SELECT piece.Num_Produit as NumProduit, piece.Descrip as Description, sum(commande.quantite) as TOTAL from piece left join commande on piece.id_piece=commande.id_piece GROUP BY  piece.Num_Produit order by SUM(commande.quantite) DESC;";
                    break;
                case ("Meilleurs clients"):
                    clp.Visibility = Visibility.Visible;
                    cle.Visibility = Visibility.Visible;
                    txt = "select clientparticulier.id_clientparticulier as IdClient, clientparticulier.nom as Nom, clientparticulier.prenom as Prénom, sum(commande.quantite) as TOTAL  from commande natural join clientparticulier group by commande.id_clientparticulier order by sum(commande.quantite) desc;";
                    break;
                case ("Analyse"):
                    Analyse.Visibility = Visibility.Visible;
                    qte1.Content = App.RequeteRetour("select AVG(commande.quantite) from commande where commande.id_piece is not null and commande.id_clientparticulier is not null;");
                    qte2.Content = App.RequeteRetour("select AVG(commande.quantite) from commande where commande.id_modele is not null and commande.id_clientparticulier is not null;");
                    qte3.Content = App.RequeteRetour("select AVG(commande.quantite) from commande where commande.id_piece is not null and commande.id_cliententreprise is not null;");
                    qte4.Content = App.RequeteRetour("select AVG(commande.quantite) from commande where commande.id_modele is not null and commande.id_cliententreprise is not null;");
                    montant1.Content = App.RequeteRetour("select AVG(commande.quantite*piece.prix) as total from commande left join piece on commande.id_piece=piece.id_piece where commande.id_piece is not null;") + "€";
                    montant2.Content = App.RequeteRetour("select AVG(commande.quantite*modele.prix)  from commande left join modele on modele.id_modele=commande.id_modele where commande.id_modele is not null") + "€";
                    txt = "";
                    break;
                default:
                    txt = "";
                    break;
            }
            try
            {
                if (txt != "")
                { 
                    MySqlDataAdapter adp = new MySqlDataAdapter(txt, conn);
                    DataSet ds = new DataSet();
                    adp.Fill(ds, "LoadDataBinding");
                    Afficher.DataContext = ds;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void piece_Checked(object sender, RoutedEventArgs e)
        {
            string txt = "SELECT piece.Num_Produit, piece.Descrip, sum(commande.quantite) as TOTAL from piece left join commande on piece.id_piece=commande.id_piece GROUP BY  piece.Num_Produit order by SUM(commande.quantite) DESC;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(txt, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                Afficher.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void modele_Checked(object sender, RoutedEventArgs e)
        {
            string txt = "SELECT modele.id_modele, modele.Nom,modele.Grandeur,modele.Ligne_Produit,sum(commande.quantite) as TOTAL from modele left join commande on modele.id_modele=commande.id_modele GROUP BY  modele.id_modele order by SUM(commande.quantite) DESC;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(txt, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                Afficher.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clp_checked(object sender, RoutedEventArgs e)
        {
            string  txt = "select clientparticulier.id_clientparticulier as IdClient, clientparticulier.nom as Nom, clientparticulier.prenom as Prénom, sum(commande.quantite) as TOTAL  from commande natural join clientparticulier group by commande.id_clientparticulier order by sum(commande.quantite) desc;";

            try
            {
                MySqlCommand cmd = new MySqlCommand(txt, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                Afficher.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cle_checked(object sender, RoutedEventArgs e)
        {
            string txt = "select cliententreprise.id_cliententreprise as IdClient, cliententreprise.Nom_Compagnie as Compagnie, sum(commande.quantite) as TOTAL from commande natural join cliententreprise group by commande.id_cliententreprise order by sum(commande.quantite) desc;";

            try
            {
                MySqlCommand cmd = new MySqlCommand(txt, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                Afficher.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
