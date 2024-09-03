using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Modeles
{
    internal class M_Tepargne
    {
        public Dictionary<string, string> callback;
        public string id;
        // dictionnaire d'insertion dans la base de donnees 
        public async void insert(Dictionary<string, string> args)
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    if (await Apps.Query.insertPrepared(
                               schema.table["TPlanEpargne"],
                                new MySqlParameter($"@{schema.TPlanEpargne["IDdate"]}", args["IDdate"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["libelle"]}", args["libelle"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["montentdepot"]}", args["montentdepot"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["Montretrait"]}", args["Montretrait"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["Codeclient"]}", args["Codeclient"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["CompteUser"]}", args["CompteUser"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["IDApprov"]}", args["IDApprov"])
                            ))
                    {
                        callback = new Dictionary<string, string> {
                            { "type", "success" }, { "message", "Information enregistrer" }
                        };
                    }
                    else
                    {
                        callback = new Dictionary<string, string> {
                            { "type", "failure" }, { "message", "Enregistrement echouer" }
                        };
                    }
                }
                else
                {
                    callback = new Dictionary<string, string> {
                        { "type", "connection" }, { "message", "Impossible d'acceder à la base de données; vérifier votre connexion" }
                    };
                }
            }
            catch (Exception ex)
            {
                callback = new Dictionary<string, string> {
                        { "type", "failure" }, { "message", "Enregistrement echouer " + ex.Message}
                    };
            }
        }
        public async void update(Dictionary<string, string> args)
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();

                    if (await Apps.Query.updatePrepared(
                            schema.table["TPlanEpargne"],
                             new MySqlParameter($"@{schema.TPlanEpargne["IDEPARG"]}", args["IDEPARG"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["IDdate"]}", args["IDdate"]),
                                 new MySqlParameter($"@{schema.TPlanEpargne["libelle"]}", args["libelle"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["montentdepot"]}", args["montentdepot"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["Montretrait"]}", args["Montretrait"]),
                                new MySqlParameter($"@{schema.TPlanEpargne["Codeclient"]}", args["Codeclient"]),
                                 new MySqlParameter($"@{schema.TPlanEpargne["CompteUser"]}", args["CompteUser"]),
                                 new MySqlParameter($"@{schema.TPlanEpargne["IDApprov"]}", args["IDApprov"])
                            ))
                    {
                        callback = new Dictionary<string, string> {
                            { "type", "success" }, { "message", "Information modifier" }
                        };
                    }
                    else
                    {
                        callback = new Dictionary<string, string> {
                            { "type", "failure" }, { "message", "Modification echouer" }
                        };
                    }
                }
                else
                {
                    callback = new Dictionary<string, string> {
                        { "type", "connection" }, { "message", "Impossible d'acceder à la base de données; vérifier votre connexion" }
                    };
                }
            }
            catch (Exception ex)
            {
                callback = new Dictionary<string, string> {
                        { "type", "failure" }, { "message", "Modification echouer " + ex.Message}
                    };
            }

        }
        public async void delete(Dictionary<string, string> args)
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();

                    if (await Apps.Query.deletePrepared(
                            schema.table["TPlanEpargne"],
                             new MySqlParameter($"@{schema.TPlanEpargne["IDEPARG"]}", args["IDEPARG"])
                           ))
                    {
                        callback = new Dictionary<string, string> {
                            { "type", "success" }, { "message", "Information supprimer" }
                        };
                    }
                    else
                    {
                        callback = new Dictionary<string, string> {
                            { "type", "failure" }, { "message", "Suppression echouer" }
                        };
                    }
                }
                else
                {
                    callback = new Dictionary<string, string> {
                        { "type", "connection" }, { "message", "Impossible d'acceder à la base de données; vérifier votre connexion" }
                    };
                }
            }
            catch (Exception ex)
            {
                callback = new Dictionary<string, string> {
                        { "type", "failure" }, { "message", "Suppression echouer " + ex.Message}
                    };
            }

        }
        // recherche des donnees dans la table 
        public async void reseach(string param)
        {

            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    Apps.Query.getData($"select * from {schema.table["TPlanEpargne"]} where {schema.TPlanEpargne["Codeclient"]} like '%{param}%' order by {schema.TPlanEpargne["IDEPARG"]} ASC;");
                    callback = new Dictionary<string, string> {
                        { "type", "success" }, { "message", "Collecte des données sans soucies" }
                    };
                }
                else
                {
                    callback = new Dictionary<string, string> {
                        { "type", "connection" }, { "message", "Impossible d'acceder à la base de données; vérifier votre connexion" }
                    };
                }
            }
            catch (Exception ex)
            {
                callback = new Dictionary<string, string> {
                        { "type", "failure" }, { "message", "Chargement echouer " + ex.Message}
                    };
            }
        }
        // selecion des donnees dans la table client
        public async void get()
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    Apps.Query.getData($"select * from {schema.table["TPlanEpargne"]}");
                    callback = new Dictionary<string, string> {
                        { "type", "success" }, { "message", "Collecte des données sans soucies" }
                    };
                }
                else
                {
                    callback = new Dictionary<string, string> {
                        { "type", "connection" }, { "message", "Impossible d'acceder à la base de données; vérifier votre connexion" }
                    };
                }
            }
            catch (Exception ex)
            {
                callback = new Dictionary<string, string> {
                        { "type", "failure" }, { "message", "Chargement echouer " + ex.Message}
                    };
            }
        }
    }
}
