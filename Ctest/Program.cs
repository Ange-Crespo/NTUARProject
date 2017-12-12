using System;
using System.IO;
using System.Net;
using ArangoDB.Client;
using Ctest.DataBaseManagement;
using Ctest.Edges;
using Ctest.Objs;
using Ctest.Scenes;
using Ctest.Users;
using System.Linq;
//using NUnit.Framework;

namespace Ctest
{
    static class Constants
    {
        public static string database = "App";
        public static string urlWithPort = "http://193.70.84.67:8529";
        public static string url_POST = "http://193.70.84.67:8529/_db/"+Constants.database+"/BinaryGestion/receive-binary";
        public static string url_GET = "http://193.70.84.67:8529/_db/"+Constants.database+"/BinaryGestion/provide-binary-file";
        public static string path_OBJ= "/home/ubuntu/App/db/OBJ/";
        public static string path_Scene= "/home/ubuntu/App/db/Scene/";
        public static string init_database = "_system";
        
        public static string adminName = "root";
        public static string password = "azertyuiop123";   
        public static string typeScene = "UNKNOWN";
        public static string typeOBJ = "obj"; 

        public static string typeTexture = "UNKNOWN";   

        public static string path_obj_Default = "/home/crespo/Bureau/db/cube.obj";

        public static string GraphName = "App_Graph";

        public static string UserCollectionName = "User";
        public static string OBJCollectionName = "OBJ";
        public static string SceneCollectionName = "Scene";

        public static string Edge_Friend_Name = "Edge_Friend";
        public static string Edge_isIn_Name = "Edge_isIn";
        public static string Edge_ownOBJ_Name = "Edge_ownOBJ";
        public static string Edge_ownScene_Name = "Edge_ownScene";
        public static string Edge_shareOBJ_Name = "Edge_shareOBJ";
        public static string Edge_shareScene_Name = "Edge_shareScene";
    }

