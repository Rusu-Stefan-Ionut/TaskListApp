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
    public class ManageCategoryVM : BaseNotification
    {
        private readonly Window window;
        private MainVM mainVM;

        private ObservableCollection<Category> categoryList;

        public ObservableCollection<Category> CategoryList
        {
            get { return categoryList; }
            set
            {
                categoryList = value;
                NotifyPropertyChanged("CategoryList");
            }
        }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                NotifyPropertyChanged("SelectedCategory");
            }
        }
        public ManageCategoryVM()
        {
        }

        public ManageCategoryVM(Window window, MainVM mainVM, ObservableCollection<Category> categoryList)
        {
            this.mainVM = mainVM;
            this.window = window;
            CategoryList = categoryList;
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
            CategoryList.Remove(SelectedCategory);
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
            CategoryList.Add(new Category());
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

    }    
}


