using Japhet.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace Japhet.Modeles
{
    internal class connection
    {
        public static string ip_ { set; get; }
        public static string port_ { set; get; }
        public static string mode_ { set; get; }
        public static string database_ { set; get; }
        public static string username_ { set; get; }
        public static string password_ { set; get; }


        public MySqlConnection conndb;
        private static readonly object msg;

        public connection()
        {
            try 
            {
                //hervainliko98
                string host = "";
                string database = "";
                string username = "";
                string password = "";
                string port = "";

                // Vérifiez si une session existe et affichez les informations
                if (ConfigSessiondb.VerifierSessionExistante())
                {
                    List<string> sessionData = ConfigSessiondb.LireSession();
                    if (sessionData != null && sessionData.Count == 5)
                    {
                        host = sessionData[0];
                        port = sessionData[1];
                        database = sessionData[2];
                        username = sessionData[3];
                        password = sessionData[4];
                    }
                }
                else
                {
                    Services.MsgFRM msg = new Services.MsgFRM();
                    msg.getInfo("Completez le paramètres d'abord svp");
                }
                string connection_string = "datasource =" + host + "; database=" + database + ";username=" + username + ";password=" + password + "; Max Pool Size=50000; Pooling=True;port="+port+"";
                conndb = new MySqlConnection(connection_string);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }


        public static void try_connexion()
        {
            try
            {
                //creation et instentiation de la variable de test de connection conn_
                MySqlConnection conn_ = new MySqlConnection();

                string connection_string = "server=" + ip_ + "; port=" + port_ + "; user=" + username_ + "; password=" + password_ + "; database=" + database_ + "; Max Pool Size=50000; Pooling=True";

                conn_ = new MySqlConnection(connection_string);

                if (conn_.State == ConnectionState.Closed)
                {
                    conn_.Open();
                    System.Runtime.Remoting.Services.MsgFRM msg = new System.Runtime.Remoting.Services.MsgFRM();
                    msg.getInfo("connexion etablie");
                 
                }
                else
                {
                    conn_.Close();
                    System.Runtime.Remoting.Services.MsgFRM msg = new System.Runtime.Remoting.Services.MsgFRM();
                    msg.getError("Connexion échouer");
                }
            }
            catch (Exception)
            {
                System.Runtime.Remoting.Services.MsgFRM msg = new System.Runtime.Remoting.Services.MsgFRM();
                msg.getError("Connexion échouer");
            }
        }

    }
}
