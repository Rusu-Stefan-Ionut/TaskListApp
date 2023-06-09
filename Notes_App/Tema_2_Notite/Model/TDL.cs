using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tema_2_Notite.Resources;

namespace Tema_2_Notite.Model
{
    [Serializable]
    public class TDL: BaseNotification
    {
        private string name;
        private string picture;
        private ObservableCollection<Task> taskList;
        private ObservableCollection<TDL> subTDL;

        [XmlElement]
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        [XmlElement]
        public string Picture
        {
            get { return picture; }
            set 
            { 
                picture = value;
                NotifyPropertyChanged("Picture");
            }
        }

        [XmlArray]
        public ObservableCollection<Task> TaskList
        {
            get { return taskList; }
            set 
            { 
                taskList = value;
                NotifyPropertyChanged("TaskList");
            }

        }

        [XmlArray]
        public ObservableCollection<TDL> SubTDL
        {
            get { return subTDL; }
            set { subTDL = value; }
        }

        public TDL(string name= "", string picture="", ObservableCollection<Task> taskList=default, ObservableCollection<TDL> subTDL=default)
        {
            Name = name;
            Picture = picture;
            TaskList = taskList;
            SubTDL = subTDL;
        }
        public TDL()
        {
            TaskList = new ObservableCollection<Task>();
            SubTDL = new ObservableCollection<TDL>();
        }
    }
}
