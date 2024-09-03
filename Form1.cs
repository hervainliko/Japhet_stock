using Japhet.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet
{
    public partial class btnExit : Form
    {
        public btnExit()
        {
            InitializeComponent();
            raduis();
        }

        // Arrrangement du cadre 
        private void raduis()
        {
            // Définir les coins arrondis pour le panneau existant
            Services.RoundedPanel.SetRoundedPanel(panel1, 20); // Remplacez "20" par le rayon souhaité
            Services.RoundedPanel.SetRoundedButton(btnConn, 10);
            Services.RoundedPanel.SetRoundedButton(button1, 5);
            Services.RoundedPanel.SetRoundedPict(pictureBox1, 50);
            Services.RoundedPanel.SetRoundedForm(this, 25);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            // Connexion de l'utilisateur à la bdd
            connexion(txtUsername.Text, txtPassword.Text);
        }



        // Connexion 
        private void connexion(string username, string password)
        {
            Modeles.M_Users obj = new Modeles.M_Users();
            obj.connexionUser(username, password);
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                MySqlDataReader dr = Apps.Query.DR;

                if (dr.Read())
                {
                    //MessageBox.Show("Bienvenue, " + username + " !");

                    //Ouverture de la session avec le rôle d'administrateur
                    ManagerSession.SauvegarderSession(dr[0].ToString());
                    ManagerSession.OuvrirSession(dr[0].ToString());
                    Getdate();
                    //Actauliser();


                    // Ouverture de la fenêtre principale
                    Vues.forms.Frm_principal mainForm = new Vues.forms.Frm_principal();
                    mainForm.Show();
                    this.Hide();
                }
                Apps.Query.DR.Close();
            }
            else if (obj.callback["type"] == "failure")
            {
                Services.MsgFRM msg = new Services.MsgFRM();
                msg.getError(obj.callback["message"]);
            }
            else
            {
                MsgFRM msg = new MsgFRM();
                msg.getError(obj.callback["message"]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vues.forms.adim add= new Vues.forms.adim();
            this.Hide();
            add.ShowDialog();
        }


        // Récuperation de la dernière date 
        public string Getdate()
        {
            var dernieredate = "";
            Modeles.M_Dateexercice obj = new Modeles.M_Dateexercice();
            obj.dernireDate();
            if (obj.callback["type"] == "success")
            {
                MySqlDataReader dr = Apps.Query.DR;

                if (dr.Read())
                {
                    DateSession.OuvrirSession(dr[0].ToString());
                    DateSession.SauvegarderSession(dr[0].ToString());
                }
                Apps.Query.DR.Close();
            }
            else if (obj.callback["type"] == "failure")
            {
                MsgFRM msg = new MsgFRM();
                msg.getError(obj.callback["message"]);
            }
            else
            {
                MsgFRM msg = new MsgFRM();
                msg.getError(obj.callback["message"]);
            }
            return dernieredate;

        }

       
    }
}
