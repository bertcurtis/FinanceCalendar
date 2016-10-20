using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace Finance_Calendar
{
    public class SaveAll
    {

        private static void Writer(string ext, object obj, string filename)
        {
            XmlSerializer sr = new XmlSerializer(obj.GetType());

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + ext;
            Directory.CreateDirectory(path);
            var file = Path.Combine(path, filename);
            //FileStream overwrite = File.Create(path);
            TextWriter writer = new StreamWriter(file);

            sr.Serialize(writer, obj);
            writer.Dispose();
            writer.Close();
        }
        public static void SaveData(object obj, string filename)
        {
            Writer("\\Calendar Finance XML", obj, filename);
        }

    }
}
