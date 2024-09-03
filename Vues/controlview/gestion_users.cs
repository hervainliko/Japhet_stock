using Japhet.Services;
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
    public partial class gestion_users : UserControl
    {
        private static gestion_users gestionUser;
        public static gestion_users instance
        {
            get
            {
                if (gestionUser == null)
                {
                    gestionUser = new gestion_users();
                }
                return gestionUser;
            }
        }
        public gestion_users()
        {
            InitializeComponent();
        }



        private void migrateUser()
        {
            if (dataUsers.Rows.Count > 0)
            {
                //viderLesTxt();
                txidUser.Text = dataUsers.CurrentRow.Cells[1].Value.ToString();
                txtNomUser.Text = dataUsers.CurrentRow.Cells[2].Value.ToString();
                //cmbClient.Text= dgvOperation.CurrentRow.Cells[5].Value.ToString();
            }
        }
        // Suppression et modfication
        private void deleteUser()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txidUser.Text))
            {
                msg.getAttention("Erreur, veiller Choisir un client à supprimer ?");
            }
            else if (msg.getDialog("Etes-vous sûr de vouloir supprimer ?"))
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"compteUser", txidUser.Text},
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Users obj = new Controleurs.C_Users(fields);
                obj.delete(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardRole();
                    loardAttribute();
                    loardUser();
                    loardRoles();
                    loardUsers();
                    viderLesTxtUser();

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
            if (string.IsNullOrEmpty(txidUser.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                if (msg.getDialog("Etes-vous sûr de vouloir modifier ?"))
                {
                    string datess = DateSession.LireSession();
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                        {"compteUser",txidUser.Text},
                        {"NomUser", txtNomUser.Text}
                    };
                    //on passe les donnees dans le controllers
                    Controleurs.C_Users obj = new Controleurs.C_Users(fields);
                    obj.update(obj);

                    if (obj.message["type"] == "success")
                    {
                        msg.getInfo(obj.message["message"]);

                        loardUser();
                        viderLesTxtUser();
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
        private void viderLesTxtUser()
        {
            txidUser.Text = txtNomUser.Text = txtPassword.Text = string.Empty;
        }


        // Save Method
        private void saveUser()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtNomUser.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"NomUser", txtNomUser.Text},
                    {"UserPass",txtPassword.Text}

                };

                //on passe les donnees dans le controllers
                Controleurs.C_Users obj = new Controleurs.C_Users(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardRole();
                    loardAttribute();
                    loardUser();
                    loardRoles();
                    loardUsers();

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
        private void loardUser()
        {
            Modeles.M_Users obj = new Modeles.M_Users();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dataUsers.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    coumpt++;
                    dataUsers.Rows.Add(coumpt,dr[0].ToString(), dr[1].ToString());
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


        // Methode Les Roles 
        private void migrateRole()
        {
            if (dataRoles.Rows.Count > 0)
            {
                //viderLesTxt();
                txtIdRole.Text = dataRoles.CurrentRow.Cells[1].Value.ToString();
                txtNomRole.Text = dataRoles.CurrentRow.Cells[2].Value.ToString();
                //cmbClient.Text= dgvOperation.CurrentRow.Cells[5].Value.ToString();
            }
        }
        // Suppression et modfication
        private void deleteRole()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtIdRole.Text))
            {
                msg.getAttention("Erreur, veiller Choisir un client à supprimer ?");
            }
            else if (msg.getDialog("Etes-vous sûr de vouloir supprimer ?"))
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"idRole", txtIdRole.Text},
                };
                //on passe les donnees dans le controllers
                Controleurs.C_Roles obj = new Controleurs.C_Roles(fields);
                obj.delete(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardRole();
                    loardAttribute();
                    loardUser();
                    loardRoles();
                    loardUsers();
                    viderLesTxtRole();
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
        private void modifyRole()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtIdRole.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                if (msg.getDialog("Etes-vous sûr de vouloir modifier ?"))
                {
                    string datess = DateSession.LireSession();
                    Dictionary<string, string> fields = new Dictionary<string, string>{
                        {"idRole",txtIdRole.Text},
                        {"NameRole", txtNomRole.Text}
                    };
                    //on passe les donnees dans le controllers
                    Controleurs.C_Roles obj = new Controleurs.C_Roles(fields);
                    obj.update(obj);

                    if (obj.message["type"] == "success")
                    {
                        msg.getInfo(obj.message["message"]);

                        loardRole();
                        loardAttribute();
                        loardUser();
                        loardRoles();
                        loardUsers();
                        viderLesTxtRole();
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
        private void viderLesTxtRole()
        {
            txtIdRole.Text = txtNomRole.Text = string.Empty;
        }


        // Save Method
        private void saveRole()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtNomRole.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"NameRole", txtNomRole.Text}

                };

                //on passe les donnees dans le controllers
                Controleurs.C_Users obj = new Controleurs.C_Users(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardRole();
                    loardAttribute();
                    loardUser();
                    loardRoles();
                    loardUsers();
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
        private void loardRole()
        {
            Modeles.M_Roles obj = new Modeles.M_Roles();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dataRoles.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    coumpt++;
                    dataRoles.Rows.Add(coumpt, dr[0].ToString(), dr[1].ToString());
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

        // Methode Les Roles 
        private void migrateAttribut()
        {
            if (dataAttribut.Rows.Count > 0)
            {
                //viderLesTxt();
                txtIdAttribute.Text = dataAttribut.CurrentRow.Cells[1].Value.ToString();
                //cmbClient.Text= dgvOperation.CurrentRow.Cells[5].Value.ToString();
            }
        }
        // Suppression et modfication
        private void deleteAttribute()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(txtIdAttribute.Text))
            {
                msg.getAttention("Erreur, veiller Choisir un client à supprimer ?");
            }
            else if (msg.getDialog("Etes-vous sûr de vouloir supprimer ?"))
            {
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"idAttrib", txtIdAttribute.Text}
                };
                //on passe les donnees dans le controllers
                Controleurs.C_RoleAttribute obj = new Controleurs.C_RoleAttribute(fields);
                obj.delete(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardRole();
                    loardAttribute();
                    loardUser();
                    loardRoles();
                    loardUsers();
                    viderLesTxtAttribute();
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
        private void viderLesTxtAttribute()
        {
            txtIdAttribute.Text = cmbRole.Text = cmbUser.Text=string.Empty;
        }


        // Save Method
        private void saveAttribute()
        {
            Services.MsgFRM msg = new Services.MsgFRM();
            if (string.IsNullOrEmpty(cmbRole.Text) || string.IsNullOrEmpty(cmbUser.Text))
            {
                msg.getAttention("Erreur, veiller remplir tous les champs ?");
            }
            else
            {
                string dates = DateSession.LireSession();
                Item user = (Item)cmbUser.SelectedItem;
                Item role = (Item)cmbRole.SelectedItem;
                Dictionary<string, string> fields = new Dictionary<string, string>{
                    {"dateRole", dates},
                    {"compteUser", user.ToString()},
                    {"idRole", role.ToString()}

                };

                //on passe les donnees dans le controllers
                Controleurs.C_RoleAttribute obj = new Controleurs.C_RoleAttribute(fields);
                obj.add(obj);

                if (obj.message["type"] == "success")
                {
                    msg.getInfo(obj.message["message"]);

                    loardRole();
                    loardAttribute();
                    loardUser();
                    loardRoles();
                    loardUsers();
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
        private void loardAttribute()
        {
            Modeles.M_role_attribute obj = new Modeles.M_role_attribute();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                //on vide la dgv
                dataAttribut.Rows.Clear();
                MySqlDataReader dr = Apps.Query.DR;
                int coumpt = 0;
                while (dr.Read())
                {
                    coumpt++;
                    dataAttribut.Rows.Add(coumpt, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
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

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            saveUser();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            deleteUser();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            modifyUser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveAttribute();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteAttribute();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveRole();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            deleteRole();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            modifyRole();
        }

        private void dataUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrateUser();
        }

        private void dataAttribut_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrateAttribut();
        }

        private void dataRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            migrateRole();
        }

        private void gestion_users_Load(object sender, EventArgs e)
        {
            loardRole();
            loardAttribute();
            loardUser();
            loardRoles();
            loardUsers();
        }

        private void loardUsers()
        {
            Modeles.M_Users obj = new Modeles.M_Users();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                cmbUser.Items.Clear();
                //on vide la dgv

                cmbUser.DisplayMember = "Value"; // Affiche la propriété "Value" dans le ComboBox
                cmbUser.ValueMember = "Id"; // Stocke la propriété "Id" associée à chaque élément sélectionné
                MySqlDataReader dr = Apps.Query.DR;
                while (dr.Read())
                {
                    cmbUser.Items.Add(new Services.Item { Id = int.Parse(dr[0].ToString()), Value = dr[1].ToString() }); // Ajoute une nouvelle ComboboxItem au ComboBox
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

        private void loardRoles()
        {
            Modeles.M_Roles obj = new Modeles.M_Roles();
            obj.get();
            if (obj.callback["type"] == "success")
            {
                cmbRole.Items.Clear();
                //on vide la dgv

                cmbRole.DisplayMember = "Value"; // Affiche la propriété "Value" dans le ComboBox
                cmbRole.ValueMember = "Id"; // Stocke la propriété "Id" associée à chaque élément sélectionné
                MySqlDataReader dr = Apps.Query.DR;
                while (dr.Read())
                {
                    cmbRole.Items.Add(new Services.Item { Id = int.Parse(dr[0].ToString()), Value = dr[1].ToString() }); // Ajoute une nouvelle ComboboxItem au ComboBox
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

        private void button3_Click(object sender, EventArgs e)
        {
            loardRole();
            loardAttribute();
            loardUser();
            loardRoles();
            loardUsers();
        }
    }
}
