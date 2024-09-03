using Japhet.Controleurs;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Japhet.Modeles
{
    internal class M_Descpese
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
                                schema.table["DescPese"],
                                new MySqlParameter($"@{schema.DescPese["numerateur"]}", args["numerateur"]),
                                new MySqlParameter($"@{schema.DescPese["indice"]}", args["indice"]),
                                 new MySqlParameter($"@{schema.DescPese["pourcentage"]}", args["pourcentage"]),
                                new MySqlParameter($"@{schema.DescPese["PU"]}", args["PU"]),
                                 new MySqlParameter($"@{schema.DescPese["poids"]}", args["poids"]), 
                                new MySqlParameter($"@{schema.DescPese["NumPese"]}", args["NumPese"]),
                                new MySqlParameter($"@{schema.DescPese["total"]}", args["total"]),
                                new MySqlParameter($"@{schema.DescPese["reference"]}", args["reference"]),
                                new MySqlParameter($"@{schema.DescPese["bourse"]}", args["bourse"]),
                                new MySqlParameter($"@{schema.DescPese["teneur"]}", args["teneur"])
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
                              schema.table["DescPese"],
                                new MySqlParameter($"@{schema.DescPese["IdDesc"]}", args["IdDesc"]),
                                new MySqlParameter($"@{schema.DescPese["numerateur"]}", args["numerateur"]),
                                new MySqlParameter($"@{schema.DescPese["indice"]}", args["indice"]),
                                 new MySqlParameter($"@{schema.DescPese["pourcentage"]}", args["pourcentage"]),
                                new MySqlParameter($"@{schema.DescPese["PU"]}", args["PU"]),
                                 new MySqlParameter($"@{schema.DescPese["poids"]}", args["poids"]),
                                 new MySqlParameter($"@{schema.DescPese["reference"]}", args["reference"]),
                                 new MySqlParameter($"@{schema.DescPese["bourse"]}", args["bourse"]),
                                 new MySqlParameter($"@{schema.DescPese["teneur"]}", args["teneur"])
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
                             schema.table["DescPese"],
                                new MySqlParameter($"@{schema.DescPese["IdDesc"]}", args["IdDesc"])
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
                    Apps.Query.getData($"select * from {schema.table["pesage"]} where {schema.pesage["Codeclient"]} like '%{param}%' order by {schema.pesage["NumPese"]} DESC;");
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
                    Apps.Query.getData($"SELECT pesage.NumPese, date_format(dateexercice.DateExec,'%d-%m-%Y') as dateex,customes.noms,mention FROM customes,pesage,dateexercice where customes.Codeclient=pesage.Codeclient AND pesage.IDdate=dateexercice.IDdate");
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


        public async void getdescpese(int param)
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    Apps.Query.getData($"select descpese.IdDesc, customes.Codeclient as compte,customes.noms as intitule, mention, reference,numerateur,indice,pourcentage,PU,poids,PT,total FROM customes,descpese,pesage WHERE customes.Codeclient=pesage.Codeclient AND pesage.NumPese=descpese.NumPese AND descpese.NumPese='{param}'");
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
