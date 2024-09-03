using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Vues.forms
{
    public partial class adim : Form
    {
        public adim()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connexion(txtAdmin.Text,txtpAdm.Text)==true)
            {
                config cc = new config();
                cc.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Connexion refusée");
            }
        }


        private bool connexion(string nom , string pass)
        {
            if (nom=="DBSC" && pass== "DBSC")
            {
                return true; 
            }else
                return false;
        }
    }
}
