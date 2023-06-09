using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tema_2_Notite.Model
{
    [Serializable]
    public class Task: BaseNotification
    {
        private string name;
        private string description;
        private string priority;
        private Category type;
        private DateTime deadLine;
        private DateTime finishedLine;
        private bool finished;

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
        public string Description
        {
            get { return description; }
            set
            { 
                description = value;
                NotifyPropertyChanged("Description");
            }
        }

        [XmlElement]
        public string Priority
        {
            get { return priority; }
            set 
            { 
                priority = value;
                NotifyPropertyChanged("Priority");
            }
        }

        [XmlElement]
        public Category Type
        {
            get { return type; }
            set 
            { 
                type = value; 
                NotifyPropertyChanged("Type");
            }
        }

        [XmlElement]
        public DateTime DeadLine
        {
            get { return deadLine; }
            set 
            {
                deadLine = value;
                NotifyPropertyChanged("DeadLine");
            }
        }

        [XmlElement]
        public DateTime FinishedLine
        {
            get { return finishedLine; }
            set
            {
                finishedLine = value;
                NotifyPropertyChanged("FinishedLine");
            }
        }

        [XmlElement]
        public bool Finished
        {
            get { return finished; }
            set
            {
                finished = value;
                NotifyPropertyChanged("Finished");
            }
        }

        public Task(string name="", string description="", string priority = "", Category type = default, DateTime deadLine=default(DateTime), DateTime finishedLine=default(DateTime),  bool finished=false)
        {
            Name = name;
            Description = description;
            Priority = priority;
            Type = type;
            DeadLine = deadLine;
            FinishedLine = finishedLine;
            Finished = finished;
        }

        public Task()
        {
        
        }
    }
}
