using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Vues.forms
{
    public partial class Frmbackup : Form
    {
        public Frmbackup()
        {
            InitializeComponent();
        }
        bool EtatBool;
        Modeles.connection cn = new Modeles.connection();
        string ExportFolder;
        private void btnrestore_Click(object sender, EventArgs e)
        {
            if (EtatBool)
            {
                try
                {
                    btnrestore.Text = "restaurer database";
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup ab = new MySqlBackup(cmd))
                        {
                            cmd.Connection = cn.conndb;
                            ab.ImportFromFile(ExportFolder);
                        }
                    }
                    MessageBox.Show("Restauration effectuer", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStartBackUp.Text = "RESTORE";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Erreur " + Ex.Message, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Désolée on a pas pu se connectez à la base de données; renseigner les paramètres de connection avant de lancer le backup ", "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStartBackUp_Click(object sender, EventArgs e)
        {
            if (EtatBool)
            {
                try
                {
                    btnStartBackUp.Text = "débuter backup";
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup ab = new MySqlBackup(cmd))
                        {
                            cmd.Connection = cn.conndb;
                            ab.ExportToFile(ExportFolder);
                        }
                    }
                    MessageBox.Show("BackUp éffectuer avec succès à l'emplacement " + lblPath.Text, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStartBackUp.Text = "DEBUTER";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Erreur " + Ex.Message, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Désolée on a pas pu se connectez à la base de données; renseigner les paramètres de connection avant de lancer le backup ", "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveDialog_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog folder = new SaveFileDialog())
            {
                folder.Filter = "sql(*.sql)|*.sql";

                if (folder.ShowDialog() == DialogResult.OK)
                {
                    ExportFolder = folder.FileName;
                    lblPath.Text = ExportFolder;

                    if (cn.conndb.State == ConnectionState.Open)
                    {
                        btnStartBackUp.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Désolée on a pas pu se connectez à la base de données; renseigner les paramètres de connection avant de lancer le backup", "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void Frmbackup_Load(object sender, EventArgs e)
        {
            try
            {
                if (cn.conndb.State == ConnectionState.Closed)//si la connection est fermé on l'ouvre sinon on la ferme
                {
                    cn.conndb.Open();
                    EtatBool = true;
                }
                else
                {
                    cn.conndb.Close();
                    EtatBool = false;
                    MessageBox.Show("Désolée on a pas pu se connectez à la base de données; renseigner les paramètres de connection avant de lancer le backup", "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Ex)
            {
                //s'il y a une erreur de connection à la base de données on ne fait rien mais on. Mais l'Etat devient fals
                EtatBool = false;
                MessageBox.Show("Désolée on a pas pu se connectez à la base de données; renseigner les paramètres de connection avant de lancer le backup " + Ex.Message, "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
