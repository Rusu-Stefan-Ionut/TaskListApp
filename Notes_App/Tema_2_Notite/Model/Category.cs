using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_2_Notite.Model
{
    public class Category : BaseNotification
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public Category()
        {

        }

        public Category(string name)
        {
            Name = name;
        }
    }
}
