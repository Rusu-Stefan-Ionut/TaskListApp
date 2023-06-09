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

namespace Tema_2_Notite.ViewModel
{
    public class ChangeTDLPathVM : BaseNotification
    {
        private readonly Window window;
        private MainVM mainVM;
        private MyFunctions myFunctions;

        private ObservableCollection<TDL> options;

        public ObservableCollection<TDL> Options
        {
            get { return options; }
            set
            {
                options = value;
                NotifyPropertyChanged("Options");
            }
        }

        private TDL selectedPath;

        public TDL SelectedPath
        {
            get { return selectedPath; }
            set
            {
                selectedPath = value;
                NotifyPropertyChanged("SelectedPath");
            }
        }

        public ChangeTDLPathVM()
        {
        }

        public ChangeTDLPathVM(Window window, MainVM mainVM, ObservableCollection<TDL> options)
        {
            this.mainVM = mainVM;
            this.window = window;
            this.options = options;
            this.myFunctions = new MyFunctions();  
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

        public void changePath()
        {
            if (mainVM.CurrentDataBase.TDLList.Contains(SelectedPath))
            {
                TDL temp = mainVM.SelectedTDL;
                mainVM.CurrentDataBase.TDLList.Remove(mainVM.SelectedTDL);
                selectedPath.SubTDL.Add(temp);
            }
            else
            {
                TDL parent = myFunctions.GetParentTDL(mainVM.SelectedTDL, mainVM.CurrentDataBase.TDLList);
                TDL temp = mainVM.SelectedTDL;
                parent.SubTDL.Remove(mainVM.SelectedTDL);
                selectedPath.SubTDL.Add(temp);
            }
            window.Close();
        }

        private ICommand saveButtonCommand;
        public ICommand SaveButtonCommand
        {
            get
            {
                if (saveButtonCommand == null)
                {
                    saveButtonCommand = new SendCommand(changePath);
                }
                return saveButtonCommand;
            }
        }

        public void changePathToRoot()
        {
            if (mainVM.CurrentDataBase.TDLList.Contains(SelectedPath))
            {
                //Do nothing
            }
            else
            {
                TDL parent = myFunctions.GetParentTDL(mainVM.SelectedTDL, mainVM.CurrentDataBase.TDLList);
                TDL temp = mainVM.SelectedTDL;
                parent.SubTDL.Remove(mainVM.SelectedTDL);
                mainVM.CurrentDataBase.TDLList.Add(temp);
            }
            window.Close();
        }

        private ICommand makeRootButtonCommand;
        public ICommand MakeRootButtonCommand
        {
            get
            {
                if (makeRootButtonCommand == null)
                {
                    makeRootButtonCommand = new SendCommand(changePathToRoot);
                }
                return makeRootButtonCommand;
            }
        }

    }
}


