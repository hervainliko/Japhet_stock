using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Vues.Reports.Document
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'DataSet1.recus'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
            if(!string.IsNullOrEmpty(numPese.Text))
            {
               // this.recusTableAdapter.Fill(this.DataSet1.recus,int.Parse(numPese.Text));
                //this.reportViewer1.RefreshReport();
            }
        }
    }
}
