using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour addeditPage.xaml
    /// </summary>
    public partial class addeditPage : Window
    {
        string _offvalue;
        string _sqlvalue;
        int _id;
        voirPage _previousPage;
        public addeditPage(string offValue, string sqlValue, int id, voirPage previousPage)
        {
            InitializeComponent();
            _offvalue = offValue;
            _sqlvalue = sqlValue;
            _id = id;
            _previousPage = previousPage;
            CmdGrid.Visibility = Visibility.Hidden;
            ShowContent(_sqlvalue,_id);
        }

        public void ShowContent(string sqlvalue,int id)
        {
            switch (sqlvalue)
            {
                case "piece":
                    
                    Label1.Content = "Numéro produit";
                    Label2.Content = "Description";
                    Label3.Content = "Prix";
                    Label4.Content = "Quantité";
                    Label5.Content = "Id Fournisseur";
                    Label6.Content = "NumProd Fournisseur";
                    Label7.Content = "Delai";
                    Label8.Content = "Date Intro";
                    Label9.Content = "Date Disc";
                    AdresseGrid.Visibility = Visibility.Hidden;
                    break;
                case "modele":
                    Label1.Content = "Num Modèle";
                    Label2.Content = "Nom";
                    Label3.Content = "Grandeur";
                    Label4.Content = "Prix";
                    Label5.Content = "Ligne Produit";
                    Label6.Content = "Date Intro";
                    Label7.Content = "Date Disc";
                    Label8.Visibility = Visibility.Hidden;
                    Tb8.Visibility = Visibility.Hidden;
                    Label9.Visibility = Visibility.Hidden;
                    Tb9.Visibility = Visibility.Hidden;
                    AdresseGrid.Visibility = Visibility.Hidden;
                    break;
                case "fournisseur":
                    Label1.Content = "Siret";
                    Label2.Content = "Nom";
                    Label3.Content = "Contact";
                    Label4.Content = "Libelle";
                    Label5.Visibility = Visibility.Hidden;
                    Tb5.Visibility = Visibility.Hidden;
                    Label6.Visibility = Visibility.Hidden;
                    Tb6.Visibility = Visibility.Hidden;
                    Label7.Visibility = Visibility.Hidden;
                    Tb7.Visibility = Visibility.Hidden;
                    Label8.Visibility = Visibility.Hidden;
                    Tb8.Visibility = Visibility.Hidden;
                    Label9.Visibility = Visibility.Hidden;
                    Tb9.Visibility = Visibility.Hidden;
 
                    break;
                case "clientparticulier":
                    Label1.Content = "Nom";
                    Label2.Content = "Prenom";
                    Label3.Content = "Telephone";
                    Label4.Content = "Courriel";
                    Label5.Content = "Progr. Fidelio";
                    Label6.Visibility = Visibility.Hidden;
                    Tb6.Visibility = Visibility.Hidden;
                    Label7.Visibility = Visibility.Hidden;
                    Tb7.Visibility = Visibility.Hidden;
                    Label8.Visibility = Visibility.Hidden;
                    Tb8.Visibility = Visibility.Hidden;
                    Label9.Visibility = Visibility.Hidden;
                    Tb9.Visibility = Visibility.Hidden;
                    break;
                case "cliententreprise":
                    Label1.Content = "Nom Compagnie";
                    Label2.Content = "Contact";
                    Label3.Content = "Telephone";
                    Label4.Content = "Courriel";
                    Label5.Content = "Remise";
                    Label6.Visibility = Visibility.Hidden;
                    Tb6.Visibility = Visibility.Hidden;
                    Label7.Visibility = Visibility.Hidden;
                    Tb7.Visibility = Visibility.Hidden;
                    Label8.Visibility = Visibility.Hidden;
                    Tb8.Visibility = Visibility.Hidden;
                    Label9.Visibility = Visibility.Hidden;
                    Tb9.Visibility = Visibility.Hidden;
                    break;
                case "commande":
                    CmdGrid.Visibility = Visibility.Visible;
                    StdGrid.Visibility = Visibility.Hidden;
                    AdresseGrid.Visibility = Visibility.Hidden;
                    break;
            }
        }

        
        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (_sqlvalue)
            {
                
                case "piece":
                    try
                    {
                        App.Requete("insert into piece(Num_Produit,Descrip,Prix,Quantite,Id_Fournisseur,Num_Prod_F,Delai,Date_Intro,Date_Dis)" +
                            " values('" + Tb1.Text + "','" + Tb2.Text + "'," + Convert.ToDouble(Tb3.Text) + "," + Convert.ToInt32(Tb4.Text) + "," +
                            Convert.ToInt32(Tb5.Text) + "," + Convert.ToInt32(Tb6.Text) + "," + Convert.ToInt32(Tb7.Text) + ", "+ Convert.ToInt32(Tb8.Text)+ ","
                            + Convert.ToInt32(Tb9.Text)+"); ");
                        MessageBox.Show("Nouvelle Pièce enregistrée avec succès!");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("ERREUR \n" + ex.ToString()); 
                    }
                    break;
                case "modele":
                    try
                    {
                        App.Requete("insert into modele(id_modele,nom,grandeur,prix,ligne_produit,date_intro,date_dis) values (" +
                            Convert.ToInt32(Tb1.Text) + ",'" +
                            Tb2.Text + "','" +
                            Tb3.Text + "'," +
                            Convert.ToDouble(Tb4.Text) + ",'" +
                            Tb5.Text + "'," +
                            Convert.ToInt32(Tb6.Text)+ ","+
                            Convert.ToInt32(Tb7.Text) + ");");
                        MessageBox.Show("Nouveau Modèle enregistré avec succès!");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("ERREUR \n" + ex.ToString());
                    }

                    break;
                case "fournisseur":
                    try
                    {
                        App.Requete("insert into FOURNISSEUR(siret,nom_fournisseur,contact,libelle,voie,ville,code_postal,province) values (" +
                           Convert.ToInt32(Tb1.Text) + ",'" +
                           Tb2.Text + "','" +
                           Tb3.Text + "'," +
                           Convert.ToDouble(Tb4.Text) + ",'" +
                           Voie.Text + "','" +
                           Ville.Text + "'," +
                           Convert.ToInt32(Code_Postal.Text) + ",'" +
                           Province.Text + "');");
                        MessageBox.Show("Nouveau Fournisseur enregistré avec succès!");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("ERREUR \n" + ex.ToString());
                    }
                    break;
                case "clientparticulier":
                    try
                    {
                        App.Requete("insert into ClientParticulier(Nom,Prenom,Telephone,Courriel,Num_Programme,Voie,Ville,Code_Postal,Province) values('" +
                           Tb1.Text + "','" +
                           Tb2.Text + "','" +
                           Tb3.Text + "','" +
                           Tb4.Text + "'," +
                           Convert.ToInt32(Tb5.Text) + ",'" +
                           Voie.Text + "','" +
                           Ville.Text + "'," +
                           Convert.ToInt32(Code_Postal.Text) + ",'" +
                           Province.Text + "');") ;
                        MessageBox.Show("Nouveau Client Particulier enregistré avec succès!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERREUR \n" + ex.ToString());
                    }
                    break;
                case "cliententreprise":
                    try
                    {
                        App.Requete("insert into ClientEntreprise(Nom_Compagnie,Contact,Telephone,Courriel,Remise,Voie,Ville,Code_Postal,Province) values('" +
                           Tb1.Text + "','" +
                           Tb2.Text + "','" +
                           Tb3.Text + "','" +
                           Tb4.Text + "'," +
                           Convert.ToInt32(Tb5.Text) + ",'" +
                           Voie.Text + "','" +
                           Ville.Text + "'," +
                           Convert.ToInt32(Code_Postal.Text) + ",'" +
                           Province.Text + "');");
                        MessageBox.Show("Nouveau Client Entreprise enregistré avec succès!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERREUR \n" + ex.ToString());
                    }
                    break;
            }
            this.Close();
            _previousPage.Close();
            voirPage newpage = new voirPage(_offvalue, _sqlvalue);
            newpage.Show();
        }

        private void OnCmdClick(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)combobox.SelectedItem;
            string strqte = typeItem.Content.ToString();
            int qte = Convert.ToInt32(strqte);
            if (rbpiece.IsChecked == true)
            {
                int availableQty = Convert.ToInt32(App.RequeteRetour("select quantite from piece where id_piece=" + +
                            Convert.ToInt32(idp.Text)));
                Console.WriteLine(availableQty);
                Console.WriteLine(Convert.ToInt32(qte));
                if (availableQty < qte)
                {
                    MessageBox.Show("Commande Impossible! Pas assez de stock!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (rbclp.IsChecked == true)
                    {
                        try
                        {
                            App.Requete("insert into Commande(Num_Commande,id_piece,quantite,id_clientparticulier) values(" +
                                Convert.ToInt32(numcmd.Text) + "," +
                                Convert.ToInt32(idp.Text) + "," +
                                qte + "," +
                                Convert.ToInt32(idc.Text) + ");");
                            int newQuantity = availableQty - qte;
                            App.Requete("update piece set quantite=" + newQuantity + " where id_piece=" + Convert.ToInt32(numcmd.Text));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERREUR" + ex.ToString());
                        }
                    }
                    if (rbcle.IsChecked == true)
                    {
                        try
                        {
                            App.Requete("insert into Commande(Num_Commande,id_piece,quantite,id_cliententreprise) values(" +
                                Convert.ToInt32(numcmd.Text) + "," +
                                Convert.ToInt32(idp.Text) + "," +
                                qte + "," +
                                Convert.ToInt32(idc.Text) + ");");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERREUR" + ex.ToString());
                        }
                    }
                }

            }
            if (rbmodele.IsChecked == true)
            {
                if (rbclp.IsChecked == true)
                {
                    try
                    {
                        App.Requete("insert into Commande(Num_Commande,id_MODELE,quantite,id_clientparticulier) values(" +
                            Convert.ToInt32(numcmd.Text) + "," +
                            Convert.ToInt32(idp.Text) + "," +
                            qte + "," +
                            Convert.ToInt32(idc.Text) + ");");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERREUR" + ex.ToString());
                    }
                }
                if (rbcle.IsChecked == true)
                {
                    try
                    {
                        App.Requete("insert into Commande(Num_Commande,id_MODELE,quantite,id_cliententreprise) values(" +
                            Convert.ToInt32(numcmd.Text) + "," +
                            Convert.ToInt32(idp.Text) + "," +
                            qte + "," +
                            Convert.ToInt32(idc.Text) + ");");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERREUR" + ex.ToString());
                    }
                }
            }

            this.Close();
            _previousPage.Close();
            voirPage newpage = new voirPage(_offvalue, _sqlvalue);
            newpage.Show();
        }
    }
}