    class Program
    {
        public static void init()
        {
            //dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            //Data.init(); // Add test in Data
            User Ange = new User("Ange","ange.crespo@hotmail.fr","1234",23);
            User Mathieu = new User("Mathieu","mathieu.crespo@hotmail.fr","1234",19);
            User Pauline = new User("Pauline","pauline.crespo@hotmail.fr","1234",22);
            OBJ objet1 = new OBJ("Monobjet","1",true);
            OBJ objet2 = new OBJ("My Object2","2",true);
            OBJ objet3 = new OBJ("My Object4","3",true);
            Scene scene1 = new Scene("My Scene1","1",true);
            Scene scene2 = new Scene("My Scene2","2",true );
            Scene scene3 = new Scene("My Scene3","3",true);
            Ange.SaveInDB();
            Mathieu.SaveInDB();
            Pauline.SaveInDB();
            objet1.SaveInDB();
            objet2.SaveInDB();
            objet3.SaveInDB();
            scene1.SaveInDB();
            scene2.SaveInDB();
            scene3.SaveInDB();

            Edge_Friend friend1 = new Edge_Friend(Ange,Mathieu);
            Edge_Friend friend2 = new Edge_Friend(Mathieu,Pauline);
            Edge_Friend friend3 = new Edge_Friend(Pauline,Ange);

            Edge_isIn isIn = new Edge_isIn(objet1,scene1);
            
            Edge_isIn isIn2 = new Edge_isIn(objet2,scene2);

            Edge_isIn isIn3 = new Edge_isIn(objet3,scene2);
            Edge_isIn isIn4 = new Edge_isIn(objet3,scene3);

            Edge_ownOBJ ownO1 = new Edge_ownOBJ(Ange,objet1);
            Edge_ownScene ownS1 = new Edge_ownScene(Ange,scene1);

            friend1.SaveInDB();
            friend2.SaveInDB();
            friend3.SaveInDB();

            isIn.SaveInDB();
            isIn2.SaveInDB();
            isIn3.SaveInDB();
            isIn4.SaveInDB();

            ownO1.SaveInDB();
            ownS1.SaveInDB();


            //TODO : FINISH IT



        }
        static void Main(string[] args)
        {
            //Call the database
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            
            //Data.db.CreateDatabase("test");
            User Ange = new User("ange.crespo@hotmail.fr");
           
            //OBJ objet1 = new OBJ("1");
            Scene objet1 = new Scene("1");

            Ange.addScene(objet1,"/home/crespo/Bureau/CTest/NTUARProject/Ctest/Serverside/ServiceBinary/cube.obj");
             
            objet1.getFile();

            //Add a User in the database.
            /*var person = new User
            {
                name = "BoB",
                emailAddress = "Bob.crespo@hotmail.fr",
                password = "1234",
                age = 24,
                
            };
            
            Data.db.Insert<User>(person);
            Console.WriteLine(person.id);*/
            
            //Create a collection
            //var createResult = Data.db.CreateCollection("User");
            //Console.WriteLine(createResult);
            
            //Add user
            //User ange = new User("Ange","ange.crespo@hotmail.fr","password",23);
            
            //Save user
            //ange.SaveInDB();

            //Create a Edges 

            /*Data.createEdge("Edge_Friend");
            Data.createEdge("Edge_ownOBJ");
            Data.createEdge("Edge_ownScene");
            Data.createEdge("Edge_shareOBJ");
            Data.createEdge("Edge_shareScene");
            Data.createEdge("Edge_isIn"); */

            //Create Graph

            //Data.createGraph("test");

            //DeleteGraph

            //Data.deleteGraph("test");
            
           
            // User Bob = new User("Bob.crespo@hotmail.fr","r");
            //Scene scn = new Scene("ma Scene","test");
            //scn.SaveInDB();
            
            //Console.WriteLine(obj2);
            //Edge_Friend egde = new Edge_Friend(ange,Bob);
            //edge.SaveInDB();
            //curl -X POST --data "@cube.obj" "http://localhost:8529/_db/test/test/receive-binary?path=/home/crespo/Bureau/db/try&id=test&type=obj"
            //byte[] data = File.ReadAllBytes();
            //var test = obj.getFile()[0];
            
            //Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}",System.Text.Encoding.ASCII.GetString(test));
            
            // TODO : TEST.CS TO RUN TEST IF TIME.

            
            /*var cursor = Data.db.All<User>();
            var loadedUsers = cursor.ToList();
            Console.Write(loadedUsers[1]);*/
            /*var result = Data.db.Traverse<User, Edge_Friend>(new TraversalConfig
            {
                StartVertex = ange.id,
                GraphName = "App",
                Direction = EdgeDirection.Any,
                MaxDepth = 1,
            });*/
            
            /*var result = Data.db.Traverse<User,Edge_Friend>(new TraversalConfig
            {
                StartVertex = ange.id,
                GraphName = Constants.GraphName,
                
                Direction = EdgeDirection.Any,
                MaxDepth = 1,
            });*/
            /* 
            User ange = new User("ange.crespo@hotmail.fr","r");
            OBJ obj = new OBJ("test","caca",2);
            Console.WriteLine(Data.db.ListCollections()[0]);
            var result = Data.db.Query().Traversal<User, Edge_Friend>(ange.id);

            var result2=result.Depth(1,1);
            var result3=result2.OutBound();
            //var result4=result3.Edge(Data.db.NameOf<Edge_Friend>(), EdgeDirection.Any);
            var result5 = result3.Graph(Constants.GraphName);
            var result6=result5.Select(g => g);
            var result7=result6.ToList();
           
            //Console.WriteLine(result5.ToList());
           foreach (var i in result7){
               Console.WriteLine(i);
           }
            Console.WriteLine(Data.db.NameOf<Edge_Friend>());
            Console.WriteLine("Hello World!");
        */
        }
        
    }
}



/*curl -X POST -u root:azertyuiop123 --data-binary @- --dump - http://localhost:8529/_db/test/_api/traversal <<EOF
{ 
  "startVertex" : "User/ange.crespo@hotmail.fr", 
  "graphName" : "App", 
  "direction" : "outbound",
  "filter": "function (config, vertex, path) {if (vertex.name !== 'Bob') { return 'exclude';}}"
}
EOF

curl -X POST -u root:azertyuiop123 --data-binary @- --dump - http://localhost:8529/_open/auth <<EOF
{"username":"root","password":"azertyuiop123"}
EOF*/

