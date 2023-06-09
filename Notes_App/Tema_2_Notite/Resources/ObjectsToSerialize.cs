using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tema_2_Notite.Model;

namespace Tema_2_Notite.Resources
{
    [Serializable]
    public class ObjectToSerialize
    {
        [XmlArray]
        public ObservableCollection<DataBase> DataBases { get; set; }

        public string LastOpenedDatabaseName { get; set; }

        public ObjectToSerialize()
        {
            DataBases = new ObservableCollection<DataBase>();
        }
    }
}
