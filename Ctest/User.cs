using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ArangoDB.Client;
using Ctest.DataBaseManagement;
using Ctest.Edges;
using Ctest.Objs;
using Ctest.Scenes;


namespace Ctest.Users
{
    public static class Ext{
        public static void upgrade(string url, string path){
                Console.WriteLine(url);
                Console.WriteLine(path);
                using(WebClient client = new WebClient()) {
                            byte[] data = File.ReadAllBytes(path);
                            Uri url_to_Reach = new Uri(url);
                            byte[] responseArray = client.UploadData(url_to_Reach,data);
                            //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}",System.Text.Encoding.ASCII.GetString(responseArray));
                }
        }
        
        public static void upgrade(string url, byte[] data){
                using(WebClient client = new WebClient()) {
                            //byte[] data = File.ReadAllBytes("/home/crespo/Bureau/db/lol.obj");
                            Uri url_to_Reach = new Uri(url);
                            byte[] responseArray = client.UploadData(url_to_Reach,data);
                            //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}",System.Text.Encoding.ASCII.GetString(responseArray));
                }
        }
    }
    public class User{
        [DocumentProperty(Identifier = IdentifierType.Handle)]
        public string id {get; set;}

        public string name { get; set; }

        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string emailAddress { get; set; }

        public string password { get; set; }
        public int age { get; set; }

        public User()
        {
        
        }
        public User(string name,string emailAddress, string password, int age)
        {
            this.name = name;
            this.emailAddress = emailAddress;
            this.password = password;
            this.age = age;  
        }
        //Create a user Instance in C# with an id getting it in the database
        public User(string id)
        {
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            
            this.id = id;
            this.name = Data.db.Document<User>(id).name;
            this.emailAddress = Data.db.Document<User>(id).emailAddress;
            this.age = Data.db.Document<User>(id).age;
            this.password = Data.db.Document<User>(id).password;
        }

        public User(string Key,string Key2)
        {
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            
            this.emailAddress = Key;
            this.name = Data.db.Document<User>(Key).name;
            this.id = Data.db.Document<User>(Key).id;
            this.age = Data.db.Document<User>(Key).age;
            this.password = Data.db.Document<User>(Key).password;
        }

        public void addFriend(User Friend)
        {
            Edge_Friend edge = new Edge_Friend(this,Friend);
        }

        public void shareOBJWith(OBJ obj,User Friend)
        {
            Edge_shareOBJ edge = new Edge_shareOBJ(obj,Friend);
        }

        public void shareSceneWith(Scene scene,User Friend)
        {
            Edge_shareScene edge = new Edge_shareScene(scene,Friend);
        }

        public void shareIsIn(OBJ obj,Scene scene)
        {
            Edge_isIn edge = new Edge_isIn(obj,scene);
        }

        public void addOBJ(OBJ obj,string path_obj,bool isTextured=false, string path_texture="")
        {
            //Address type : "http://localhost:8529/_db/test/test/receive-binary?path=/home/crespo/Bureau/db/OBJ/1234/&id=obj&type=obj"
            string url_obj = Constants.url_POST+"?path="+Constants.path_OBJ+obj.address+"/&id=obj"+"&type="+Constants.typeOBJ;
            Ext.upgrade(url_obj, path_obj);
            if (isTextured){
                //Address type : "http://localhost:8529/_db/test/test/receive-binary?path=/home/crespo/Bureau/db/OBJ/1234/&id=texture&type=obj"
                string url_texture = Constants.url_POST+"?path="+Constants.path_OBJ+obj.address+"/&id=texture"+"&type="+Constants.typeOBJ;
                Ext.upgrade(url_texture, path_texture);
            }
        }
        public void addOBJ(OBJ obj,byte[] data_obj,bool isTextured=false, byte[] data_texture = null)
        {
            //Address type : "http://localhost:8529/_db/test/test/receive-binary?path=/home/crespo/Bureau/db/OBJ/1234/&id=obj&type=obj"
            string url_obj = Constants.url_POST+"?path="+Constants.path_OBJ+obj.address+"/&id=obj"+"&type="+Constants.typeOBJ;
            Ext.upgrade(url_obj, data_obj);
            if(isTextured){
                //Address type : "http://localhost:8529/_db/test/test/receive-binary?path=/home/crespo/Bureau/db/OBJ/1234/&id=texture&type=obj"
                string url_texture = Constants.url_POST+"?path="+Constants.path_OBJ+obj.address+"/&id=texture"+"&type="+Constants.typeTexture;
                Ext.upgrade(url_texture, data_texture);
            }
        }
        public void addScene(Scene scene,string path)
        {   
            //Address type : //Address type : "http://localhost:8529/_db/test/test/receive-binary?path=/home/crespo/Bureau/db/scene/&id=1234&type=bundle"
            string url = Constants.url_POST+"?path="+Constants.path_Scene+"&id="+scene.Key+"&type="+Constants.typeScene;
            Ext.upgrade(url, path);
        }
        public void addScene(Scene scene,byte[] data)
        {   
            //Address type : //Address type : "http://localhost:8529/_db/test/test/receive-binary?path=/home/crespo/Bureau/db/scene/&id=1234&type=bundle"
            string url = Constants.url_POST+"?path="+Constants.path_Scene+"&id="+scene.Key+"&type="+Constants.typeScene;
            Ext.upgrade(url, data);
        }
        //Save the modified user in DataBase if already exist : update if not : create.
        public void SaveInDB()
        {
            //You cannot modify User with SaveInDB cause it will raise a violed constraint Should use Upsert but does not be implemented yet
            //After implemented with upsert the issue is to not erase a user when an other want to use the same email address !!!
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            Data.db.Insert<User>(this);
            this.id=Data.db.Document<User>(this.emailAddress).id;
        }

        public List<User> getFriend()
        {
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            var list = new List<User>();
            var result = Data.db.Traverse<User, Edge_Friend>(new TraversalConfig
            {
                StartVertex = this.id,
                GraphName = Constants.GraphName,
                Direction = EdgeDirection.Any,
                MaxDepth = 1,
            });
           
            list = result.Visited.Vertices; // List of the user visited first one is the user himself!
            list.RemoveAt(0);
            return list;
        }
        
        /*public List<User> getMyOBJ()
        {
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            var list = new List<OBJ>();
            var result = Data.db.Traverse<OBJ, Edge_ownOBJ>(new TraversalConfig
            {
                StartVertex = this.id,
                GraphName = Constants.GraphName,
                Direction = EdgeDirection.Outbound,
                MaxDepth = 1,
            });
           
            list = result.Visited.Vertices; // List of the user visited first one is the user himself!
            list.RemoveAt(0);
            return list;
        }*/


        public override string ToString()
        {
                return name?.ToString();
        }
    }
    //Change Age Data.db.Query().Update(_ => new { Age = 25 }, _ => this.emailAddress).In<User>().Execute();
}