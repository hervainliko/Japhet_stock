using Japhet.Services;
using Japhet.Vues.controlview;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Japhet.Vues.forms
{
    public partial class Frm_principal : Form
    {
        public Frm_principal()
        {
            InitializeComponent();
            Verifysessionstock();
            Verifysessiondate();
            roless();
            RoundedPanel.SetRoundedButton(button1, 10);
            raduis();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pnlParametre.Visible=!pnlParametre.Visible;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            btnExit login = new btnExit();
            Frm_principal frm_Principal = new Frm_principal();
            frm_Principal.Close();
            login.ShowDialog();
        }



        // Vérification de la session 

        private void Verifysessionstock()
        {
            if (sesionVerify() == true)
            {
                lblUser.Text=ManagerSession.GetNameUserConnected(int.Parse(ManagerSession.LireSession()));
                lbldate.Text=DateSession.LireSession();
            }
            else
            {
                // Fermer
                this.Hide();
            }
        }


        // Vérification de la session 

        public void Verifysessiondate()
        {
            if (sesionVerify() == true)
            {
                lbldate.Text = DateSession.Getdate(DateSession.LireSession());
            }
            else
            {
                // Fermer
                this.Hide();
            }
        }



        // Récupération de roles 
        public static string[] GetRolesForUser(int id)
        {
            Modeles.M_Roles obj = new Modeles.M_Roles();
            obj.reseach(id.ToString());
            if (obj.callback["type"] == "success")
            {
                MySqlDataReader dr = Apps.Query.DR;
                var roles = new List<string>();
                while (dr.Read())
                {
                    roles.Add(dr.GetString(0));
                }
                Apps.Query.DR.Close();
                return roles.ToArray();
            }
            else if (obj.callback["type"] == "failure")
            {
                Services.MsgFRM msg = new Services.MsgFRM();
                msg.getError(obj.callback["message"]);
                return null;
            }
            else
            {
                Services.MsgFRM msg = new Services.MsgFRM();
                msg.getError(obj.callback["message"]);
                return null;
            }

        }


        // Vérication de la sessions 
        private static bool sesionVerify()
        {
            if (ManagerSession.VerifierSessionExistante() == true)
            {
                // ManageSession.OuvrirSession();
                if (ManagerSession.LireSession() != null)
                {
                    foreach (string role in GetRolesForUser(int.Parse(ManagerSession.LireSession())))
                    {
                        MessageBox.Show(role);
                    }
                    return true;

                }
                else
                {
                    MessageBox.Show("L'utlisateur n'a aucun role");
                    return false;
                }

            }
            else
            {
                return false;
            }
        }


        // Roles 

        private bool[] roless()
        {
            // Simulez la récupération des activités depuis une source de données
            var activities = new List<bool>();
            foreach (var role in GetRolesForUser(int.Parse(ManagerSession.LireSession())))
            {

                switch (role)
                {
                    case "caisse":
                        activities.Add(btnGestionUtilisateur.Enabled = true);
                        activities.Add(btnGestionClients.Enabled = true);
                        activities.Add(btnOperations.Enabled = true);
                        activities.Add(btnEpargne.Enabled = true);
                        activities.Add(btnPesages.Enabled = false);
                        activities.Add(btnGestionCaisse.Enabled = true);
                        activities.Add(btnDeconnection.Enabled = true);
                        activities.Add(btnBakup.Enabled = true);
                        activities.Add(btnDeterminerDate.Enabled = true);
                        activities.Add(btnGestionUtilisateur.Enabled=true);
                        activities.Add(button8.Enabled = false);
                        break;
                    case "pesage":
                        activities.Add(btnGestionUtilisateur.Enabled = false);
                        activities.Add(btnGestionClients.Enabled = true);
                        activities.Add(btnOperations.Enabled = false);
                        activities.Add(btnEpargne.Enabled = false);
                        activities.Add(btnPesages.Enabled = true);
                        activities.Add(btnGestionCaisse.Enabled = false);
                        activities.Add(btnDeconnection.Enabled = true);
                        activities.Add(btnBakup.Enabled = false);
                        activities.Add(btnDeterminerDate.Enabled = false);
                        activities.Add(btnGestionUtilisateur.Enabled = false);
                        activities.Add(button8.Enabled = false);
                        break;
                    // Ajoutez d'autres cas pour d'autres rôles si nécessaire
                    default:
                        break;
                }
            }
            return activities.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGestionClients_Click(object sender, EventArgs e)
        {
           
        }

        private void btnOperations_Click(object sender, EventArgs e)
        {
            
                 pnlParametre.Visible = false;
            if (!pnlAccuil.Controls.Contains(controlview.UserOperation.instance))
            {
                pnlAccuil.Controls.Add(controlview.UserOperation.instance);
                controlview.UserOperation.instance.Dock = DockStyle.Fill;
                controlview.UserOperation.instance.loard();
                controlview.UserOperation.instance.loardcmbCaisse();
                //controlview.UserOperation.instance.loardApprov();
                controlview.UserOperation.instance.BringToFront();
            }
            else
            {
                controlview.UserOperation.instance.loard();
                controlview.UserOperation.instance.loardcmbCaisse();
                //controlview.UserOperation.instance.loardApprov();
                controlview.UserOperation.instance.BringToFront();
            }
        }

        private void btnDeterminerDate_Click(object sender, EventArgs e)
        {
            Vues.forms.DateExercice dates = new DateExercice();
            dates.ShowDialog();
        }

        private void btnEpargne_Click(object sender, EventArgs e)
        {
                 pnlParametre.Visible = false;
            if (!pnlAccuil.Controls.Contains(controlview.UserEpargne.instance))
            {
                controlview.UserEpargne.instance.loard();
                pnlAccuil.Controls.Add(controlview.UserEpargne.instance);
                controlview.UserEpargne.instance.Dock = DockStyle.Fill;
                
                controlview.UserEpargne.instance.BringToFront();
            }
            else
            {
                controlview.UserEpargne.instance.loard();
                controlview.UserEpargne.instance.BringToFront();
            }
        }

        private void btnPesages_Click(object sender, EventArgs e)
        {
            
                   pnlParametre.Visible = false;
            if (!pnlAccuil.Controls.Contains(controlview.UserPesage.instance))
            {
                controlview.UserPesage.instance.loard();
                pnlAccuil.Controls.Add(controlview.UserPesage.instance);
                controlview.UserPesage.instance.Dock = DockStyle.Fill;
                controlview.UserPesage.instance.BringToFront();

            }
            else
            {
                controlview.UserPesage.instance.loard();
                controlview.UserPesage.instance.BringToFront();
            }
        }

        private void btnGestionCaisse_Click(object sender, EventArgs e)
        {
            


                   pnlParametre.Visible = false;
            if (!pnlAccuil.Controls.Contains(UserGestionCaisse.instance))
            {
                pnlAccuil.Controls.Add(controlview.UserGestionCaisse.instance);
                controlview.UserGestionCaisse.instance.Dock = DockStyle.Fill;
                controlview.UserGestionCaisse.instance.loardApp();
                controlview.UserGestionCaisse.instance.loardcmbCaisse();
                controlview.UserGestionCaisse.instance.BringToFront();
            }
            else
            {
                controlview.UserGestionCaisse.instance.BringToFront();
            }
        }

        private void btnDeconnection_Click(object sender, EventArgs e)
        {
            btnExit css = new btnExit();
            this.Close();
            css.ShowDialog();
            
        }

        private void btnBakup_Click(object sender, EventArgs e)
        {
          Vues.forms.Frmbackup fback= new Vues.forms.Frmbackup();
            fback.ShowDialog();
        }

        private void btnGestionUtilisateur_Click(object sender, EventArgs e)
        {
            pnlParametre.Visible = false;
            if (!pnlAccuil.Controls.Contains(controlview.gestion_users.instance))
            {
                pnlAccuil.Controls.Add(controlview.gestion_users.instance);
                controlview.gestion_users.instance.Dock = DockStyle.Fill;
                controlview.gestion_users.instance.BringToFront();
            }
            else
            {
                controlview.gestion_users.instance.BringToFront();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Modeles.M_Caisse Caisse = new Modeles.M_Caisse();
            //Caisse.getcaisse();
            //Modeles.M_Approvcaisse app = new Modeles.M_Approvcaisse();
            //app.get();
            //Modeles.M_Customes client = new Modeles.M_Customes();
            //client.get();
            //Modeles.M_Dateexercice dates = new Modeles.M_Dateexercice();
            //dates.get();
            //dates.DescDate(DateSession.LireSession());

            //Modeles.M_Descpese descpese = new Modeles.M_Descpese();
            //descpese.get();
            //Modeles.M_Mouvement_caisse mouve = new Modeles.M_Mouvement_caisse();
            //mouve.getcaisse();
            //Modeles.M_Roles roles = new Modeles.M_Roles();
            //roles.get();
            //Modeles.M_role_attribute attrirole = new Modeles.M_role_attribute();
            //attrirole.get();
            //Modeles.M_Tepargne eparne = new Modeles.M_Tepargne();
            //eparne.get();

            //Modeles.M_Toperation oper = new Modeles.M_Toperation();
            //oper.get("");

            //Modeles.M_Users users = new Modeles.M_Users();
            //users.get();
                btnExit act = new btnExit();
                act.Getdate();
                
                Verifysessiondate();
                
        }

        private void btnGestionClients_MouseDown(object sender, MouseEventArgs e)
        {
            btnGestionClients.ForeColor = Color.White;
        }

        private void btnGestionClients_MouseHover(object sender, EventArgs e)
        {
            btnGestionClients.ForeColor = Color.White;
        }

        private void btnGestionClients_MouseLeave(object sender, EventArgs e)
        {
            btnGestionCaisse.ForeColor = Color.Teal;
        }

        private void btnOperations_MouseDown(object sender, MouseEventArgs e)
        {
            btnOperations.ForeColor = Color.White;
        }

        private void btnOperations_MouseHover(object sender, EventArgs e)
        {
            btnOperations.ForeColor = Color.White;
        }

        private void btnOperations_MouseLeave(object sender, EventArgs e)
        {
            btnOperations.ForeColor = Color.Teal;
        }

        private void btnEpargne_MouseDown(object sender, MouseEventArgs e)
        {
            btnEpargne.ForeColor = Color.White;
        }

        private void btnEpargne_MouseHover(object sender, EventArgs e)
        {
            btnEpargne.ForeColor = Color.White;
        }

        private void btnEpargne_MouseLeave(object sender, EventArgs e)
        {
            btnEpargne.ForeColor = Color.Teal;
        }

        private void btnPesages_MouseDown(object sender, MouseEventArgs e)
        {
            btnPesages.ForeColor = Color.White;
        }

        private void btnPesages_MouseHover(object sender, EventArgs e)
        {
            btnPesages.ForeColor = Color.White;
        }

        private void btnPesages_MouseLeave(object sender, EventArgs e)
        {
            btnPesages.ForeColor = Color.Teal;
        }

        private void btnGestionCaisse_MouseDown(object sender, MouseEventArgs e)
        {
            btnGestionCaisse.ForeColor = Color.White;
        }

        private void btnGestionCaisse_MouseHover(object sender, EventArgs e)
        {
            btnGestionCaisse.ForeColor = Color.White;
        }

        private void btnGestionCaisse_MouseLeave(object sender, EventArgs e)
        {
            btnGestionCaisse.ForeColor = Color.Teal;
        }

        private void raduis()
        {
            // Définir les coins arrondis pour le panneau existant
            //Services.RoundedPanel.SetRoundedPanel(panel1, 20); // Remplacez "20" par le rayon souhaité
            Services.RoundedPanel.SetRoundedButton(btnGestionClients, 10);
            Services.RoundedPanel.SetRoundedButton(btnGestionCaisse, 5);
            Services.RoundedPanel.SetRoundedButton(btnEpargne, 10);
            Services.RoundedPanel.SetRoundedButton(btnPesages, 5);
            Services.RoundedPanel.SetRoundedButton(btnOperations, 5);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnlParametre.Visible = false;
            if (!pnlAccuil.Controls.Contains(controlview.UserGestionClients.instance))
            {
                controlview.UserGestionClients.instance.loard();
                pnlAccuil.Controls.Add(controlview.UserGestionClients.instance);
                controlview.UserGestionClients.instance.Dock = DockStyle.Fill;
               
                controlview.UserGestionClients.instance.BringToFront();
            }
            else
            {
                controlview.UserGestionClients.instance.loard();
                controlview.UserGestionClients.instance.BringToFront();
            }
        }

        private void btnGestionClients_MouseDown_1(object sender, MouseEventArgs e)
        {
            btnGestionClients.ForeColor= Color.White;
        }

        private void btnGestionClients_MouseHover_1(object sender, EventArgs e)
        {
            btnGestionClients.ForeColor = Color.White;
        }

        private void btnGestionClients_MouseLeave_1(object sender, EventArgs e)
        {
            btnGestionClients.ForeColor = Color.Teal;
        }
    }
}
