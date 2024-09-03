using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Japhet.Services
{
    public static class ManagerSession
    {
        public enum Role
        {
            Administrateur,
            UtilisateurStandard
        }
        public static string NomUtilisateur { get; private set; }
        public static Role RoleUtilisateur { get; private set; }

        // Méthode pour ouvrir une session pour un utilisateur
        public static void OuvrirSession(string nomUtilisateur)
        {
            NomUtilisateur = nomUtilisateur;
            //RoleUtilisateur = role;
        }

        // Méthode pour fermer la session
        public static void FermerSession()
        {

            NomUtilisateur = null;
            RoleUtilisateur = Role.UtilisateurStandard; // Réinitialisation du rôle à utilisateur standard
        }

        // Méthode pour vérifier si un utilisateur a le rôle requis pour accéder à une fonctionnalité
        public static bool VerifierRole(Role roleRequis)
        {
            return RoleUtilisateur >= roleRequis;
        }


        public static void SauvegarderSession(string utilisateur)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("session.txt"))
                {
                    sw.WriteLine(utilisateur);
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
                using (StreamReader sr = new StreamReader("session.txt"))
                {
                    return sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la lecture de la session : " + ex.Message);
                return null;
            }
        }

        // Méthode pour vérifier si une session existe déjà
        public static bool VerifierSessionExistante()
        {
            string utilisateur = LireSession();
            if (!string.IsNullOrEmpty(utilisateur))
            {
                // Si une session existe déjà, démarrer la session automatiquement
                OuvrirSession(utilisateur);
                return true;
            }
            else
                return false;
        }



        public static string GetNameUserConnected(int id)
        {
            var dataUser = "";
            Modeles.M_Users obj = new Modeles.M_Users();
            obj.reseachSession(id.ToString());
            if (obj.callback["type"] == "success")
            {
                MySqlDataReader dr = Apps.Query.DR;
                var roles = new List<string>();

                if (dr.Read())
                {
                    dataUser = dr.GetString(1);
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
            return dataUser;

        }


    }
}
