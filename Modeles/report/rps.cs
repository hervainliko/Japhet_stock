using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Modeles.report
{
    internal class rps
    {
        public Dictionary<string, string> message;
        private string Query;
        
        MySqlDataAdapter InvPhisiqueAdapter;
        private ReportDocument document;

        //les dates du rapports
        public static string id_student, compteclient, dte = string.Empty;
        
        private void sql(string byQuery)
        {
            if (byQuery == "Releve compte client")
            {
                Query = $"select customes.Codeclient,customes.noms,customes.genre,customes.telephone,customes.adresse,customes.DateNaisse,customes.fonction,toperations.IDoperation,toperations.libelle,toperations.MontantDepot,toperations.MontantRetrait FROM customes,toperations where customes.Codeclient=toperations.Codeclient order by toperations.IDoperation ASC;";
            }
            else if (byQuery == "Approvisionnement caisse")
            {
                Query = $"select usedb.compteUser,usedb.NomUser, role_attribute.idAttrib,role_attribute.idRole,roles.NameRole,approvcaisse.IDApprov,approvcaisse.ApproVEntree,approvcaisse.IDdate,approvcaisse.libelle from usedb,role_attribute,roles,approvcaisse,dateexercice where usedb.compteUser=approvcaisse.compteUser and usedb.compteUser=role_attribute.compteUser and roles.idRole=role_attribute.idRole and dateexercice.IDdate=approvcaisse.IDdate order by dateexercice.IDdate ASC;";
            }
        }
        public ReportDocument rpsView(string parm)
        {
            try
            {
                sql(parm); //le code sql
                string byQuery = parm;

                if (byQuery == "Releve compte client")
                {
                    InvPhisiqueAdapter = new MySqlDataAdapter(Query, Apps.Query.conn);
                    InvPhisiqueAdapter.SelectCommand.CommandTimeout = 0;
                    Vues.Reports.Document.DataSet1 DataSet = new Vues.Reports.Document.DataSet1();
                    InvPhisiqueAdapter.Fill(DataSet, "ReleveClient");
                    Vues.Reports.Document.ReleveCustomers RLVC = new Vues.Reports.Document.ReleveCustomers();
                    RLVC.SetDataSource(DataSet.Tables["ReleveClient"]);
                    document = RLVC;
                }
                else if (byQuery == "Approvisionnement caisse")
                {
                    InvPhisiqueAdapter = new MySqlDataAdapter(Query, Apps.Query.conn);
                    InvPhisiqueAdapter.SelectCommand.CommandTimeout = 0;
                    Vues.Reports.Document.DataSet1 DataSet = new Vues.Reports.Document.DataSet1();
                    InvPhisiqueAdapter.Fill(DataSet, "ApprovCaisse");
                    Vues.Reports.Document.ReportCaisse RCAISSE = new Vues.Reports.Document.ReportCaisse();
                    RCAISSE.SetDataSource(DataSet.Tables["ApprovCaisse"]);
                    // entete du rapport
                    TextObject title = (TextObject)RCAISSE.ReportDefinition.Sections["Section1"].ReportObjects["TextTitle"];
                    //title.Text = "RAPPORT DU CAISSIER: " + dte;
                    //On passe le document
                    document = RCAISSE;
                }
                message = new Dictionary<string, string> {
                    { "type", "success" }, { "message", "Rapport générer" }
                };
            }
            catch (Exception ex)
            {
                message = new Dictionary<string, string> {
                        { "type", "failure" }, { "message", "Erreur du rapport générer " + ex.ToString() }
              };
            }
            return document;
        }
    }
}
