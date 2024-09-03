using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace Japhet.Services
{
    public static class DateSession
    {
        public static string datedujour { get; private set; }


        // Méthode pour ouvrir une session pour un utilisateur
        public static void OuvrirSession(string datasession)
        {
            datedujour = datasession;
            //RoleUtilisateur = role;
        }

        // Méthode pour fermer la session
        public static void FermerSession()
        {

            datedujour = null;
        }
        public static void SauvegarderSession(string datesession)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("datesession.txt"))
                {
                    sw.WriteLine(datesession);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la sauvegarde de la session : " + ex.Message);
            }
        }

        // Méthode pour lire les informations de session à partir du fichier local
        public static string LireSession()
        {
            try
            {
                using (StreamReader sr = new StreamReader("datesession.txt"))
                {
                    return sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la lecture de la date de saisie : " + ex.Message);
                return null;
            }
        }

        // Méthode pour vérifier si une session existe déjà
        public static bool VerifierSessionExistante()
        {
            string datesession = LireSession();
            if (!string.IsNullOrEmpty(datesession))
            {
                // Si une session existe déjà, démarrer la session automatiquement
                OuvrirSession(datesession);
                return true;
            }
            else
                return false;
        }

        // Récuperation de la dernière date 
        public static string Getdate(string id)
        {
            var dernieredate = "";
            Modeles.M_Dateexercice obj = new Modeles.M_Dateexercice();
            obj.DescDate(id);
            if (obj.callback["type"] == "success")
            {
                MySqlDataReader dr = Apps.Query.DR;

                if (dr.Read())
                {
                    dernieredate = dr[1].ToString();
                }
                Apps.Query.DR.Close();
            }
            else if (obj.callback["type"] == "failure")
            {
                MsgFRM msg = new MsgFRM();
                msg.getError(obj.callback["message"]);
            }
            else
            {
                MsgFRM msg = new MsgFRM();
                msg.getError(obj.callback["message"]);
            }
            return dernieredate;

        }




    }

}
