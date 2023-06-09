using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tema_2_Notite.Model;

namespace Tema_2_Notite.Resources
{
    class SerializationActions
    {
        public ObservableCollection<DataBase> DataBases;
        public string FileName;

        public SerializationActions(ObservableCollection<DataBase> other)
        {
            this.DataBases = other;
        }

        public void SerializeObject(ObjectToSerialize entity)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(ObjectToSerialize));
            FileStream fileStr = new FileStream("DateOut.xml", FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }

        public void SerializeObject(ObjectToSerialize entity, string fileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(ObjectToSerialize));
            FileStream fileStr = new FileStream(fileName, FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }

        public void DeserializeObject()
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(ObjectToSerialize));
            using (FileStream file = new FileStream("DateOut.xml", FileMode.Open))
            {
                var obj = (ObjectToSerialize)xmlser.Deserialize(file);
                FileName = obj.LastOpenedDatabaseName;
                DataBases.Clear();
                foreach (var database in obj.DataBases)
                {
                    DataBases.Add(database);
                }
            }
        }

        public ObservableCollection<DataBase> DeserializeObject(string fileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(ObjectToSerialize));
            FileStream file = new FileStream(fileName, FileMode.Open);
            var elem_salvate = (xmlser.Deserialize(file) as ObjectToSerialize).DataBases;
            file.Dispose();
            return new ObservableCollection<DataBase>(elem_salvate);
        }


    }
}
