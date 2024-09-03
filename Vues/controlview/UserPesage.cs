using Japhet.Services;
using Japhet.Vues.forms;
using Japhet.Vues.Reports.Document;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Japhet.Vues.controlview
{
    public partial class UserPesage : UserControl
    {
        int idpesqge = 0;

        private static UserPesage userOperation;

        public static UserPesage instance
        {
            get
            {
                if (userOperation == null)
                {
                    userOperation = new UserPesage();
                }
                return userOperation;
            }
        }
        public UserPesage()
        {
            InitializeComponent();
            loard();
            viderLesTxt();
            virifaction();

        }

        private void button5_Click(object sender, EventArgs e)
        {
           // save();
            addDetail();
            //descSave();
            pnlDescpesage.Visible = true;
            dgvPesage.Visible = false;

        }


        // Save Method
        private void save()
        {
            Random RD = new Random();
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtNumerateur.Text) || string.IsNullOrEmpty(txtIndice.Text) || string.IsNullOrEmpty(txtPourcentage.Text) || string.IsNullOrEmpty(txtPU.Text) || string.IsNullOrEmpty(cmbClient.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                idpesqge = RD.Next();
                string mention = "Non Payé";
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"NumPese",idpesqge.ToString()},
                     {"IDdate", DateSession.LireSession()},
                    {"Codeclient", cmbClient.Text},
                    {"CompteUser", ManagerSession.LireSession()},
                    {"total",txtTotale.Text},
                    {"mention",mention}
                };

                //on passe les donnees dans le controllers
                Controleurs.C_Pesage obj = new Controleurs.C_Pesage(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);
                    descSave();
                    //loard();
                    // viderLesTxt();
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
        private void descSave()
        {

            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtNumerateur.Text) || string.IsNullOrEmpty(txtIndice.Text) || string.IsNullOrEmpty(txtPourcentage.Text) || string.IsNullOrEmpty(txtPU.Text) || string.IsNullOrEmpty(cmbClient.Text)|| string.IsNullOrEmpty(txtbourse.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                Controleurs.C_Descpese obj=null;
                foreach (var item in Apps.Descpesages.listDetail)
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"numerateur", item.numerateur},
                    {"indice", item.indice},
                    {"pourcentage", item.pourcentage},
                    {"PU", item.PU},
                    {"poids", item.poids},
                    {"NumPese",idpesqge.ToString()},
                    {"total",item.total},
                    {"reference",item.reference},
                    {"bourse",item.bourse},
                    {"teneur",item.teneur},
                };

                    //on passe les donnees dans le controllers
                    obj = new Controleurs.C_Descpese(fields);
                    obj.add(obj);
                }
                    

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loard();
                    viderLesTxt();
                    pnlDescpesage.Visible = false;
                    dgvPesage.Visible = true;
                    // Vider la liste une fois ajouter dans la base de données 

                    Apps.Descpesages.listDetail.Clear();
                    actualise_dgvDetailCommande();

                    //Close();
                    printRecu();
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
            Modeles.M_Descpese obj = new Modeles.M_Descpese();
            string cptezclient = cmbClient.Text;
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
                    dgvPesage.Rows.Add(dr[0].ToString(), coumpt, dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
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

        private void txtNumerateur_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Calcule du poids 
                if (!string.IsNullOrEmpty(txtNumerateur.Text))
                {
                    double numérateur = double.Parse(txtNumerateur.Text.Replace(",", "."));
                    double poids = numérateur / 11.664;
                    double arrondd = Math.Truncate(poids * 100) / 100;
                    txtPoids.Text = arrondd.ToString().Replace(",", ".");
                    txtPoids.ForeColor = Color.White;

                }
                else
                {
                    txtPoids.Text = string.Empty;
                }
            }catch (FormatException ex)
            {
                MsgFRM msg = new MsgFRM();
                msg.getError("Erreur: "+ex.Message);
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
        private void migrateCompte()
        {
            if (dgvSearch.Rows.Count > 0)
            {

                cmbClient.Text = dgvSearch.CurrentRow.Cells[0].Value.ToString();
                txtintituleclient.Text = dgvSearch.CurrentRow.Cells[1].Value.ToString();
                loard();
            }
        }
        private void txtPrixTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void migrate()
        {
            if (dgvPesage.Rows.Count > 0)
            {
                viderLesTxt();

               idpesqge = int.Parse(dgvPesage.CurrentRow.Cells[0].Value.ToString());
            }
        }
        // Suppression et modfication
        private void delete()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(idpesqge.ToString()))
            {
                msg.getAttention("Erreur, veiller Choisir Une opération à supprimer ?");
            }
            else if (msg.getDialog("Etes-vous sûr de vouloir supprimer ?"))
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"NumPese", idpesqge.ToString()},
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Pesage obj = new Controleurs.C_Pesage(fields);
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
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtId.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                if (msg.getDialog("Etes-vous sûr de vouloir modifier ?"))
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"NumPese", txtId.Text},
                    {"datePese", DateSession.LireSession()},
                    {"numerateur", txtNumerateur.Text},
                    {"indice", txtIndice.Text},
                    {"pourcentage", txtPourcentage.Text},
                    {"PU", txtPU.Text},
                    {"Codeclient", cmbClient.Text},
                    {"CompteUser",ManagerSession.LireSession()}
                    };
                    //on passe les donnees dans le controllers
                    Controleurs.C_Pesage obj = new Controleurs.C_Pesage(fields);
                    obj.update(obj);

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
        }

        // Vidage de champs
        private void viderLesTxt()
        {
           txtNumerateur.Text = txtPrixTotal.Text = txtPU.Text = txtPourcentage.Text = txtPoids.Text = txtTenneurMatiere.Text = txtintituleclient.Text = txtIndice.Text = txtDuexiemeIndicateur.Text = string.Empty;
        }

        private void dgvPesage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrate();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            modify();
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            printRecu();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            saearchname(txtsearch.Text);
        }

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            migrateCompte();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlDescpesage.Visible = false;
            save();
        }




        private void addDetail()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtNumerateur.Text) || string.IsNullOrEmpty(txtIndice.Text) || string.IsNullOrEmpty(txtPourcentage.Text) || string.IsNullOrEmpty(txtPU.Text) || string.IsNullOrEmpty(cmbClient.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                Random rr= new Random();
                Apps.Descpesages listPese = new Apps.Descpesages
                {
                    id=rr.Next(),
                    NumPese = idpesqge,
                    poids = txtPoids.Text,
                    PU = txtPU.Text,
                    pourcentage = txtPourcentage.Text,
                    indice = txtIndice.Text,
                    numerateur = txtNumerateur.Text,
                    total= txtPrixTotal.Text, 
                    reference=txtDuexiemeIndicateur.Text,
                    bourse=txtbourse.Text,
                   teneur=txtTenneurMatiere.Text,
               
                };

                Apps.Descpesages.listDetail.Add(listPese);
                // Actualiser le dgvdetailCommande 
                
            }
            actualise_dgvDetailCommande();
        }


        // DgvCommande 
        public void actualise_dgvDetailCommande()
        {
            // Calcul de total
            float total = 0;

            //if (txtTva.Text != "") TVA = float.Parse(txtTva.Text);
            dataDespese.Rows.Clear();
            int num= 0;
            foreach (var item in Apps.Descpesages.listDetail)
            {
                num++;
                dataDespese.Rows.Add(num,item.id,item.NumPese,item.reference,item.numerateur,item.indice, item.pourcentage, item.poids, item.PU, item.total) ;
                total = total + float.Parse(item.total);
            }

            // Affichage de total dans le textBox
            txtTotale.Text = total.ToString();

            // calcul de total toute taxe comprise
           // total_TTC = total + (total * TVA / 100);

            // Afficher totale Toute taxe comprise dans le txttotalttc
            //txtTotalTTC.Text = total_TTC.ToString();
        }

        private void retirerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Supprimer le chant d'invitatoire
            if (dataDespese.CurrentRow != null)
            {
                DialogResult rr = MessageBox.Show("Voulez vous retirer l'opération ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rr == DialogResult.Yes)
                {
                    // Index
                    int index = Apps.Descpesages.listDetail.FindIndex(s => s.id == int.Parse(dataDespese.CurrentRow.Cells[1].Value.ToString()));
                    Apps.Descpesages.listDetail.RemoveAt(index);
                    MessageBox.Show("Opération réussie avec succès");

                    // Actualiser 
                    actualise_dgvDetailCommande();
                }
                else
                {
                    MessageBox.Show("Opération annulée");
                }

            }
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (dataDespese.Rows.Count > 0)
                {
                    viderLesTxt();
                    txtDuexiemeIndicateur.Text= dataDespese.CurrentRow.Cells[3].Value.ToString();
                    txtIdOpPes.Text = dataDespese.CurrentRow.Cells[1].Value.ToString();
                    txtNumerateur.Text = dataDespese.CurrentRow.Cells[4].Value.ToString();
                    txtIndice.Text = dataDespese.CurrentRow.Cells[5].Value.ToString();
                    txtPourcentage.Text = dataDespese.CurrentRow.Cells[6].Value.ToString();
                    txtPU.Text = dataDespese.CurrentRow.Cells[7].Value.ToString();
                    btnEditer.Visible = true;
                    button5.Visible = false;
                }
            else
            {
                MessageBox.Show("Veuillez selectionner");
            }

        }

        private void btnEditer_Click(object sender, EventArgs e)
        {

            Apps.Descpesages listPese = new Apps.Descpesages
            {
                id = int.Parse(txtIdOpPes.Text),
                NumPese = idpesqge,
                poids = txtPoids.Text,
                PU = txtPU.Text,
                pourcentage = txtPourcentage.Text,
                indice = txtIndice.Text,
                numerateur = txtNumerateur.Text,
                total = txtPrixTotal.Text,
                reference=txtDuexiemeIndicateur.Text
            };

            
       
            DialogResult rr = MessageBox.Show("Voulez - vous éditer ?", "Modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rr == DialogResult.Yes)
            {
                //Trouver l'index
                int index = Apps.Descpesages.listDetail.FindIndex(s => s.id == int.Parse(txtIdOpPes.Text));
                Apps.Descpesages.listDetail[index] = listPese;
                MessageBox.Show("Modification réussie");
                btnEditer.Visible= false;
                button5.Visible= true;
            }
            else
            {
                MessageBox.Show("Modification annulée");
            }
            actualise_dgvDetailCommande();

        }

        private void UserPesage_Load(object sender, EventArgs e)
        {
            calcult();
        }

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
            }catch (FormatException ex)
            {
                MessageBox.Show("Erreur Format incorrect");
            }
        }

        private void txtPoids_Click(object sender, EventArgs e)
        {
            
        }

        private void txtPoids_VisibleChanged(object sender, EventArgs e)
        {
            calcult();
        }

        private void txtPoids_TextChanged(object sender, EventArgs e)
        {
            calcult();
        }
        private void printRecu()
        {
            try
            {
                if (idpesqge == 0)
                {
                    MessageBox.Show("Veillez selectionner");
                }
                else
                {

                    Modeles.connection con = new Modeles.connection();
                    Vues.Reports.Document.Recu fcli = new Vues.Reports.Document.Recu();
                    Reports.frmReports_design FF = new Reports.frmReports_design();


                    if (con.conndb.State != ConnectionState.Open)
                    {
                        con.conndb.Open();
                    }
                    MySqlCommand cmd = new MySqlCommand("select * from recu where NumPese='" + idpesqge + "'", con.conndb);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    DataSet1 ds = new DataSet1();
                    PrintDocument printDoc = new PrintDocument();
                    printDoc.DefaultPageSettings.PaperSize = new PaperSize("custom", 300, 200);
                    printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                    ada.Fill(ds, "recu");
                    fcli.SetDataSource(ds.Tables["recu"]);
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

        private void dataDespese_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idpesqge.ToString()) || idpesqge == 0)
            {
                MessageBox.Show("Veillez selectionner une opération ");
            }
            else
            {
                detailPese pese = new detailPese();
                pese.txtNumOperation.Text = idpesqge.ToString();
                pese.txtDuexiemeIndicateur.Enabled = true;
                pese.txtNumOperation.Enabled = true;
                pese.txtIndice.Enabled=true;
                pese.txtPourcentage.Enabled=true;
                pese.txtPU.Enabled=true;
                pese.btnModif.Visible = true;
                pese.btnSupprimer.Visible = true;
                pese.btnPaiement.Visible = false;
                pese.ShowDialog();
            }
        }

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
                }catch (FormatException ex){
                    Services.MsgFRM msg = new Services.MsgFRM();
                    msg.getError("Format entré est inconu");
                }
            }
        }

        private void txtPourcentage_TextChanged(object sender, EventArgs e)
        {

        }

        // Verification
        private void virifaction()
        {
            txtDuexiemeIndicateur.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtNumerateur.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtIndice.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtPourcentage.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtPoids.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
            txtPU.KeyPress += new KeyPressEventHandler(VerificationChamps.TextBox_KeyPress);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loard();
                }
    }
}
