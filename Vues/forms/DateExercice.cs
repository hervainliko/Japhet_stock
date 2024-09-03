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
using System.Web.Profile;
using System.Windows.Forms;

namespace Japhet.Vues.forms
{
    public partial class DateExercice : Form
    {
        public string id;
        public DateExercice()
        {
            InitializeComponent();
            loard();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string datess = string.Format("{0}-{1}-{2}", txtDate.Value.Year, txtDate.Value.Month, txtDate.Value.Day);
            int coumpt = 0;
            //on passe les donnees dans le controllers
            Modeles.M_Dateexercice obj = new Modeles.M_Dateexercice();
            obj.verifydate(datess);
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dgvOperation.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                
                if (dr.Read())
                {
                    coumpt = int.Parse(dr[0].ToString());
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
            if (coumpt>0)
            {
                MessageBox.Show("cette date existe");
            }
            else
            {
                save();
                
            }
            loard();

        }


        // Save Method
        private void save()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                string datess = string.Format("{0}-{1}-{2}", txtDate.Value.Year, txtDate.Value.Month, txtDate.Value.Day);
                Dictionary<string, string> fields = new Dictionary<string, string>{
                     {"DateExec", datess},

                };

                //on passe les donnees dans le controllers
                Controleurs.C_Dateexercice obj = new Controleurs.C_Dateexercice(fields);
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


        private void loard()
        {
            Modeles.M_Dateexercice obj = new Modeles.M_Dateexercice();
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
                    dgvOperation.Rows.Add(dr[0].ToString(), coumpt, dr[1].ToString());
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
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(txtDate.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs?");
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string> {
                    {"IDdate",id},
                    {"DateExec", txtDate.Text}
                };

                //on passe les donnees dans le controllers
                Controleurs.C_Dateexercice obj = new Controleurs.C_Dateexercice(fields);
                obj.update(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);
                    loard();
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

        private void dgvOperation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            migrate();
        }
        public void migrate()
        {
            if (dgvOperation.Rows.Count > 0)
            {
                id = dgvOperation.CurrentRow.Cells[1].Value.ToString();
                txtDate.Text = dgvOperation.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            delete();
        }
        private void delete()
        {
            id = Microsoft.VisualBasic.Interaction.InputBox("Entrez le numero de l'operation:", "information", "", 100, 100);
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(id))
            {
                msg.getAttention("Erreur, veiller Choisir le numero à supprimer ?");
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"IDdate", id}
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Dateexercice obj = new Controleurs.C_Dateexercice(fields);
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
    }
}
