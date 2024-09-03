using Japhet.Services;
using Japhet.Vues.Reports.Document;
using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Vues.controlview
{
    public partial class UserGestionCaisse : UserControl
    {
        private static UserGestionCaisse userOperation;
        public string idcaisse;
        public static UserGestionCaisse instance
        {
            get
            {
                if (userOperation == null)
                {
                    userOperation = new UserGestionCaisse();
                }
                return userOperation;
            }
        }
        public UserGestionCaisse()
        {
            InitializeComponent();
            loardApp();
            loardDate();
            //loardcmbCaisse();
            loardCaisse();
            txtSolde.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            //txtMontantEntree.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            //txtMontantsortie.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveAppo();
        }
        public void loardDate()
        {

            Modeles.M_Dateexercice obj = new Modeles.M_Dateexercice();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                cmbDate.Items.Clear();
                //on vide la dgv

                cmbDate.DisplayMember = "Value"; // Affiche la propriété "Value" dans le ComboBox
                cmbDate.ValueMember = "Id"; // Stocke la propriété "Id" associée à chaque élément sélectionné
                MySqlDataReader dr = Apps.Query.DR;
                while (dr.Read())
                {

                    cmbDate.Items.Add(new Services.Item { Id = int.Parse(dr[0].ToString()), Value = dr[1].ToString() }); // Ajoute une nouvelle ComboboxItem au ComboBox
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
                Services.MsgFRM msg = new Services.MsgFRM();
                msg.getError(obj.callback["message"]);
            }

        }
        private void saveAppo()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(cmbCaisse.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                double txtMontantEn = double.Parse(txtMontantEnCaisse.Text);
                Item caisse = (Item)cmbCaisse.SelectedItem;
                Item IdDate = (Item)cmbDate.SelectedItem;
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"Montant", txtMontantEntree.Text},
                    {"compteUser", ManagerSession.LireSession()},
                    {"compteCaisse", caisse.ToString()},
                    {"IDdate", IdDate.ToString()}
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Approvisionnement obj = new Controleurs.C_Approvisionnement(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);
                    loardApp();
                    //modifyMontantenCaisse();

                    //CaissePrincipale();

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

        public void loardApp()
        {
            Modeles.M_Approvcaisse obj = new Modeles.M_Approvcaisse();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dgvPesage.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    coumpt++;
                    dgvPesage.Rows.Add(dr[0].ToString(), dr[2].ToString(), dr[3].ToString(), dr[1].ToString());
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
                Services.MsgFRM msg = new Services.MsgFRM();
                msg.getError(obj.callback["message"]);
            }

        }

        private void migrate()
        {
            if (dgvCaisse.Rows.Count > 0)
            {
                txtIdCaisse.Text = dgvCaisse.CurrentRow.Cells[0].Value.ToString();
                txtIntitule.Text = dgvCaisse.CurrentRow.Cells[2].Value.ToString();
                txtSolde.Text = dgvCaisse.CurrentRow.Cells[3].Value.ToString();
            }

        }
        private void migrateAppro()
        {
            if (dgvPesage.Rows.Count > 0)
            {
                idcaisse = dgvPesage.CurrentRow.Cells[0].Value.ToString();
                txtMontantEntree.Text = dgvPesage.CurrentRow.Cells[3].Value.ToString();
            }

        }

        private void saveCaisse()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtIntitule.Text) || string.IsNullOrEmpty(txtSolde.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                      {"intituleCaisse", txtIntitule.Text},
                      {"Montantcaisse", txtSolde.Text}
                };

                //on passe les donnees dans le controllers
                Controleurs.C_Caisse obj = new Controleurs.C_Caisse(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardCaisse();
                    loardcmbCaisse();
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

        private void loardCaisse()
        {
            Modeles.M_Caisse obj = new Modeles.M_Caisse();
            obj.getcaisse();
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dgvCaisse.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    coumpt++;
                    dgvCaisse.Rows.Add(dr[0].ToString(), coumpt, dr[1].ToString(), dr[2].ToString());
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
                Services.MsgFRM msg = new Services.MsgFRM();
                msg.getError(obj.callback["message"]);
            }
        }

        public void loardcmbCaisse()
        {

            Modeles.M_Caisse obj = new Modeles.M_Caisse();
            obj.getcaisse();
            if (obj.callback["type"] == "success")
            {
                cmbCaisse.Items.Clear();
                //on vide la dgv

                cmbCaisse.DisplayMember = "Value"; // Affiche la propriété "Value" dans le ComboBox
                cmbCaisse.ValueMember = "Id"; // Stocke la propriété "Id" associée à chaque élément sélectionné
                MySqlDataReader dr = Apps.Query.DR;
                while (dr.Read())
                {
                    cmbCaisse.Items.Add(new Services.Item { Id = int.Parse(dr[0].ToString()), Value = dr[1].ToString() }); // Ajoute une nouvelle ComboboxItem au ComboBox
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
                Services.MsgFRM msg = new Services.MsgFRM();
                msg.getError(obj.callback["message"]);
            }

        }


        //private void CaissePrincipale()
        //{
        //    double entree;
        //    if (string.IsNullOrEmpty(txtMontantEntree.Text))
        //    {
        //        entree = 0;
        //    }
        //    else
        //    {
        //        entree = double.Parse(txtMontantEntree.Text);
        //    }

        //    double soortie;
        //    //if (string.IsNullOrEmpty(txtMontantsortie.Text))
        //    //{
        //    //    soortie = 0;
        //    //}
        //    //else
        //    //{
        //    //    soortie = double.Parse(txtMontantsortie.Text);
        //    //}
        //    Services.MsgFRM msg = new Services.MsgFRM();
        //    if (string.IsNullOrEmpty(cmbCaisse.Text))
        //    {
        //        msg.getAttention("Erreur, veiller remplir tous les champs ?");
        //    }
        //    else
        //    {


        //        //string MontantSortie = Convert.ToString(soortie);
        //        string MontaEntree = Convert.ToString(entree);

        //        Item idCaisse = (Item)cmbCaisse.SelectedItem;
        //        Dictionary<string, string> fields = new Dictionary<string, string>{
        //              //{"compteCaisse",idCaisse.ToString()},
        //              {"intituleCaisse", txtIntitule.Text},
        //              {"Montantcaisse", MontantSortie},
        //              {"montantsortie", MontaEntree}
        //        };
        //        //on passe les donnees dans le controllers
        //        Controleurs.C_Caisse obj = new Controleurs.C_Caisse(fields);
        //        obj.add(obj);

        //        if (obj.message["type"] == "success")
        //        {
        //            //msg.getInfo(obj.message["message"]);
        //            loardApp();
        //            loardCaisse();
        //        }
        //        else if (obj.message["type"] == "failure")
        //        {
        //            msg.getError(obj.message["message"]);
        //        }
        //        else
        //        {
        //            msg.getError(obj.message["message"]);
        //        }



        //    }

        //}
        //private void modifyMontantenCaisse()
        //{
        //    Services.MsgFRM msg = new Services.MsgFRM();
        //    if (string.IsNullOrEmpty(cmbCaisse.Text) || string.IsNullOrEmpty(txtMontantEnCaisse.Text))
        //    {
        //        msg.getAttention("Erreur, veiller remplir tous les champs ?");
        //    }
        //    else
        //    {
        //        double montantEntre=0, montantEnCaisse, MontaRestant=0;
        //        if (!string.IsNullOrEmpty(txtMontantEntree.Text))
        //        {
        //            montantEntre = double.Parse(txtMontantEntree.Text);
        //        }

        //        montantEnCaisse = double.Parse(txtMontantEnCaisse.Text);
        //        if (montantEntre <= montantEnCaisse)
        //        {
        //            if (!string.IsNullOrEmpty(txtMontantEntree.Text))
        //            {
        //                MontaRestant = montantEnCaisse - montantEntre;
        //            }
        //            if (!string.IsNullOrEmpty(txtMontantsortie.Text))
        //            {
        //                MontaRestant = double.Parse(txtMontantsortie.Text) + montantEnCaisse;
        //            }
        //            Item idCaisse = (Item)cmbCaisse.SelectedItem;
        //            Dictionary<string, string> fields = new Dictionary<string, string>{
        //              {"compteCaisse", idCaisse.ToString()},
        //              {"intituleCaisse", txtIntitule.Text},
        //              {"Montantcaisse", MontaRestant.ToString()}
        //        };


        //            //on passe les donnees dans le controllers
        //            Controleurs.C_Caisse obj = new Controleurs.C_Caisse(fields);
        //            obj.update(obj);

        //            if (obj.message["type"] == "success")
        //            {
        //                //msg.getInfo(obj.message["message"]);
        //                addMouvement();
        //                loardCaisse();

        //            }
        //            else if (obj.message["type"] == "failure")
        //            {
        //                msg.getError(obj.message["message"]);
        //            }
        //            else
        //            {
        //                msg.getError(obj.message["message"]);
        //            }
        //        }
        //        else
        //        {
        //            msg.getAttention("Attention, le montant est supérieur au montant existant en Caisse");
        //        }

        //    }

        //}

        //private void addMouvement()
        //{
        //    string entree;
        //    if (string.IsNullOrEmpty(txtMontantEntree.Text))
        //    {
        //        entree = "0";
        //    }
        //    else
        //    {
        //        entree = txtMontantEntree.Text;
        //    }

        //    string soortie;
        //    if (string.IsNullOrEmpty(txtMontantsortie.Text))
        //    {
        //        soortie = "0";
        //    }
        //    else
        //    {
        //        soortie = txtMontantsortie.Text;
        //    }
        //    Services.MsgFRM msg = new Services.MsgFRM();
        //    if (string.IsNullOrEmpty(cmbCaisse.Text))
        //    {
        //        msg.getAttention("Erreur, veiller remplir tous les champs ?");
        //    }

        //    double MontS = double.Parse(entree);
        //    double txtMontantEn = double.Parse(txtMontantEnCaisse.Text);
        //    if (MontS >= txtMontantEn)
        //    {
        //        msg.getAttention("Erreur, il n'y a pas de montant suiffisant dans la caisse?");
        //    }
        //    else
        //    {
        //        Item caisse = (Item)cmbCaisse.SelectedItem;
        //        Dictionary<string, string> fields = new Dictionary<string, string>{
        //            {"caisse", caisse.ToString()},
        //            {"credit", entree},
        //            {"debut", soortie},
        //            {"dates", DateSession.LireSession()}
        //        };


        //        //on passe les donnees dans le controllers
        //        Controleurs.C_mouvement obj = new Controleurs.C_mouvement(fields);
        //        obj.add(obj);

        //        if (obj.message["type"] == "success")
        //        {
        //            msg.getInfo(obj.message["message"]);
        //            loardApp();
        //            loardCaisse();
        //            //CaissePrincipale();

        //        }
        //        else if (obj.message["type"] == "failure")
        //        {
        //            msg.getError(obj.message["message"]);
        //        }
        //        else
        //        {
        //            msg.getError(obj.message["message"]);
        //        }

        //    }

        //}




        private void cmbCaisse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbCaisse.Text))
            {
                loardMontaCais();
            }
            else
            {
                txtMontantEnCaisse.Text = string.Empty;
            }
        }
        private void dgvPesage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            migrateAppro();
        }

     
        private void caisseSolde()
        {
            Services.MsgFRM msg = new Services.MsgFRM();

            // Configure les formats des DateTimePicker
            dtdebut.Format = DateTimePickerFormat.Custom;
            dtdebut.CustomFormat = "dd-MM-yyyy";

            //dtFin.Format = DateTimePickerFormat.Custom;
            //dtFin.CustomFormat = "dd-MM-yyyy";

            // Vérifiez si les dates sont sélectionnées
            if (string.IsNullOrEmpty(dtdebut.Text))
            {
                msg.getAttention("Erreur, veuillez compléter les dates");
                return; // Assurez-vous de sortir de la méthode en cas d'erreur
            }

            try
            {
                // Créez une connexion
                Modeles.connection con = new Modeles.connection();
                Vues.Reports.Document.RapportCaisse fcli = new Vues.Reports.Document.RapportCaisse();
                Reports.frmReports_design FF = new Reports.frmReports_design();

                // Ouvrez la connexion si elle est fermée
                if (con.conndb.State != ConnectionState.Open)
                {
                    con.conndb.Open();
                }

                // Créez la commande avec la procédure stockée et les paramètres
                using (MySqlCommand cmd = new MySqlCommand("CALL CalculerSoldeCaisse(@dateDebut)", con.conndb))
                {
                    // Ajoutez les paramètres
                    cmd.Parameters.AddWithValue("@dateDebut", dtdebut.Value.ToString("yyyy-MM-dd"));
                    //cmd.Parameters.AddWithValue("@dateFin", dtFin.Value.ToString("yyyy-MM-dd"));

                    // Exécutez la commande
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    DataSet1 ds = new DataSet1();
                    ada.Fill(ds, "CalculerSoldeCaisse1");

                    // Configurez le rapport avec les données récupérées
                    fcli.SetDataSource(ds.Tables["CalculerSoldeCaisse1"]);
                    FF.crystalReportViewer1.ReportSource = fcli;
                    FF.crystalReportViewer1.Refresh();
                    FF.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     private void cmdPrint_Click(object sender, EventArgs e)
        {
            caisseSolde();
           
        }

        private void gpctrl_Enter(object sender, EventArgs e)
        {

        }

        private void UserGestionCaisse_Load(object sender, EventArgs e)
        {
            loardCaisse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveCaisse();
            
        }
        private void delete()
        {
            idcaisse = Microsoft.VisualBasic.Interaction.InputBox("Entrez le numero de l'operation:", "information", "", 100, 100);
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(idcaisse))
            {
                msg.getAttention("Erreur, veiller Choisir le numero à supprimer ?");
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDApprov", idcaisse}
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Approvisionnement obj = new Controleurs.C_Approvisionnement(fields);
                obj.delete(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardApp();
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
        private void button7_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteCaisse();
        }
        // Suppression et modfication
        private void deleteCaisse()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtIdCaisse.Text))
            {
                msg.getAttention("Erreur, veiller Choisir un client à supprimer ?");
            }
            else if (msg.getDialog("Etes-vous sûr de vouloir supprimer ?"))
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"compteCaisse", txtIdCaisse.Text},
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Caisse obj = new Controleurs.C_Caisse(fields);
                obj.delete(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardCaisse();
                    loardcmbCaisse();
                    txtIdCaisse.Text = txtIntitule.Text = txtSolde.Text = string.Empty;
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




        private void modifyUser()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtIdCaisse.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                if (msg.getDialog("Etes-vous sûr de vouloir modifier ?"))
                {
                    string datess = DateSession.LireSession();
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                        {"compteCaisse",txtIdCaisse.Text},
                        {"Montantcaisse", txtSolde.Text}
                    };
                    //on passe les donnees dans le controllers
                    Controleurs.C_Caisse obj = new Controleurs.C_Caisse(fields);
                    obj.update(obj);

                    if (obj.message["type"] == "success")
                    {
                        msg.getInfo(obj.message["message"]);

                        loardCaisse();
                        loardcmbCaisse();
                        txtIdCaisse.Text = txtIntitule.Text = txtSolde.Text = string.Empty;
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

        private void btnModif_Click(object sender, EventArgs e)
        {
            modifyUser();
        }

        private void dgvCaisse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrate();
        }


        public void loardMontaCais()
        {

            Item caisse = (Item)cmbCaisse.SelectedItem;
            Modeles.M_Caisse obj = new Modeles.M_Caisse();
            obj.reseach(caisse.ToString());
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                //dgvPesage.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    txtMontantEnCaisse.Text = dr[0].ToString();
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
                Services.MsgFRM msg = new Services.MsgFRM();
                msg.getError(obj.callback["message"]);
            }

        }
        private void modify()
        {
            
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(idcaisse)|| string.IsNullOrEmpty(cmbCaisse.Text) || string.IsNullOrEmpty(cmbDate.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs?");
            }
            else
            {
               
                Item datS = (Item)cmbDate.SelectedItem;
                Item caisse= (Item)cmbCaisse.SelectedItem;
               
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDApprov", idcaisse.ToString()},
                    {"IDdate", datS.ToString()},
                    {"Montant", txtMontantEntree.Text},
                    {"compteCaisse", caisse.ToString()},
                    {"compteUser", ManagerSession.LireSession()},
                };

                //on passe les donnees dans le controllers
                Controleurs.C_Approvisionnement obj = new Controleurs.C_Approvisionnement(fields);
                obj.update(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);
                    loardApp();
                    //viderLesTxt();
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
        private void button6_Click(object sender, EventArgs e)
        {
            modify();
        }

        private void BtnAct_Click(object sender, EventArgs e)
        {
            loardDate();
            MessageBox.Show("Connexion reussie!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //DateTime dd = Convert.ToDateTime(Microsoft.VisualBasic.Interaction.InputBox("entree la date:","Message","",100,100));

    }
}
