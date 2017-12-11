
using System;
using System.IO;
using Ctest.Objs;

namespace Ctest.BinaryManager
{
    public class BinaryManager
    {
        public BinaryWriter bw;
        public BinaryReader br;
        int i = 25;
        double d = 3.14157;
        bool b = true;
        string s = "I am happy";
        
        public void createAndWriteFile(string name, string location,OBJ obj)
        {
            try {
                bw = new BinaryWriter(output: new FileStream(path: name, mode: FileMode.Create));
            } catch (IOException e) {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }

            //writing into the file
            try {
                // Understand the function Write.
                //bw.Write(obj);
            } catch (IOException e) {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;
            }
            bw.Close();
        }

        public void readFile()
        {
            //reading from the file
            try {
                br = new BinaryReader(new FileStream(path: "mydata", mode: FileMode.Open));
            } catch (IOException e) {
                Console.WriteLine(e.Message + "\n Cannot open file.");
                return;
            }
            
            try {
                i = br.ReadInt32();
                Console.WriteLine("Integer data: {0}", i);
                d = br.ReadDouble();
                Console.WriteLine("Double data: {0}", d);
                b = br.ReadBoolean();
                Console.WriteLine("Boolean data: {0}", b);
                s = br.ReadString();
                Console.WriteLine("String data: {0}", s);
            } catch (IOException e) {
                Console.WriteLine(e.Message + "\n Cannot read from file.");
                return;
            }
            br.Close();
            Console.ReadKey();
        }
    }

}