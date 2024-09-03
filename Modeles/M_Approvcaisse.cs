using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Japhet.Modeles
{
    internal class M_Approvcaisse
    {
        public Dictionary<string, string> callback;
        // dictionnaire d'insertion dans la base de donnees 
        public async void insert(Dictionary<string, string> args)
        {
    
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    if (await Apps.Query.insertPrepared(
                                schema.table["approvcaisse"],
                                new MySqlParameter($"@{schema.approvcaisse["Montant"]}", args["Montant"]),
                                new MySqlParameter($"@{schema.approvcaisse["compteUser"]}", args["compteUser"]),
                                new MySqlParameter($"@{schema.approvcaisse["compteCaisse"]}", args["compteCaisse"]),
                                new MySqlParameter($"@{schema.approvcaisse["IDdate"]}", args["IDdate"])

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
                            schema.table["approvcaisse"],
                                new MySqlParameter($"@{schema.approvcaisse["IDApprov"]}", args["IDApprov"]),
                                new MySqlParameter($"@{schema.approvcaisse["Montant"]}", args["Montant"]),
                                new MySqlParameter($"@{schema.approvcaisse["compteUser"]}", args["compteUser"]),
                                new MySqlParameter($"@{schema.approvcaisse["compteCaisse"]}", args["compteCaisse"]),
                                new MySqlParameter($"@{schema.approvcaisse["IDdate"]}", args["IDdate"])
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
                               schema.table["approvcaisse"],
                                new MySqlParameter($"@{schema.approvcaisse["IDApprov"]}", args["IDApprov"])
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
        public async void getcaisse()
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    Apps.Query.getData($"select * from approvcaisse order by IDApprov DESC limit 1");
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
        // recherche des donnees dans la table 
        public async void reseach(string param)
        {

            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    Apps.Query.getData($"select * from {schema.table["approvcaisse"]} where {schema.approvcaisse["dateA"]} like '%{param}%' order by {schema.approvcaisse["IDApprov"]} DESC;");
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
                    Apps.Query.getData($" select IDApprov,Montant,date_format(DateExec,\"%d-%m-%Y\") as DateExec, intituleCaisse from approvcaisse,dateexercice,caisse,usedb where approvcaisse.compteCaisse=caisse.compteCaisse and approvcaisse.IDdate=dateexercice.IDdate  and approvcaisse.compteUser=usedb.compteUser ");
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
        // Récupération de l'approvisionnement du client en ligne 
        public async void getIdAndSomeAppforUserConnected(string param)
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    Apps.Query.getData($"select IDApprov,ApproVEntree from {schema.table["approvcaisse"]},{schema.table["usedb"]} where {schema.approvcaisse["dateA"]}=CURDATE() AND {schema.table["approvcaisse"]}.{schema.approvcaisse["CompteUser"]}={schema.table["usedb"]}.{schema.usedb["CompteUser"]} AND {schema.table["usedb"]}.{schema.usedb["CompteUser"]}='{param}'");
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
