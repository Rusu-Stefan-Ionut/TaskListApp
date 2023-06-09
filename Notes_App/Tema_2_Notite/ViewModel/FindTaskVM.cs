using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema_2_Notite.Model;
using Tema_2_Notite.Resources;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Tema_2_Notite.View;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Windows;
using Task = Tema_2_Notite.Model.Task;

namespace Tema_2_Notite.ViewModel
{
    public class FindTaskVM : BaseNotification
    {
        MainVM mainVM;
        private readonly Window window;
        private MyFunctions myFunctions;

        private string nameTF;

        public string NameTF
        {
            get { return nameTF; }
            set 
            { 
                nameTF = value;
                NotifyPropertyChanged("Name");
            }
        }

        private DateTime? dateTF = null;
        public DateTime? DateTF
        {
            get { return dateTF; }
            set
            {
                dateTF = value;
                NotifyPropertyChanged("DateTF");
            }
        }

        private ObservableCollection<Task> foundTasks;

        public ObservableCollection<Task> FoundTasks
        {
            get { return foundTasks; }
            set
            {
                foundTasks = value;
                NotifyPropertyChanged("FoundTasks");
            }
        }

        private ObservableCollection<string> foundPaths;

        public ObservableCollection<string> FoundPaths
        {
            get { return foundPaths; }
            set
            {
                foundPaths = value;
                NotifyPropertyChanged("FoundTasks");
            }
        }

        public FindTaskVM()
        {

        }

        public FindTaskVM(Window window, MainVM mainVM)
        {
            this.window = window;
            this.mainVM = mainVM;
            this.myFunctions = new MyFunctions();
            FoundPaths = new ObservableCollection<string>();
        }

        public void CancellButton()
        {
            window.Close();
        }

        private ICommand cancellButtonCommand;
        public ICommand CancellButtonCommand
        {
            get
            {
                if (cancellButtonCommand == null)
                {
                    cancellButtonCommand = new SendCommand(CancellButton);
                }
                return cancellButtonCommand;
            }
        }


        public void FindButton()
        {
            FoundPaths.Clear();
            FoundTasks = myFunctions.FindTasks3(NameTF, mainVM.CurrentDataBase.TDLList, DateTF, FoundPaths); 
        }

        private ICommand findButtonCommand;
        public ICommand FindButtonCommand
        {
            get
            {
                if (findButtonCommand == null)
                {
                    findButtonCommand = new SendCommand(FindButton);
                }
                return findButtonCommand;
            }
        }

    }
}
