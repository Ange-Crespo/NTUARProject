using System.Collections.Generic;
using ArangoDB.Client;
using Ctest.DataBaseManagement;

namespace Ctest.Objs

{
    public class OBJ
    {
        [DocumentProperty(Identifier = IdentifierType.Handle)]
        public string id {get; set;}

        public string name { get; set; }

        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string address {get; set;}

        public bool isPublic {get; set;}

        public OBJ()
        {
        
        }
        public OBJ(string name,string address,bool isPublic = true)
        {
            this.name = name;
            this.address = address;
            this.isPublic = isPublic;
        }
        //Create a user Instance in C# with an id getting it in the database
        public OBJ(string id)
        {
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            
            this.id = id;
            this.name = Data.db.Document<OBJ>(id).name;
            this.address = Data.db.Document<OBJ>(id).address;
            this.isPublic = Data.db.Document<OBJ>(id).isPublic;
        }

        public OBJ(string Key,string Key2, int t)
        {
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            
            this.address = Key;
            this.name = Data.db.Document<OBJ>(Key).name;
            this.id = Data.db.Document<OBJ>(Key).id;
            this.isPublic = Data.db.Document<OBJ>(Key).isPublic;
        }

        public List<byte[]> getFile(bool isTextured = false)
    {   
        var list = new List<byte[]>();
        //Address http://localhost:8529/_db/test/test/provide-binary-file?path=/home/crespo/Bureau/db/OBJ/id/obj.OBJ
        byte[] data_OBJ = DataBaseManagement.Ext.getOBSc(Constants.path_OBJ+"/"+this.address+"/obj."+Constants.typeOBJ);
        list.Add(data_OBJ);
        
        if(isTextured){
            //Address http://localhost:8529/_db/test/test/provide-binary-file?path=/home/crespo/Bureau/db/OBJ/id/texture.UNKNOWN
            byte[] data_Texture = DataBaseManagement.Ext.getOBSc(Constants.path_OBJ+"/"+this.address+"/texture."+Constants.typeTexture);
            list.Add(data_Texture);
        }
        return list;
    }

        public void SaveInDB()
        {
            //You cannot modify OBJ with SaveInDB cause it will raise a violed constraint Should use Upsert but does not be implemented yet
            dataBaseManager Data = new dataBaseManager(Constants.urlWithPort,Constants.database,Constants.adminName,Constants.password);
            Data.db.Insert<OBJ>(this);
        }

         public override string ToString()
        {
            string display = name?.ToString() + "\n" + id?.ToString() + "\n" + address?.ToString()+ "\n" + isPublic.ToString();
            return display;
        }
        //Make an Obj thanks to the address
        public OBJ(string address, int t)
        {
            
        }
    }
}