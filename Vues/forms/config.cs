using CrystalDecisions.CrystalReports.Engine;
using Japhet.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Japhet.Vues.forms
{
    public partial class config : Form
    {
        public config()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtServeur.Text) || string.IsNullOrEmpty(txtPort.Text) || string.IsNullOrEmpty(txtBase.Text)|| string.IsNullOrEmpty(txtUser.Text)|| string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Remplissez tous les champs");

            }
            else
            {
                string serveur=txtServeur.Text;
                string port=txtPort.Text;
                string bases=txtBase.Text;
                string user=txtUser.Text;
                string password=txtPassword.Text;
                ConfigSessiondb.SauvegarderSession(serveur, port, bases, user, password);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if(test()) MessageBox.Show("Connexion reussie!");
            else MessageBox.Show("Echec de connexion!");
        }

        private bool test()
        {
             string connection_string = "server=" + txtServeur.Text + ";user=" + txtUser.Text + "; password=" + txtPassword.Text + "; database=" + txtBase.Text + "; Max Pool Size=50000; Pooling=True; port=" + txtPort.Text +"";
            try {
            using (MySqlConnection connection = new MySqlConnection(connection_string))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            }catch (Exception exx)
            {
                return false;
            }
        }

        private void config_Load(object sender, EventArgs e)
        {
            affiche();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            btnExit exx = new btnExit();
            this.Hide();
            exx.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigSessiondb.SupprimerSession();
            affiche();
        }


        private void affiche()
        {
            // Vérifiez si une session existe et affichez les informations
            if (ConfigSessiondb.VerifierSessionExistante())
            {
                List<string> sessionData = ConfigSessiondb.LireSession();
                if (sessionData != null && sessionData.Count == 5)
                {
                    txtServeur.Text = sessionData[0];
                    txtPort.Text = sessionData[1];
                    txtBase.Text = sessionData[2];
                    txtUser.Text = sessionData[3];
                    txtPassword.Text = sessionData[4];
                }
            }
            else
            {
                txtServeur.Text = "Aucune session";
                txtPort.Text = "Aucune session";
                txtBase.Text = "Aucune session";
                txtUser.Text = "Aucune session";
                txtPassword.Text = "Aucune session";
            }
        }
    }
}
