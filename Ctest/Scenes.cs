using ArangoDB.Client;
using Ctest.DataBaseManagement;

namespace Ctest.Scenes
{
    public class Scene{

    [DocumentProperty(Identifier = IdentifierType.Handle)]
    public string id {get; set;}

    public string name { get; set; }
    public string address { get; set; }

    [DocumentProperty(Identifier = IdentifierType.Key)]
    public string Key{ get; set; }

    public bool isPublic{ get; set;}

    public Scene()
    {
      
    }
    public Scene(string name,string address, bool isPublic = true)
    {
        this.name = name;
        this.address = address;
        this.isPublic = isPublic;
    }
    //Create a user Instance in C# with an id getting it in the database
    public Scene(string id)
    {
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        
        this.id = id;
        this.name = Data.db.Document<Scene>(id).name;
        this.Key = Data.db.Document<Scene>(id).Key;
        this.address = Data.db.Document<Scene>(id).address;
        this.isPublic = Data.db.Document<Scene>(id).isPublic;
    }

    public Scene(string Key,int Key2)
    {
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        
        this.Key= Key;
        this.name = Data.db.Document<Scene>(Key).name;
        this.id = Data.db.Document<Scene>(Key).id;
        this.address = Data.db.Document<Scene>(Key).address;
        this.isPublic = Data.db.Document<Scene>(Key).isPublic;
    }

    public byte[] getFile()
    {
        //Address http://localhost:8529/_db/test/test/provide-binary-file?path=/home/crespo/Bureau/db/Scene/id.bundle
        return DataBaseManagement.Ext.getOBSc(Constants.path_Scene+this.Key+"."+Constants.typeScene);
    }
    //Save the modified user in DataBase if already exist : update if not : create.
    public void SaveInDB()
    {
        //You cannot modify Scenes with SaveInDB cause it will raise a violed constraint Should use Upsert but does not be implemented yet
        dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
        Data.db.Insert<Scene>(this);
    }

    public override string ToString()
        {
            string display = name?.ToString() + "\n" + id?.ToString() + "\n" + Key?.ToString() + "\n" + isPublic.ToString();
            return display;
        }
}


}