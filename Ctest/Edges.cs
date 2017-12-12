using System;
using ArangoDB.Client;
using Ctest.DataBaseManagement;
using Ctest.Objs;
using Ctest.Scenes;
using Ctest.Users;

namespace Ctest.Edges
{
    public class Edge
{
    [DocumentProperty(Identifier = IdentifierType.Handle)]
    public string id {get; set;}

    [DocumentProperty(Identifier = IdentifierType.Key)]
    public string Key {get; set;}
    
    [DocumentProperty(Identifier = IdentifierType.EdgeFrom)]
    public string from {get; set;}


    [DocumentProperty(Identifier = IdentifierType.EdgeTo)]
    public string to {get; set;}

    public DateTime CreatedDate {get; set;}


    //public string type {get; set;}
    
    public Edge() 
    { 

    }

    //Usefull if you use FILTER definition of Edge
    
    public Edge(string id1, string id2)
    {
        this.from = id1;
        this.to = id2; 
        CreatedDate = DateTime.Now;  
        
    }
    //Usefull if you use FILTER definition of Edge
    /* 
    public Edge(User Friend1, User Friend2)
    : this( Friend1.id, Friend2.id )
    {
        this.type = "friend";
    }

    public Edge(User user, OBJ obj)
    : this( user.id, obj.id)
    {
        this.type = "ownOBJ";
    }

    public Edge(User user, Scene scene)
    : this( user.id, scene.id)
    {
        this.type = "ownScene";
    }

    public Edge(OBJ obj, User user)
    : this ( obj.id, user.id )
    {
        this.type = "isSharedOBJ";
    }

    public Edge(Scene scene, User user)
    : this( scene.id, user.id )
    {
        this.type = "isSharedScene";
    }

    public Edge(OBJ obj, Scene scene)
    : this( obj.id,scene.id )
    {
        this.type = "isIn";
    }*/ 

}

public class Edge_Friend : Edge
{
    public Edge_Friend()
    {

    }

    public Edge_Friend(User user1, User user2):base(user1.id,user2.id)
    {
        this.SaveInDB(); 
    }

    public void SaveInDB()
    {
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        Data.db.Insert<Edge_Friend>(this);
    }
}

public class Edge_ownOBJ : Edge
{
    public Edge_ownOBJ()
    {

    }

    public Edge_ownOBJ(User user1, OBJ obj):base(user1.id,obj.id)
    {
        this.SaveInDB(); 
    }

    public void SaveInDB()
    {
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        Data.db.Insert<Edge_ownOBJ>(this);
    }
}

public class Edge_ownScene : Edge
{
    public Edge_ownScene()
    {

    }

    public Edge_ownScene(User user1, Scene scene):base(user1.id,scene.id)
    {
        this.SaveInDB(); 
    }

    public void SaveInDB()
    {
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        Data.db.Insert<Edge_ownScene>(this);
    }
}

public class Edge_shareOBJ : Edge
{
    public Edge_shareOBJ()
    {

    }

    public Edge_shareOBJ(OBJ obj, User user):base(obj.id,user.id)
    {
        this.SaveInDB(); 
    }

    public void SaveInDB()
    {
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        Data.db.Insert<Edge_shareOBJ>(this);
    }
}

public class Edge_shareScene : Edge
{
    public Edge_shareScene()
    {

    }

    public Edge_shareScene(Scene scene, User user):base(scene.id,user.id)
    {
        this.SaveInDB(); 
    }

    public void SaveInDB()
    {
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        Data.db.Insert<Edge_shareScene>(this);
    }
}

public class Edge_isIn : Edge
{
    public Edge_isIn()
    {

    }

    public Edge_isIn(OBJ obj, Scene scene):base(obj.id,scene.id)
    {
        this.SaveInDB(); 
    }

    public void SaveInDB()
    {
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        Data.db.Insert<Edge_isIn>(this);
    }
}



}
