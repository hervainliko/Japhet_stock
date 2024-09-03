using Japhet.Services;
//using MySqlX.XDevAPI.Relational;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Japhet.Apps
{
    internal class Schema
    {
        // declaration du dictionnaire avec cle_valeur 

        public Dictionary<string, string> table = new Dictionary<string, string>
        {
            {"approvcaisse","approvcaisse" },
            {"caisse","caisse" },
            {"customes","customes" },
            {"dateexercice","dateexercice" },
            {"DescPese","DescPese" },
            {"pesage","pesage" },
            {"role_attribute","role_attribute" },
            {"roles","roles" },
            {"toperations","toperations" },
            {"TPlanEpargne","tplanepargne"},
            {"usedb","usedb"},
            {"mouvement_caisse","mouvement_caisse"}
        };
        /// <summary>
        /// les champs des differentes tables
        /// </summary>

        public Dictionary<string, string> approvcaisse = new Dictionary<string, string>
        {
            {"IDApprov","IDApprov" },
            {"Montant","Montant" },
            {"compteUser","compteUser" },
            {"compteCaisse","compteCaisse" },
            {"IDdate","IDdate" }
            
        };
        // dictionnaire de la table userdb avec ses champs cle_valeur
        public Dictionary<string, string> caisse = new Dictionary<string, string>
        {
            {"compteCaisse","compteCaisse" },
            {"intituleCaisse","intituleCaisse" },
            {"Montantcaisse","Montantcaisse" }

        };
        // dictionnaire de la table des entrees avec ses champs cle_valeur
        public Dictionary<string, string> customes = new Dictionary<string, string>
        {
            { "Codeclient", "Codeclient" },
            { "noms", "noms" },
            { "genre", "genre" },
            { "Lieu_Naiss", "Lieu_Naiss" },
            { "DateNaisse", "DateNaisse" },
            { "piece", "piece" },
            { "Numpiece", "Numpiece" },
            { "adresse", "adresse" },
            { "fonction", "fonction" },
            { "telephone", "telephone" }
        };

        // dictionnaire de la table des Retrait avec ses champs cle_valeur
        public Dictionary<string, string> dateexercice = new Dictionary<string, string>
        {
            { "IDdate", "IDdate" },
            { "DateExec", "DateExec" }
        };

        // dictionnaire de la table des pesage avec ses champs cle_valeur
        public Dictionary<string, string> pesage = new Dictionary<string, string>
        {
            { "NumPese", "NumPese" },
            { "IDdate", "IDdate" },
            { "Codeclient", "Codeclient" },
            { "CompteUser", "CompteUser" },
            { "total", "total"},
            { "mention", "mention" }
        };
        // dictionnaire de la table description pesage avec ses champs
        public Dictionary<string, string> DescPese = new Dictionary<string, string>
        {
             { "IdDesc", "IdDesc" },
            { "numerateur", "numerateur" },
            { "indice", "indice" },
            { "pourcentage", "pourcentage" },
            { "PU", "PU" },
            { "poids", "poids" },
            { "NumPese", "NumPese" },
            { "total", "PT" },
            { "reference", "reference" },
            {"bourse","bourse" },
            {"teneur","teneur" }
            

        };

        // dictionnaire de la table des role_attribute avec ses champs cle_valeur
        public Dictionary<string, string> role_attribute = new Dictionary<string, string>
        {
            { "idAttrib", "idAttrib" },
            { "dateRole", "dateRole" },
            { "compteUser", "compteUser" },
            { "idRole", "idRole" }
        };

        // dictionnaire de la table des Roles avec ses champs cle_valeur
        public Dictionary<string, string> roles = new Dictionary<string, string>
        {
            { "idRole", "idRole" },
            { "NameRole", "NameRole" }
        };

        // dictionnaire de la table toperations des des caissiers avec ses champs cle_valeur
        public Dictionary<string, string> toperations = new Dictionary<string, string>
        {
            {"IDoperation","IDoperation"},
            {"Codeclient","Codeclient"},
            {"MontantDepot","MontantDepot"},
            {"MontantRetrait","MontantRetrait"},
            {"IDdate","IDdate"},
            {"libelle","libelle"},
            {"compteUser","compteUser"},
            {"IDApprov","IDApprov" },
        };
        // dictionnaire de la table des credit avec ses champs cle_valeur
        public Dictionary<string, string> TPlanEpargne = new Dictionary<string, string>
        {
            { "IDEPARG", "IDEPARG" },
            { "IDdate", "IDdate" },
            {"libelle","libelle" },
            { "montentdepot", "montentdepot" },
            { "Montretrait", "Montretrait" },
            { "Codeclient", "Codeclient" },
            { "CompteUser", "CompteUser" },
            {"IDApprov","IDApprov" }
        };
        // dictionnaire de la table des usedb avec ses champs cle_valeur
        public Dictionary<string, string> usedb = new Dictionary<string, string>
        {
            { "compteUser", "compteUser" },
            { "NomUser", "NomUser" },
            { "UserPass", "UserPass" }
        };

        // dictionnaire de la table des usedb avec ses champs cle_valeur
        public Dictionary<string, string> mouvement_caisse = new Dictionary<string, string>
        {
            { "id", "id" },
            { "caisse", "caisse" },
            { "debut", "debut" },
            { "credit", "credit" },
            { "dates", "dates" }
        };
    }
}