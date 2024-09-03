using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Japhet.Vues.Reports.Document;
using MySql.Data.MySqlClient;

namespace Japhet.Vues.Reports
{
    public partial class frmReports_design : Form
    {
        public frmReports_design()
        {
            InitializeComponent();
            crystalReportViewer1.Top = 0;

        }
    }
}

