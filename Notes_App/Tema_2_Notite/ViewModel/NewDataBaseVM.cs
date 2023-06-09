using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tema_2_Notite.Model;
using Tema_2_Notite.Resources;
using Tema_2_Notite.ViewModel;

namespace Tema_2_Notite.ViewModel
{
    public class NewDataBaseVM : BaseNotification
    {
        Window window;
        MainVM mainVM;

        private string nameDB;

        public string NameDB
        {
            get { return nameDB; }
            set
            {
                nameDB = value;
                NotifyPropertyChanged("NameDB");
            }
        }

        private DataBase selectedDB;

        public DataBase SelectedDB
        {
            get { return selectedDB; }
            set
            {
                selectedDB = value;
                NotifyPropertyChanged("SelectedDB");
            }
        }

        private ObservableCollection<DataBase> listDB;

        public ObservableCollection<DataBase> ListDB
        {
            get { return listDB; }
            set
            {
                listDB = value;
                NotifyPropertyChanged("ListDB");
            }
        }

        public NewDataBaseVM()
        {

        }

        public NewDataBaseVM(Window window, MainVM mainVM, ObservableCollection<DataBase> DB)
        {
            this.window = window;
            this.mainVM = mainVM;
            this.ListDB = DB;
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

        public void DeleteButton()
        {
            mainVM.BaseObject.DataBases.Remove(SelectedDB);
        }

        private ICommand deleteButtonCommand;
        public ICommand DeleteButtonCommand
        {
            get
            {
                if (deleteButtonCommand == null)
                {
                    deleteButtonCommand = new SendCommand(DeleteButton);
                }
                return deleteButtonCommand;
            }
        }

        public void AddButton()
        {
            mainVM.BaseObject.DataBases.Add(new DataBase() { Name = NameDB });
        }

        private ICommand addButtonCommand;
        public ICommand AddButtonCommand
        {
            get
            {
                if (addButtonCommand == null)
                {
                    addButtonCommand = new SendCommand(AddButton);
                }
                return addButtonCommand;
            }
        }

        public void SelectButton()
        {
            mainVM.CurrentDataBase = selectedDB;
        }

        private ICommand selectButtonCommand;
        public ICommand SelectButtonCommand
        {
            get
            {
                if (selectButtonCommand == null)
                {
                    selectButtonCommand = new SendCommand(SelectButton);
                }
                return selectButtonCommand;
            }
        }
    }
}
