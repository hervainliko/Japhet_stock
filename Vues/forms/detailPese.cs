using Japhet.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Vues.forms
{
    public partial class detailPese : Form
    {
        public detailPese()
        {
            InitializeComponent();
            // Associe l'événement KeyPress du TextBox à la méthode TextBox_KeyPress
            txtDuexiemeIndicateur.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtNumerateur.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtIndice.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtPourcentage.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtPoids.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtPU.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);

        }

        private void detailPese_Load(object sender, EventArgs e)
        {
            raduis();
            detail(int.Parse(txtNumOperation.Text));
        }



        // Arrrangement du cadre 
        private void raduis()
        {
            // Définir les coins arrondis pour le panneau existant
            //Services.RoundedPanel.SetRoundedPanel(panel1, 20); // Remplacez "20" par le rayon souhaité
           // Services.RoundedPanel.SetRoundedButton(btnConn, 10);
            //Services.RoundedPanel.SetRoundedButton(button1, 5);
            //Services.RoundedPanel.SetRoundedPict(pictureBox1, 50);
            Services.RoundedPanel.SetRoundedForm(this, 25);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // selection detail pesage 
        private void detail(int param)
        {
            Modeles.M_Descpese obj = new Modeles.M_Descpese();
            obj.getdescpese(param);
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dataDespese.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int num = 0;
                while (dr.Read())
                {
                    num++;
                    txtCompte.Text = dr[1].ToString();
                    txtIntitule.Text = dr[2].ToString();
                    txtMention.Text = dr[3].ToString();
                    //0 id
                    //1 Code
                    //2 noms
                    //3 mention
                    //4 reference
                    //5 numerateur
                    //6 indice
                    //7 pourcent
                    //8 pU
                    //9 poids
                    //10 prix total
                    //11 Total Globale
                    dataDespese.Rows.Add(num,dr[0].ToString(), dr[0].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString());
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

        private void btnPaiement_Click(object sender, EventArgs e)
        {
            modifyMention();
        }


        private void modifyMention()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtNumOperation.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                if (msg.getDialog("Etes-vous sûr de vouloir modifier ?"))
                {
                    string mention = "Opération payée";
                    string datess = DateSession.LireSession();
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                        {"NumPese",txtNumOperation.Text},
                        {"mention", mention}
                    };
                    //on passe les donnees dans le controllers
                    Controleurs.C_Pesage obj = new Controleurs.C_Pesage(fields);
                    obj.updateMention(obj);

                    if (obj.message["type"] == "success")
                    {
                        msg.getInfo(obj.message["message"]);
                    }
                    else if (obj.message["type"] == "failure")
                    {
                        msg.getError(obj.message["message"]);
                    }
                    else
                    {
                        msg.getError(obj.message["message"]);
                    }

                }
            }
        }

        private void dataDespese_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrate();
        }

        private void migrate()
        {
            if (dataDespese.Rows.Count > 0)
            {
                viderLesTxt();

                txtId.Text = dataDespese.CurrentRow.Cells[1].Value.ToString();
                txtDuexiemeIndicateur.Text= dataDespese.CurrentRow.Cells[3].Value.ToString();
                txtNumerateur.Text = dataDespese.CurrentRow.Cells[4].Value.ToString();
                txtIndice.Text = dataDespese.CurrentRow.Cells[5].Value.ToString();
                txtPourcentage.Text = dataDespese.CurrentRow.Cells[6].Value.ToString();
                txtPU.Text = dataDespese.CurrentRow.Cells[7].Value.ToString();
                txtPoids.Text = dataDespese.CurrentRow.Cells[8].Value.ToString();
                txtPrixTotal.Text = dataDespese.CurrentRow.Cells[9].Value.ToString();
            }
        }

        public void viderLesTxt()
        {
            txtId.Text = txtDuexiemeIndicateur.Text = txtNumerateur.Text = txtIndice.Text = txtPourcentage.Text = txtPU.Text = txtPoids.Text = txtPrixTotal.Text = string.Empty;
        }

        private void txtNumerateur_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Calcule du poids 
                if (!string.IsNullOrEmpty(txtNumerateur.Text))
                {
                    double numérateur = double.Parse(txtNumerateur.Text);
                    double poids = numérateur / 11.664;
                    double arrondd = Math.Truncate(poids * 100) / 100;
                    txtPoids.Text = arrondd.ToString();
                    txtPoids.ForeColor = Color.White;

                }
                else
                {
                    txtPoids.Text = string.Empty;
                }
            }
            catch (FormatException ex)
            {
                MsgFRM msg = new MsgFRM();
                msg.getError("Erreur: " + ex.Message);
            }
            calculTeneur();
        }

        private void txtIndice_TextChanged(object sender, EventArgs e)
        {
            calculTeneur();
        }

        private void txtPU_TextChanged(object sender, EventArgs e)
        {
            calcult();
        }

        private void txtPoids_TextChanged(object sender, EventArgs e)
        {
            calcult();
        }


        // Calcul du teneur
        private void calculTeneur()
        {
            // Calcule du tenneur
            if (string.IsNullOrEmpty(txtIndice.Text) || string.IsNullOrEmpty(txtNumerateur.Text))
            {
                txtTenneurMatiere.Text = string.Empty;
            }
            else
            {
                try
                {
                    double denominateur = double.Parse(txtIndice.Text);
                    double numerateur = double.Parse(txtNumerateur.Text);
                    double tenner = numerateur / denominateur;
                    double tennerArrondi = Math.Truncate(tenner * 100) / 100;
                    txtTenneurMatiere.Text = tennerArrondi.ToString();
                    txtTenneurMatiere.ForeColor = Color.White;
                }
                catch (FormatException ex)
                {
                    Services.MsgFRM msg = new Services.MsgFRM();
                    msg.getError("Format entré est inconu");
                }
            }
        }

        // Calcul du prix total
        private void calcult()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPoids.Text) || string.IsNullOrEmpty(txtPU.Text))
                {
                    txtPrixTotal.Text = string.Empty;
                }
                else
                {
                    double poids = double.Parse(txtPoids.Text);
                    double pu = double.Parse(txtPU.Text);
                    double totals = Math.Truncate((poids * pu) * 100) / 100;
                    txtPrixTotal.Text = totals.ToString();
                    txtPrixTotal.ForeColor = Color.White;

                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Erreur Format incorrect");
            }
        }

        // Modification d'un pesage
        private void update()
        {

            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtNumerateur.Text) || string.IsNullOrEmpty(txtIndice.Text) || string.IsNullOrEmpty(txtPourcentage.Text) || string.IsNullOrEmpty(txtPU.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IdDesc", txtId.Text},
                    {"numerateur", txtNumerateur.Text},
                    {"indice", txtIndice.Text},
                    {"pourcentage", txtPourcentage.Text},
                    {"PU", txtPU.Text},
                    {"poids", txtPoids.Text},
                    {"total",txtPrixTotal.Text},
                    {"reference",txtDuexiemeIndicateur.Text}
                };

                //on passe les donnees dans le controllers
                Controleurs.C_Descpese obj = new Controleurs.C_Descpese(fields);
                obj.update(obj);


                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);
                    viderLesTxt();
                    detail(int.Parse(txtNumOperation.Text));
                }
                else if (obj.message["type"] == "failure")
                {
                    msg.getError(obj.message["message"]);
                }
                else
                {
                    msg.getError(obj.message["message"]);
                }

            }

        }

        // Modification d'un pesage
        private void delete()
        {

            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtIndice.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IdDesc", txtId.Text}
                };

                //on passe les donnees dans le controllers
                Controleurs.C_Descpese obj = new Controleurs.C_Descpese(fields);
                obj.delete(obj);


                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);
                    viderLesTxt();
                    detail(int.Parse(txtNumOperation.Text));
                }
                else if (obj.message["type"] == "failure")
                {
                    msg.getError(obj.message["message"]);
                }
                else
                {
                    msg.getError(obj.message["message"]);
                }

            }

        }

        private void txtDuexiemeIndicateur_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtDuexiemeIndicateur_KeyPress(object sender, KeyPressEventArgs e)
        {
            //VerificationChamps.TextBox_KeyPress(txtDuexiemeIndicateur.Text, e);
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
            update();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            delete();
        }
    }
}
