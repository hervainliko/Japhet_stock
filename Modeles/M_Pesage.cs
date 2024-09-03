using Japhet.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Japhet.Modeles
{
    internal class M_Pesage
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
                                schema.table["pesage"],
                                 new MySqlParameter($"@{schema.pesage["NumPese"]}", args["NumPese"]),
                                new MySqlParameter($"@{schema.pesage["IDdate"]}", args["IDdate"]),
                                 new MySqlParameter($"@{schema.pesage["Codeclient"]}", args["Codeclient"]),
                                new MySqlParameter($"@{schema.pesage["CompteUser"]}", args["CompteUser"]),
                                new MySqlParameter($"@{schema.pesage["total"]}", args["total"]),
                                new MySqlParameter($"@{schema.pesage["mention"]}", args["mention"])
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
                              schema.table["pesage"],
                                new MySqlParameter($"@{schema.pesage["NumPese"]}", args["NumPese"]),
                                new MySqlParameter($"@{schema.pesage["IDdate"]}", args["IDdate"]),
                                 new MySqlParameter($"@{schema.pesage["Codeclient"]}", args["Codeclient"]),
                                new MySqlParameter($"@{schema.pesage["CompteUser"]}", args["CompteUser"])
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




        // Modification de le mention 

        public async void updateMention(Dictionary<string, string> args)
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();

                    if (await Apps.Query.updatePrepared(
                              schema.table["pesage"],
                                new MySqlParameter($"@{schema.pesage["NumPese"]}", args["NumPese"]),
                                new MySqlParameter($"@{schema.pesage["mention"]}", args["mention"])
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
                             schema.table["pesage"],
                                new MySqlParameter($"@{schema.pesage["NumPese"]}", args["NumPese"])
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
        //public async void get()
        //{
        //    try
        //    {
        //        if (await Apps.Query.Open())
        //        {
        //            Apps.Schema schema = new Apps.Schema();
        //            Apps.Query.getData($"select * from {schema.table["pesage"]}");
        //            callback = new Dictionary<string, string> {
        //                { "type", "success" }, { "message", "Collecte des données sans soucies" }
        //            };
        //        }
        //        else
        //        {
        //            callback = new Dictionary<string, string> {
        //                { "type", "connection" }, { "message", "Impossible d'acceder à la base de données; vérifier votre connexion" }
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        callback = new Dictionary<string, string> {
        //                { "type", "failure" }, { "message", "Chargement echouer " + ex.Message}
        //            };
        //    }
        //}
    }
}
