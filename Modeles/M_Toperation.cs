using Japhet.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Modeles
{
    internal class M_Toperation
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
                               schema.table["toperations"],
                                new MySqlParameter($"@{schema.toperations["Codeclient"]}", args["Codeclient"]),
                                new MySqlParameter($"@{schema.toperations["MontantDepot"]}", args["MontantDepot"]),
                                new MySqlParameter($"@{schema.toperations["MontantRetrait"]}", args["MontantRetrait"]),
                                new MySqlParameter($"@{schema.toperations["IDdate"]}", args["IDdate"]),
                                new MySqlParameter($"@{schema.toperations["libelle"]}", args["libelle"]),
                                new MySqlParameter($"@{schema.toperations["compteUser"]}", args["compteUser"]),
                                new MySqlParameter($"@{schema.toperations["IDApprov"]}", args["IDApprov"]) 
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
                            schema.table["toperations"],
                                new MySqlParameter($"@{schema.toperations["IDoperation"]}", args["IDoperation"]),
                                 new MySqlParameter($"@{schema.toperations["Codeclient"]}", args["Codeclient"]),
                                new MySqlParameter($"@{schema.toperations["MontantDepot"]}", args["MontantDepot"]),
                                new MySqlParameter($"@{schema.toperations["MontantRetrait"]}", args["MontantRetrait"]),
                                new MySqlParameter($"@{schema.toperations["IDdate"]}", args["IDdate"]),
                                new MySqlParameter($"@{schema.toperations["libelle"]}", args["libelle"]),
                                new MySqlParameter($"@{schema.toperations["compteUser"]}", args["compteUser"]),
                                 new MySqlParameter($"@{schema.toperations["IDApprov"]}", args["IDApprov"])
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
                            schema.table["toperations"],
                                new MySqlParameter($"@{schema.toperations["IDoperation"]}", args["IDoperation"])
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
                    Apps.Query.getData($"select noms from {schema.table["customes"]} where {schema.customes["noms"]} like '%{param}%';");
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
        public async void get(string param)
        {
            try
            {
                if (await Apps.Query.Open())
                {
                    Apps.Schema schema = new Apps.Schema();
                    Apps.Query.getData($"SELECT Codeclient,IDoperation,date_format(dateexercice.DateExec,'%d-%m-%Y') as DateExec,libelle,MontantDepot,MontantRetrait FROM toperations,dateexercice WHERE toperations.IDdate=dateexercice.IDdate AND Codeclient='" + param+"' order by IDoperation DESC");
                    //Apps.Query.getData($"select * from {schema.table["toperations"]} where Codeclient='" + param+"'");
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
        //public async void getApprov(string param)
        //{
        //    try
        //    {
        //        if (await Apps.Query.Open())
        //        {
        //            Apps.Schema schema = new Apps.Schema();
        //            Apps.Query.getData($"select  approvcaisse.compteCaisse,approvcaisse.IDdate,approvcaisse.compteUser,sum((ApproVEntree)-(ApprovSortie)) as DISPONIBLE FROM approvcaisse,caisse,dateexercice,usedb WHERE caisse.compteCaisse=approvcaisse.compteCaisse AND dateexercice.IDdate=approvcaisse.IDdate AND usedb.compteUser='" + param + "' AND dateexercice.IDdate='" + DateSession.LireSession()+"'");
        //            //Apps.Query.getData($"select * from {schema.table["toperations"]} where Codeclient='" + param+"'");
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
        //               { "type", "failure" }, { "message", "Chargement echouer " + ex.Message}
        //        };
        //    }
        //}
    }
}
