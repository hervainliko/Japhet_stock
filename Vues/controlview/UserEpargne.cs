using Japhet.Services;
using Japhet.Vues.Reports.Document;
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
namespace Japhet.Vues.controlview
{
    public partial class UserEpargne : UserControl
    {
        private static UserEpargne userOperation;
        public string idEpag;
        public static UserEpargne instance
        {
            get
            {
                if (userOperation == null)
                {
                    userOperation = new UserEpargne();
                }
                return userOperation;
            }
        }
        public UserEpargne()
        {
            InitializeComponent();
            txtMontantdepot.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtMontantRetrait.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            loard();
            loardDate();
            loardcmbCaisse();
            //loard();

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
                    cmbCaisse.Items.Add(dr[0].ToString()); // Ajoute une nouvelle ComboboxItem au ComboBox
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
                    solde(cmbClient.Text);
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

        void solde(String compteclient)
        {

            try
            {
                Modeles.connection conn = new Modeles.connection();
                String cmdSelect = "select Codeclient,dateexercice.IDdate,dateexercice.DateExec, sum(montentdepot) as DEPOT,sum(Montretrait) as RETRAIT,SUM((montentdepot)-(Montretrait)) AS SOLDE FROM tplanepargne,dateexercice where tplanepargne.IDdate=dateexercice.IDdate AND  Codeclient= '" + compteclient + "' group by Codeclient; ";//selectionner les donnees de la table
                MySqlDataAdapter monAdapteur = new MySqlDataAdapter(cmdSelect, conn.conndb);// dataadapteur recuperer les donnees et le met dans la memoire vive
                DataTable maTable = new DataTable();// data table stocke les donnees recuperer par l'adaptateur
                monAdapteur.Fill(maTable);// nous remplissons nos données dans ma variable matable
                if (maTable.Rows.Count != 0)
                {

                    txtsolde.Text = Convert.ToString(Math.Round(Convert.ToDecimal(maTable.Rows[0][5].ToString()), 2));
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
        private void button10_Click(object sender, EventArgs e)
        {
            // Enregistrement 
            saveDepot();

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
        void visible()
        {
            cmbClient.Enabled = txtintituleclient.Enabled = txtlibelle.Enabled = txtMontantdepot.Enabled = txtMontantRetrait.Enabled = true;
        }
        private void migrateCompte()
        {
            if (dgvSearch.Rows.Count > 0)
            {
                visible();
                cmbClient.Text = dgvSearch.CurrentRow.Cells[0].Value.ToString();
                txtintituleclient.Text = dgvSearch.CurrentRow.Cells[1].Value.ToString();
                solde(cmbClient.Text);
                loard();
            }
        }
        private void migrate()
        {
            if (dgvOperation.Rows.Count > 0)
            {
                visible();
                //viderLesTxt();
                idEpag = dgvOperation.CurrentRow.Cells[0].Value.ToString();
                txtlibelle.Text = dgvOperation.CurrentRow.Cells[2].Value.ToString();
                txtMontantdepot.Text = dgvOperation.CurrentRow.Cells[3].Value.ToString();
                txtMontantRetrait.Text = dgvOperation.CurrentRow.Cells[4].Value.ToString();
                //cmbClient.Text= dgvOperation.CurrentRow.Cells[5].Value.ToString();
            }
        }
        // Suppression et modfication
        private void delete()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(idEpag))
            {
                msg.getAttention("Erreur, veiller Choisir un client à supprimer ?");
            }
            else if (msg.getDialog("Etes-vous sûr de vouloir supprimer ?"))
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDEPARG", idEpag},
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Tepargne obj = new Controleurs.C_Tepargne(fields);
                obj.delete(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loard();
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
            if (string.IsNullOrEmpty(idEpag))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                if (msg.getDialog("Etes-vous sûr de vouloir modifier ?"))
                {
                    Item datS = (Item)cmbDate.SelectedItem;
                    // Item idapprov = (Item)cmbCaisse.SelectedItem;
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDEPARG",idEpag},
                    {"IDdate",datS.ToString() },
                    {"libelle",txtlibelle.Text },
                    {"montentdepot", depot},
                    {"Montretrait", retrait},
                    {"Codeclient", cmbClient.Text},
                    {"CompteUser",ManagerSession.LireSession()},
                     {"IDApprov", cmbCaisse.Text}
                    };
                    //on passe les donnees dans le controllers
                    Controleurs.C_Tepargne obj = new Controleurs.C_Tepargne(fields);
                    obj.update(obj);

                    if (obj.message["type"] == "success")
                    {
                        msg.getInfo(obj.message["message"]);

                        loard();
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
        }

        // Vidage de champs
        private void viderLesTxt()
        {
            txtMontantRetrait.Text = txtMontantdepot.Text =txtlibelle.Text = string.Empty;
        }
        // Save Method
        private void saveDepot()
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
            if (string.IsNullOrEmpty(cmbClient.Text) || string.IsNullOrEmpty(cmbClient.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                Item datS = (Item)cmbDate.SelectedItem;
                // Item idapprov = (Item)cmbCaisse.SelectedItem;
                string id = ManagerSession.LireSession();
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDdate", datS.ToString() },
                    {"libelle",txtlibelle.Text},
                    {"montentdepot", depot},
                    {"Montretrait", retrait},
                    {"Codeclient", cmbClient.Text},
                    {"CompteUser", ManagerSession.LireSession()},
                    {"IDApprov", cmbCaisse.Text}

                };

                //on passe les donnees dans le controllers
                Controleurs.C_Tepargne obj = new Controleurs.C_Tepargne(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loard();
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
        public void loard()
        {
            Modeles.M_Tepargne obj = new Modeles.M_Tepargne();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dgvOperation.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    coumpt++;
                    dgvOperation.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
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

        private void dgvOperation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            modify();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmbClient.Text == "" && txtintituleclient.Text == "")
                {
                    MessageBox.Show("Veillez completez le numero ");
                }
                else
                {

                    Modeles.connection con = new Modeles.connection();
                    Vues.Reports.Document.ReleveEpargne fcli = new Vues.Reports.Document.ReleveEpargne();
                    Reports.frmReports_design FF = new Reports.frmReports_design();

                    if (con.conndb.State != ConnectionState.Open)
                    {
                        con.conndb.Open();
                    }
                    MySqlCommand cmd = new MySqlCommand("select * from ReleveEpargne where Codeclient='" + cmbClient.Text + "'", con.conndb);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    DataSet1 ds = new DataSet1();
                    ada.Fill(ds, "ReleveEpargne");
                    fcli.SetDataSource(ds.Tables["ReleveEpargne"]);
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

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            saearchname(txtsearch.Text);
        }

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            migrateCompte();
        }
        void invisible()
        {
            cmbClient.Enabled = txtintituleclient.Enabled = txtlibelle.Enabled = txtsolde.Enabled = txtMontantdepot.Enabled = txtMontantRetrait.Enabled = false;
        }
        private void UserEpargne_Load(object sender, EventArgs e)
        {
            invisible();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            visible();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loard();
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

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            string dd = Microsoft.VisualBasic.Interaction.InputBox("entree la date:", "Message", "", 100, 100);
            if (string.IsNullOrEmpty(dd))
            {
                msg.getAttention("Erreur, veillez completez la date");
            }
            else
            {
                try
                {
                    Modeles.connection con = new Modeles.connection();
                    Vues.Reports.Document.Repargne fcli = new Vues.Reports.Document.Repargne();
                    Reports.frmReports_design FF = new Reports.frmReports_design();

                    if (con.conndb.State != ConnectionState.Open)
                    {
                        con.conndb.Open();
                    }
                    MySqlCommand cmd = new MySqlCommand("select * from Repargne where DateExec='" + dd + "' ", con.conndb);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    DataSet1 ds = new DataSet1();
                    ada.Fill(ds, "Repargne");
                    fcli.SetDataSource(ds.Tables["Repargne"]);
                    FF.crystalReportViewer1.ReportSource = fcli;
                    FF.crystalReportViewer1.Refresh();
                    FF.Show();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
