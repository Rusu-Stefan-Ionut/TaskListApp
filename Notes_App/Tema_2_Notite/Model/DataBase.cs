using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Tema_2_Notite.ViewModel;

namespace Tema_2_Notite.Model
{
    [Serializable]
    public class DataBase: BaseNotification
    {
        private string name;
        private ObservableCollection<TDL> tdlList;

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set 
            {
                name = value; 
                NotifyPropertyChanged("Name");
            }
        }

        [XmlArray]
        public ObservableCollection<TDL> TDLList
        {
            get { return tdlList; }
            set
            {
                tdlList = value;
                NotifyPropertyChanged("TDLList");
            }
        }

        private ObservableCollection<Category> categoryList;

        [XmlArray]
        public ObservableCollection<Category> CategoryList
        {
            get { return categoryList; }
            set
            {
                categoryList = value;
                NotifyPropertyChanged("CategoryList");
            }
        }

        private int stat1;

        [XmlAttribute]
        public int Stat1
        {
            get { return stat1; }
            set
            {
                stat1 = value;
                NotifyPropertyChanged("Stat1");
            }
        }

        private int stat2;

        [XmlAttribute]
        public int Stat2
        {
            get { return stat2; }
            set
            {
                stat2 = value;
                NotifyPropertyChanged("Stat2");
            }
        }

        private int stat3;

        [XmlAttribute]
        public int Stat3
        {
            get { return stat3; }
            set
            {
                stat3 = value;
                NotifyPropertyChanged("Stat3");
            }
        }

        private int stat4;

        [XmlAttribute]
        public int Stat4
        {
            get { return stat4; }
            set
            {
                stat4 = value;
                NotifyPropertyChanged("Stat4");
            }
        }

        public DataBase(string name, ObservableCollection<TDL> tdls, ObservableCollection<Category> categoryList)
        {
            Name = name;
            TDLList = tdls;
            CategoryList = categoryList;
        }

        public DataBase()
        {
            Name = "1";
            TDLList = new ObservableCollection<TDL>();
            CategoryList = new ObservableCollection<Category>();   
            
        }

    }
}
