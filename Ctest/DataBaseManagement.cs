using System;
using System.Net;
using System.Collections.Generic;
using ArangoDB.Client;
using ArangoDB.Client.Data;
using Ctest.Edges;
using Ctest.Users;
using Ctest.Objs;
using Ctest.Scenes;

namespace Ctest.DataBaseManagement
{
    public static class Ext{

        public static byte[] getOBSc(string url){
            Uri url_to_Reach = new Uri(url);
            using(WebClient client = new WebClient()) {
                byte[] responseArray = client.DownloadData(url_to_Reach);
                //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}",System.Text.Encoding.ASCII.GetString(responseArray));
                return responseArray;
            }   
        }

    }
    class dataBaseManager
    {
        public IArangoDatabase db;
        
        public string urlWithPort;
        public string database;
        public string adminName;
        public string password;

        public dataBaseManager(string urlWithPort,string database, string adminName, string password)
        {
            this.urlWithPort = urlWithPort;
            this.database = database;
            this.adminName = adminName;
            this.password = password;
            this.connect();
            this.db = ArangoDatabase.CreateWithSetting();
        }
        public void connect(){

                ArangoDatabase.ChangeSetting(s =>
                    {
                        s.Database = this.database;
                        s.Url = this.urlWithPort;

                        // you can set other settings if you need
                        s.Credential = new NetworkCredential(this.adminName, this.password);
                        s.SystemDatabaseCredential = new NetworkCredential(this.adminName, this.password);
                    });

                }
        public void createDB(string name){

            this.connect();
            using (var db = ArangoDatabase.CreateWithSetting())
            {
                db.CreateDatabase(name);
                //var currentDatabaseInfo = db.CurrentDatabaseInformation();
                //Console.WriteLine(currentDatabaseInfo.Path);
            }
        }

        public void deleteDB(string name){

            this.connect();
            using (var db = ArangoDatabase.CreateWithSetting())
            {
                db.DropDatabase(name);   
            }
        }

        public void createCollection(string name)
        {
            var createResult = db.CreateCollection(name);
        }

        public void dropCollection(string name)
        {
            var dropResult = db.DropCollection(name);
        }

        public void createEdge(string name)
        {
            var createResult = db.CreateCollection(name,
                    type: CollectionType.Edge);
        }

        public void createGraph(string name)
        {
            var graph = db.Graph(name);
            graph.Create(new List<EdgeDefinitionTypedData>
                {
                    new EdgeDefinitionTypedData
                    {
                        Collection = typeof(Edge_Friend),
                        From = new List<Type> { typeof(User) },
                        To = new List<Type> { typeof(User) }
                    },
                    new EdgeDefinitionTypedData
                    {
                        Collection = typeof(Edge_ownOBJ),
                        From = new List<Type> { typeof(User) },
                        To = new List<Type> { typeof(OBJ) }
                    },
                    new EdgeDefinitionTypedData
                    {
                        Collection = typeof(Edge_ownScene),
                        From = new List<Type> { typeof(User) },
                        To = new List<Type> { typeof(Scene) }
                    },
                    new EdgeDefinitionTypedData
                    {
                        Collection = typeof(Edge_shareOBJ),
                        From = new List<Type> { typeof(OBJ) },
                        To = new List<Type> { typeof(User) }
                    },
                    new EdgeDefinitionTypedData
                    {
                        Collection = typeof(Edge_shareScene),
                        From = new List<Type> { typeof(Scene) },
                        To = new List<Type> { typeof(User) }
                    },
                    new EdgeDefinitionTypedData
                    {
                        Collection = typeof(Edge_isIn),
                        From = new List<Type> { typeof(OBJ) },
                        To = new List<Type> { typeof(Scene) }
                    }
                });
        }
        public List<User> getAllUser(){
            var listUser = this.db.All<User>();
            return listUser.ToList();
        }
        
        public void deleteGraph(string name)
        {
            var graph = db.Graph(name);
            graph.Drop();
        }
    }
}