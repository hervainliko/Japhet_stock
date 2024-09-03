using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Japhet.Services
{
    public static class ConfigSessiondb
    {
        public static string serveur { get; private set; }
        public static string port { get; private set; }
        public static string dbname { get; private set; }
        public static string dbUser { get; private set; }
        public static string dbopass { get; private set; }

        // Méthode pour ouvrir une session pour un utilisateur
        public static void OuvrirSession(string srv, string prt, string db, string user, string pass)
        {
            serveur = srv;
            port = prt;
            dbname = db;
            dbUser = user;
            dbopass = pass;
        }

        // Méthode pour fermer la session
        public static void FermerSession()
        {
            serveur = null;
            port = null;
            dbname = null;
            dbUser = null;
            dbopass = null;
        }

        // Méthode pour sauvegarder la session
        public static void SauvegarderSession(string srv, string prt, string db, string user, string pass)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("configdb.txt"))
                {
                    sw.WriteLine(srv);
                    sw.WriteLine(prt);
                    sw.WriteLine(db);
                    sw.WriteLine(user);
                    sw.WriteLine(pass);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la sauvegarde de la session : " + ex.Message);
            }
        }



        public static void SupprimerSession()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("configdb.txt"))
                {
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression de la session : " + ex.Message);
            }
        }

        // Méthode pour lire les informations de session à partir du fichier local
        public static List<string> LireSession()
        {
            try
            {
                List<string> sessionData = new List<string>();
                using (StreamReader sr = new StreamReader("configdb.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sessionData.Add(line);
                    }
                }
                return sessionData;
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
            List<string> sessionData = LireSession();
            if (sessionData != null && sessionData.Count == 5)
            {
                string srv = sessionData[0];
                string prt = sessionData[1];
                string db = sessionData[2];
                string user = sessionData[3];
                string pass = sessionData[4];

                if (!string.IsNullOrEmpty(srv) && !string.IsNullOrEmpty(prt) && !string.IsNullOrEmpty(db) &&
                    !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
                {
                    // Si une session existe déjà, démarrer la session automatiquement
                    OuvrirSession(srv, prt, db, user, pass);
                    return true;
                }
            }
            return false;
        }
    }
}

