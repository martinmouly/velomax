using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Logique d'interaction pour voirPage.xaml
    /// </summary>
    public partial class voirPage : Window
    {
        string _sqlvalue;
        string _offvalue;
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        public voirPage(string offValue,string sqlValue)
        {
            InitializeComponent();
            _sqlvalue = sqlValue;
            _offvalue = offValue;
            Titre.Content = "  Vélomax - " + _offvalue;
            Ajouter.Content = "Ajouter " + _offvalue;
            FillGrid();
        }

        private void FillGrid()
        {
            if (_sqlvalue != "histocommande")
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("Select * from " + _sqlvalue, conn);
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
            else
            {
                Ajouter.Visibility = Visibility.Hidden;
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Num_Commande as Num,clientparticulier.Nom,clientparticulier.prenom as Prenom,cliententreprise.Nom_Compagnie as Compagnie,SUM(piece.prix*commande.quantite) as TotalPieces,SUM(modele.prix*commande.quantite) as TotalModele, DATE_FORMAT(commande.Date_Livraison, \"%D %b %Y\") as DateLivraison FROM commande LEFT JOIN clientparticulier ON commande.id_clientparticulier = clientparticulier.id_clientparticulier LEFT JOIN cliententreprise ON commande.id_cliententreprise = cliententreprise.id_cliententreprise LEFT JOIN piece ON commande.id_piece = piece.id_piece LEFT JOIN modele ON commande.id_modele = modele.id_modele group by num_commande order BY Num_Commande" +
                        "" +
                        "", conn);
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
        public void OnDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                
                DataRowView drv = (DataRowView)Afficher.SelectedItem;
                string result = (drv["id_"+_sqlvalue]).ToString();
                if (_sqlvalue == "piece")
                {
                    if (drv["Quantite"].ToString() == "0")
                    {
                        if (MessageBox.Show("Voulez-vous commander la pièce "+ (drv["Num_Produit"]).ToString()+ " à nouveau?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            App.Requete("Delete from " + _sqlvalue + " where id_" + _sqlvalue + "=" + Convert.ToInt32(result));
                            MessageBox.Show("Supression confirmée");
                        }
                        else
                        {
                            App.Requete("update piece set quantite=5 where id_piece=" + Convert.ToInt32(result));
                            MessageBox.Show("Commande effectuée! Le stock a été mis à jour!");
                        }
                    }
                    else
                    {
                        App.Requete("Delete from " + _sqlvalue + " where id_" + _sqlvalue + "=" + Convert.ToInt32(result));
                        MessageBox.Show("Supression confirmée");
                    }
                }
                else
                {
                    App.Requete("Delete from " + _sqlvalue + " where id_" + _sqlvalue + "=" + Convert.ToInt32(result));
                    MessageBox.Show("Supression confirmée");
                }
                
            }
            catch
            {
                MessageBox.Show("Impossible de supprimer","",MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            this.Close();
            var voirPage = new voirPage(_offvalue, _sqlvalue);
            voirPage.Show();

        }
        public void OnAdd(object sender, RoutedEventArgs e)
        {
            addeditPage add = new addeditPage(_offvalue, _sqlvalue,0,this);
            add.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            
        }


    }
}
