using CrystalDecisions.CrystalReports.Engine;
using Japhet.Controleurs;
using Japhet.Modeles;
using Japhet.Services;
using Japhet.Vues.forms;
using Japhet.Vues.Reports.Document;
using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Japhet.Vues.controlview
{
    public partial class UserOperation : UserControl
    {

        private static UserOperation userOperation;

        public static UserOperation instance
        {
            get
            {
                if (userOperation == null)
                {
                    userOperation = new UserOperation();
                }
                return userOperation;
            }
        }
        public string idop;
        public UserOperation()
        {
            InitializeComponent();
            loard();
            loardcmbCaisse();
            loardDate();
            initilDefaultDateDebut();
            //loardApprov();
        }
        // initialiser la date debut par la premiere date de l'annee encours
        private void initilDefaultDateDebut()
        {
            dtdebut.Value= new DateTime(DateTime.Now.Year, 1, 1);
        }
        private void cmbClient_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbClient.Text))
            {
                txtintituleclient.Text = loardNameForClient(int.Parse(cmbClient.Text));
            }
            else
            {
                txtintituleclient.Text = string.Empty;
            }
        }
        private string loardNameForClient(int param)
        {
            string data = null;
            Modeles.M_Customes obj = new Modeles.M_Customes();
            obj.CountCustom(param);
            if (obj.callback["type"] == "success")
            {
                MySqlDataReader dr = Apps.Query.DR;
                while (dr.Read())
                {
                    data = dr[0].ToString();
                    solde(cmbClient.Text);
                    loard();
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
            return data;

        }
        private void migrateCompte()
        {
            if (dgvSearch.Rows.Count > 0)
            {
                visible();
                cmbClient.Text= dgvSearch.CurrentRow.Cells[0].Value.ToString();
                txtintituleclient.Text = dgvSearch.CurrentRow.Cells[1].Value.ToString();
                solde(cmbClient.Text);
                loard();
            }
        }

        private void saearchname(string param)
        {
            Modeles.M_Customes obj = new Modeles.M_Customes();
            obj.reseach(param);
            if (obj.callback["type"] == "success")
            {
                dgvSearch.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    coumpt++;
                    dgvSearch.Rows.Add(dr[0].ToString(), dr[1].ToString());
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
        private string loardIdForClient(string param)
        {
            string data = null;
            txtintituleclient.Text = param;
            Modeles.M_Customes obj = new Modeles.M_Customes();
            obj.CountNameCustom(param);
            if (obj.callback["type"] == "success")
            {
                MySqlDataReader dr = Apps.Query.DR;
                while (dr.Read())
                {
                    data = dr[0].ToString();
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
            return data;

        }

        private void txtintituleclient_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtintituleclient.Text))
            {
                cmbClient.Text = loardIdForClient(txtintituleclient.Text);

            }
            else
            {
                cmbClient.Text = string.Empty;
            }
        }

        
        // Save Method
       
        private void savDepot()
        {
            string retrait;
            if (string.IsNullOrEmpty(txtMontantRetrait.Text))
            {
                retrait = "0";
            }
            else
            {
                retrait = txtMontantRetrait.Text;
            }

            string depot;
            if (string.IsNullOrEmpty(txtMontantdepot.Text))
            {
                depot = "0";
            }
            else
            {
                depot = txtMontantdepot.Text;
            }
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(cmbClient.Text) || string.IsNullOrEmpty(cmbDate.Text) || string.IsNullOrEmpty(cmbCaisse.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                string id = ManagerSession.LireSession();
                //string datS = DateSession.LireSession();
                Item datS = (Item)cmbDate.SelectedItem;
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDdate", datS.ToString()},
                    {"MontantDepot", depot},
                    {"MontantRetrait", retrait},
                    {"Codeclient", cmbClient.Text},
                    {"libelle", txtLibelle.Text},
                    {"IDApprov",cmbCaisse.Text },
                    {"compteUser", ManagerSession.LireSession()}
                };

                //on passe les donnees dans le controllers
                Controleurs.C_Toperation obj = new Controleurs.C_Toperation(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loard();
                    viderOP();
                    solde(cmbClient.Text);
                    invisible();
                   
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
        public void loard()
        {
            Modeles.M_Toperation obj = new Modeles.M_Toperation();
            obj.get(cmbClient.Text);
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dgvOperation.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    coumpt++;
                    dgvOperation.Rows.Add(dr[0].ToString(),dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
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
        //public void loardApprov()
        //{
        //    Modeles.M_Toperation obj = new Modeles.M_Toperation();
        //    string datS = DateSession.LireSession();
        //    string id = ManagerSession.LireSession();
        //    obj.getApprov(ManagerSession.LireSession());
        //    if (obj.callback["type"] == "success")
        //    {
        //        //on vide la dgv
        //        MySqlDataReader dr = Apps.Query.DR;
        //        while (dr.Read())
        //        {
        //            lblappro.Text = dr[3].ToString();
                    
        //        }
        //        Apps.Query.DR.Close();
        //    }
        //    else if (obj.callback["type"] == "failure")
        //    {
        //        Services.MsgFRM msg = new Services.MsgFRM();
        //        msg.getError(obj.callback["message"]);
        //    }
        //    else
        //    {
        //        Services.MsgFRM msg = new Services.MsgFRM();
        //        msg.getError(obj.callback["message"]);
        //    }

        //}
        void invisible()
        {
            cmbClient.Enabled = txtintituleclient.Enabled = txtLibelle.Enabled = txtSolde.Enabled = txtMontantdepot.Enabled = txtMontantRetrait.Enabled  =false;
        }

        void visible()
        {
            cmbClient.Enabled= txtintituleclient.Enabled = txtLibelle.Enabled = txtMontantdepot.Enabled = txtMontantRetrait.Enabled= true;
        }
        

        public void loardcmbCaisse()
        { 
            Modeles.M_Approvcaisse obj = new Modeles.M_Approvcaisse();
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
                    cmbCaisse.Items.Add( dr[0].ToString()); // Ajoute une nouvelle ComboboxItem au ComboBox
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
       
        void solde(String compteclient)
        {
            
            try
            {
                Modeles.connection conn = new Modeles.connection();
                String cmdSelect = "select Codeclient,dateexercice.IDdate,dateexercice.DateExec,sum(MontantDepot) as DEPOT,sum(MontantRetrait) AS RETRAIT,sum((MontantDepot)-(MontantRetrait)) AS SOLDE from toperations,dateexercice where toperations.IDdate = dateexercice.IDdate AND  Codeclient= '"+ compteclient+"' group by Codeclient; ";//selectionner les donnees de la table
                MySqlDataAdapter monAdapteur = new MySqlDataAdapter(cmdSelect, conn.conndb);// dataadapteur recuperer les donnees et le met dans la memoire vive
                DataTable maTable = new DataTable();// data table stocke les donnees recuperer par l'adaptateur
                monAdapteur.Fill(maTable);// nous remplissons nos données dans ma variable matable
                if (maTable.Rows.Count != 0)
                {

                    txtSolde.Text= Convert.ToString(Math.Round(Convert.ToDecimal(maTable.Rows[0][5].ToString()), 2));
                }

            }
            catch (Exception ex)
            {
                //ici on met le message d'erreur si l'execution a rencontré une erreure
                MessageBox.Show(ex.Message.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                // ici on met le code qui succede l'execution meilleure du code essayé
                //connexion.macon.Close();
            }


        }
        private void migrate()
        {
            if (dgvOperation.Rows.Count > 0)
            {
                visible();
                idop= dgvOperation.CurrentRow.Cells[1].Value.ToString();
                txtMontantdepot.Text = dgvOperation.CurrentRow.Cells[4].Value.ToString();
                txtMontantRetrait.Text = dgvOperation.CurrentRow.Cells[5].Value.ToString();
                txtLibelle.Text = dgvOperation.CurrentRow.Cells[3].Value.ToString();
            }
        }
        // Suppression et modfication
        //
        private void delete()
        {
            idop = Microsoft.VisualBasic.Interaction.InputBox("Entrez le numero de l'operation:", "information", "", 100, 100);
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(idop))
            {
                msg.getAttention("Erreur, veiller Choisir un client à supprimer ?");
            }
            else 
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDoperation", idop}
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Toperation obj = new Controleurs.C_Toperation(fields);
                obj.delete(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loard();
                    solde(cmbClient.Text);
                    viderLesTxt();
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

        // Save Method
        private void modify()
        {
            string retrait;
            if (string.IsNullOrEmpty(txtMontantRetrait.Text))
            {
                retrait = "0";
            }
            else
            {
                retrait = txtMontantRetrait.Text;
            }

            string depot;
            if (string.IsNullOrEmpty(txtMontantdepot.Text))
            {
                depot = "0";
            }
            else
            {
                depot = txtMontantdepot.Text;
            }
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(idop) || string.IsNullOrEmpty(cmbClient.Text) || string.IsNullOrEmpty(cmbDate.Text) || string.IsNullOrEmpty(cmbCaisse.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs?");
            }
            else
            {
                string id = ManagerSession.LireSession();
                //string datS = DateSession.LireSession();
                Item datS = (Item)cmbDate.SelectedItem;
                //Item caisse= (Item)cmbCaisse.SelectedItem;    
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDoperation", idop.ToString()},
                    {"IDdate", datS.ToString()},
                    {"MontantDepot", depot},
                    {"MontantRetrait", retrait},
                    {"Codeclient", cmbClient.Text},
                    {"libelle", txtLibelle.Text},
                    {"compteUser", ManagerSession.LireSession()},
                    {"IDApprov",cmbCaisse.Text}

                };

                //on passe les donnees dans le controllers
                Controleurs.C_Toperation obj = new Controleurs.C_Toperation(fields);
                obj.update(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loard();
                    solde(cmbClient.Text);
                    invisible();
                    viderLesTxt();
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
        // Vidage de champs
        private void viderLesTxt()
        {
          txtLibelle.Text= txtMontantRetrait.Text = txtMontantdepot.Text = string.Empty;
        }

        private void dgvOperation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            visible();
            modify();
           
        }
        private void Releveclient()
        {
            Services.MsgFRM msg = new Services.MsgFRM();

            // Configure les formats des DateTimePicker
            dtdebut.Format = DateTimePickerFormat.Custom;
            dtdebut.CustomFormat = "dd-MM-yyyy";

            dtFin.Format = DateTimePickerFormat.Custom;
            dtFin.CustomFormat = "dd-MM-yyyy";

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
                Vues.Reports.Document.ReleveCustomers fcli = new Vues.Reports.Document.ReleveCustomers();
                Reports.frmReports_design FF = new Reports.frmReports_design();

                // Ouvrez la connexion si elle est fermée
                if (con.conndb.State != ConnectionState.Open)
                {
                    con.conndb.Open();
                }

                // Créez la commande avec la procédure stockée et les paramètres
                using (MySqlCommand cmd = new MySqlCommand("CALL CalculerSoldeClient(@Codeclient,@dateDebut,@dateFin)", con.conndb))
                {
                    // Ajoutez les paramètres
                    cmd.Parameters.AddWithValue("@Codeclient", cmbClient.Text.ToString());
                    cmd.Parameters.AddWithValue("@dateDebut", dtdebut.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@dateFin", dtFin.Value.ToString("yyyy-MM-dd"));

                    // Exécutez la commande
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    DataSet1 ds = new DataSet1();
                    ada.Fill(ds, "Commande");

                    // Configurez le rapport avec les données récupérées
                    fcli.SetDataSource(ds.Tables["Commande"]);
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
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            visible();
            viderLesTxt();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            Releveclient();
        }

        private void UserOperation_Load(object sender, EventArgs e)
        {
            //loardApprov();
            loardcmbCaisse();
            invisible();
            initilDefaultDateDebut();

        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            saearchname(txtsearch.Text);
        }

        private void cmbClient_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            migrateCompte();
        }

        private void cmbCaisse_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void viderOP()
        {
            txtLibelle.Text=txtMontantdepot.Text=txtMontantRetrait.Text=string.Empty;
        }
        private void btnDepot_Click(object sender, EventArgs e)
        {
            savDepot();
           // MontantDepot();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumRecu.Text))
            {
                MessageBox.Show("Veillez Entrez le numéro ");
            }
            else
            {
                detailPese pese = new detailPese();
                pese.txtNumOperation.Text = txtNumRecu.Text;
                pese.txtDuexiemeIndicateur.Enabled = false;
                pese.txtNumOperation.Enabled = false;
                pese.txtIndice.Enabled = false;
                pese.txtPourcentage.Enabled = false;
                pese.txtPU.Enabled = false;
                pese.btnModif.Visible = false;
                pese.btnSupprimer.Visible = false;
                pese.btnPaiement.Visible = true;
                pese.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtAfficherRecu.Visible = !txtAfficherRecu.Visible;
        }

        private void cmdInterv_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnAct_Click(object sender, EventArgs e)
        {
            loardDate();
            MessageBox.Show("Connexion reussie!", "Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void grpBtn_Enter(object sender, EventArgs e)
        {

        }
    }
}
