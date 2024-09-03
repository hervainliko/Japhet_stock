using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Vues.forms
{
    public partial class loradExcel : Form
    {
        public string Nomfichier;
        Modeles.connection connection = new Modeles.connection();
        MySqlCommand cmd;
        public loradExcel()
        {
            InitializeComponent();
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog BrowsD = new OpenFileDialog();
                BrowsD.Multiselect = false;
                BrowsD.Filter = "Excel Files(*.XLS;*XLSX;)|*.XLS;*XLSX;";
                BrowsD.Title = "select a Excel File.";
                BrowsD.FilterIndex = 1;
                BrowsD.RestoreDirectory = true;
                if (BrowsD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Nomfichier = BrowsD.FileName;
                    txtFilePath.Text = BrowsD.FileName;
                    btnsave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCharger_Click(object sender, EventArgs e)
        {
            DataSet dat = default(DataSet);
            dat = new DataSet();
            //declaration  et utilisation de oledbconnection
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;"
                + "Data Source='" + Nomfichier + "';Extended Properties=\"Excel 12.0;\""))
            {
                conn.Open();
                //declaration du dataadapter
                //la requette selectionner pour tous les champs de la feuille une

                string table = txtfeuille.Text;

                using (OleDbDataAdapter Adap = new OleDbDataAdapter("SELECT * FROM [" + table + "$]", conn))
                {
                    Adap.TableMappings.Add("Table", "TestTable");
                    //chargement du dataset
                    Adap.Fill(dat);
                    //on binde les donnes sur le DGV
                    dgvcustomes.DataSource = dat.Tables[0];
                }
                //A la fin on ferme la connexion
                conn.Close();
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            // dgvstudent.Rows.Clear();
            try
            {
                Modeles.connection conn = new Modeles.connection();
                foreach (DataGridViewRow row in dgvcustomes.Rows)
                {
                    if (row.IsNewRow) continue; // Ignorer la nouvelle ligne vierge
                    conn.conndb.Open();
                    // Préparer la commande d'insertion
                    string query = "INSERT INTO customes(noms, genre, Lieu_Naiss, DateNaisse, piece, Numpiece, adresse, fonction, telephone)  VALUES(@noms, @genre, @Lieu_Naiss, @DateNaisse, @piece, @Numpiece, @adresse, @fonction, @telephone)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn.conndb))
                    {
                        // Ajouter les paramètres
                        // cmd.Parameters.AddWithValue("@Codeclient", row.Cells["Codeclient"].Value);
                        cmd.Parameters.AddWithValue("@noms", row.Cells["noms"].Value);
                        cmd.Parameters.AddWithValue("@genre", row.Cells["genre"].Value);
                        cmd.Parameters.AddWithValue("@Lieu_Naiss", row.Cells["Lieu_Naiss"].Value);
                        cmd.Parameters.AddWithValue("@DateNaisse", row.Cells["DateNaisse"].Value);
                        cmd.Parameters.AddWithValue("@piece", row.Cells["piece"].Value);
                        cmd.Parameters.AddWithValue("@Numpiece", row.Cells["Numpiece"].Value);
                        cmd.Parameters.AddWithValue("@adresse", row.Cells["adresse"].Value);
                        cmd.Parameters.AddWithValue("@fonction", row.Cells["fonction"].Value);
                        cmd.Parameters.AddWithValue("@telephone", row.Cells["telephone"].Value);
                        // Exécuter la commande
                        cmd.ExecuteNonQuery();
                        conn.conndb.Close();
                    }
                }

                MessageBox.Show("Données enregistrées avec succès dans la base de données !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
    }
}
