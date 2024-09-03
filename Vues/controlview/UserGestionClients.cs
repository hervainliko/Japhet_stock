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
    public partial class UserGestionClients : UserControl
    {

        //UcGestion_Clients client = new UcGestion_Clients();
        public string id;
        private static UserGestionClients userClients;

        public static UserGestionClients instance
        {
            get
            {
                if (userClients == null)
                {
                    userClients = new UserGestionClients();
                }
                return userClients;
            }
        }
        public UserGestionClients()
        {
            InitializeComponent();
            loard();
        }

        private void txtReseach_Leave(object sender, EventArgs e)
        {
            if (txtReseach.Text == "")
            {
                txtReseach.Text = "Recherche ...........................";
                txtReseach.ForeColor = Color.DarkCyan;
            }
        }

        private void txtReseach_Enter(object sender, EventArgs e)
        {
            if (txtReseach.Text == "Recherche ...........................")
            {
                txtReseach.Text = "";
                txtReseach.ForeColor = Color.Black;
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            save();
            viderLesTxt();
            loard();
        }



        private void save()
        {
            string dateNai = string.Format("{0}-{1}-{2}", dtNaissance.Value.Year, dtNaissance.Value.Month, dtNaissance.Value.Day);
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtNoms.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"noms", txtNoms.Text},
                    {"genre", cmbGenre.Text},
                    {"piece", txtPiece.Text},
                    {"Numpiece", txtNumPiece.Text},
                    {"adresse", txtAdresse.Text},
                    {"fonction", txtFonction.Text},
                    {"telephone",txtPhone.Text},
                    {"Lieu_Naiss",txtLieuNaissnce.Text},
                    {"DateNaisse",dateNai}

                };

                //on passe les donnees dans le controllers
                Controleurs.C_Customs obj = new Controleurs.C_Customs(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loard();
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

        private void delete()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(id))
            {
                msg.getAttention("Erreur, veiller Choisir un client à supprimer ?");
            }
            else if (msg.getDialog("Etes-vous sûr de vouloir supprimer ?"))
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"Codeclient", id},
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Customs obj = new Controleurs.C_Customs(fields);
                obj.delete(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loard();
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
            string dateNai = string.Format("{0}-{1}-{2}", dtNaissance.Value.Year, dtNaissance.Value.Month, dtNaissance.Value.Day);
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(id))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                if (msg.getDialog("Etes-vous sûr de vouloir modifier ?"))
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"Codeclient", id},
                    {"noms", txtNoms.Text},
                    {"genre", cmbGenre.Text},
                    {"piece", txtPiece.Text},
                    {"Numpiece", txtNumPiece.Text},
                    {"adresse", txtAdresse.Text},
                    {"fonction", txtFonction.Text},
                    {"telephone",txtPhone.Text},
                    {"Lieu_Naiss",txtLieuNaissnce.Text},
                    {"DateNaisse",dateNai}
                    };

                    //on passe les donnees dans le controllers
                    Controleurs.C_Customs obj = new Controleurs.C_Customs(fields);
                    obj.update(obj);

                    if (obj.message["type"] == "success")
                    {
                        msg.getInfo(obj.message["message"]);

                        loard();
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


        private void migrate()
        {
            if (dgvClient.Rows.Count > 0)
            {
                viderLesTxt();
                id = dgvClient.CurrentRow.Cells[0].Value.ToString();
                txtNoms.Text = dgvClient.CurrentRow.Cells[1].Value.ToString();
                cmbGenre.Text = dgvClient.CurrentRow.Cells[2].Value.ToString();
                txtPiece.Text = dgvClient.CurrentRow.Cells[5].Value.ToString();
                txtNumPiece.Text = dgvClient.CurrentRow.Cells[6].Value.ToString();
                txtAdresse.Text = dgvClient.CurrentRow.Cells[7].Value.ToString();
                txtFonction.Text = dgvClient.CurrentRow.Cells[8].Value.ToString();
                txtPhone.Text = dgvClient.CurrentRow.Cells[9].Value.ToString();
                txtLieuNaissnce.Text = dgvClient.CurrentRow.Cells[3].Value.ToString();
            }
        }

        public void loard()
        {
            Modeles.M_Customes obj = new Modeles.M_Customes();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dgvClient.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;

                while (dr.Read())
                {
                    dgvClient.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString());
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
        private void search(string param)
        {
            Modeles.M_Customes obj = new Modeles.M_Customes();
            obj.reseach(param);
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dgvClient.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;

                while (dr.Read())
                {
                    dgvClient.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString());
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


        // Vider le champs 
        private void viderLesTxt()
        {
            txtNoms.Text = txtAdresse.Text= txtFonction.Text = txtNumPiece.Text = txtPhone.Text = txtPiece.Text = cmbGenre.Text=txtLieuNaissnce.Text = string.Empty;
        }

        private void dgvClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            delete();
            viderLesTxt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modify();
            viderLesTxt();
        }

        private void txtReseach_TextChanged(object sender, EventArgs e)
        {
            if (txtReseach.Text!="" && txtReseach.Text!= "Recherche ...........................")
            {
                search(txtReseach.Text);
            }
            else
            {
                loard();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            forms.loradExcel los = new forms.loradExcel();
            los.ShowDialog();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            string dd = Microsoft.VisualBasic.Interaction.InputBox("entree la date du jour:", "Message", "", 100, 100);
            if (string.IsNullOrEmpty(dd))
            {
                msg.getAttention("Erreur, veillez completez la date");
            }
            else
            {
                try
                {
                    Modeles.connection con = new Modeles.connection();
                    Vues.Reports.Document.Rlivre fcli = new Vues.Reports.Document.Rlivre();
       
                    Reports.frmReports_design FF = new Reports.frmReports_design();

                    if (con.conndb.State != ConnectionState.Open)
                    {
                        con.conndb.Open();
                    }
                    MySqlCommand cmd = new MySqlCommand("select * from Rlivre where DateExec='" + dd + "' ", con.conndb);
                    MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                    DataSet1 ds = new DataSet1();
                    ada.Fill(ds, "Rlivre");
                    fcli.SetDataSource(ds.Tables["Rlivre"]);
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
