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
    public class CreateTDLVM : BaseNotification
    {
        private string newName;
        private string picturePath;
        private string action;

        private readonly Window window;
        private MainVM mainVM;

        ImageStorage imageStorage;
        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                NotifyPropertyChanged("NewName");
            }
        }
        public string PicturePath
        {
            get { return picturePath; }
            set
            {
                picturePath = value;
                NotifyPropertyChanged("PicturePath");
            }
        }

        public CreateTDLVM()
        {
            imageStorage = new ImageStorage();
            picturePath = "../Resources/Images/tree.png";
        }

        public CreateTDLVM(Window window, MainVM mainVM, string action)
        {
            this.mainVM = mainVM;
            this.action = action;
            this.window = window;
            imageStorage = new ImageStorage();
            picturePath = "../Resources/Images/tree.png";
            if (action=="edit")
            {
                picturePath = mainVM.SelectedTDL.Picture;
                NewName = mainVM.SelectedTDL.Name;
            }
        }

        public void PrevPicture()
        {
            if (imageStorage.Index > 0)
            {
                imageStorage.Index--;
            }
            else
            {
                imageStorage.Index = imageStorage.CatJpg.Count - 1;
            }
            PicturePath = imageStorage.CatJpg[imageStorage.Index];
        }

        private ICommand leftArrowommand;
        public ICommand LeftArrowCommand
        {
            get
            {
                if (leftArrowommand == null)
                {
                    leftArrowommand = new SendCommand(PrevPicture);
                }
                return leftArrowommand;
            }
        }

        public void NextPicture()
        {
            if (imageStorage.Index < imageStorage.CatJpg.Count - 1)
            {
                imageStorage.Index++;
            }
            else
            {
                imageStorage.Index = 0;
            }
            PicturePath = imageStorage.CatJpg[imageStorage.Index];
        }

        private ICommand rightArrowCommand;
        
        public ICommand RightArrowCommand
        {
            get
            {
                if (rightArrowCommand == null)
                {
                    rightArrowCommand = new SendCommand(NextPicture);
                }
                return rightArrowCommand;
            }
        }

        public void SaveNewTDL()
        {
            if (IsNameUnique(NewName, mainVM.CurrentDataBase.TDLList))
            {
                if (action == "root")
                {
                    TDL newTDL = new TDL() { Name = newName, Picture = PicturePath };
                    mainVM.CurrentDataBase.TDLList.Add(newTDL);
                }
                else if (action == "sub")
                {
                    TDL newTDL = new TDL() { Name = newName, Picture = PicturePath };
                    mainVM.SelectedTDL.SubTDL.Add(newTDL);
                }
                else if ( action == "edit")
                {
                    mainVM.SelectedTDL.Name = NewName;
                    mainVM.SelectedTDL.Picture = PicturePath;
                }
                window.Close();
            }
            else
            {
                MessageBox.Show("Name Duplicate in Database", "Error");
            }
            
        }

        private ICommand saveNewTDLCommand;
        public ICommand SaveNewTDLCommand
        {
            get
            {
                if (saveNewTDLCommand == null)
                {
                    saveNewTDLCommand = new SendCommand(SaveNewTDL);
                }
                return saveNewTDLCommand;
            }
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

        public bool IsNameUnique(string name, ObservableCollection<TDL> tdlCollection)
        {
            if (tdlCollection == null)
                return true;
            foreach (var tdl in tdlCollection)
            {
                if (tdl.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    // A TDL with the same name already exists
                    return false;
                }

                if (tdl.SubTDL != null)
                {
                    foreach (var subTdl in tdl.SubTDL)
                    {
                        if (subTdl.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        {
                            // A SubTDL with the same name already exists
                            return false;
                        }

                        // Check sub-level SubTDL objects recursively
                        if (!IsNameUnique(name, subTdl.SubTDL))
                        {
                            return false;
                        }
                    }
                }
                
            }

            // No TDL or SubTDL with the same name exists
            return true;
        }

    }
}


