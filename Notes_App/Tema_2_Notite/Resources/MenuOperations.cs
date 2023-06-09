using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_2_Notite.ViewModel;
using Tema_2_Notite.View;
using Tema_2_Notite.Model;
using System.Collections.ObjectModel;
using Task = Tema_2_Notite.Model.Task;
using System.Windows;
using System.Windows.Documents;

namespace Tema_2_Notite.Resources
{
    public class MenuOperations
    {
        private MainVM mainVM;
        private MyFunctions myFunctions;
        public MenuOperations(MainVM mainVM)
        {
            this.mainVM = mainVM;
            myFunctions = new MyFunctions();
        }

        public void OpenDataBase()
        {
            Window newWindow = new NewDataBaseWindow();
            NewDataBaseVM newClass = new NewDataBaseVM(newWindow, mainVM, mainVM.BaseObject.DataBases);
            newWindow.DataContext = newClass;
            newWindow.Show();
        }
        public void CreateNewTDL()
        {
            Window newWindow = new CreateTDLWindow();
            CreateTDLVM newClass = new CreateTDLVM(newWindow, mainVM, "root");
            newWindow.DataContext = newClass;
            newWindow.Show();
        }

        public void CreateNewSubTDL()
        {
            Window newWindow = new CreateTDLWindow();
            CreateTDLVM newClass = new CreateTDLVM(newWindow, mainVM, "sub");
            newWindow.DataContext = newClass;
            newWindow.Show();
        }

        public void EditTDL()
        {
            Window newWindow = new CreateTDLWindow();
            CreateTDLVM newClass = new CreateTDLVM(newWindow, mainVM, "edit");
            newWindow.DataContext = newClass;
            newWindow.Show();
        }

        public void DeleteTDL()
        {
            mainVM.CurrentDataBase.TDLList.Remove(mainVM.SelectedTDL);
        }

        public void MoveUpTDL()
        {
            if (mainVM.CurrentDataBase.TDLList.Contains(mainVM.SelectedTDL))
            {
                int index=mainVM.CurrentDataBase.TDLList.IndexOf(mainVM.SelectedTDL);
                if (index > 0 )
                {
                    TDL temp = mainVM.CurrentDataBase.TDLList[index];
                    mainVM.CurrentDataBase.TDLList.RemoveAt(index);
                    mainVM.CurrentDataBase.TDLList.Insert(index-1, temp);
                }
            }
            else
            {
                TDL parent = myFunctions.GetParentTDL(mainVM.SelectedTDL, mainVM.CurrentDataBase.TDLList);
                int index = parent.SubTDL.IndexOf(mainVM.SelectedTDL);
                if (index > 0)
                {
                    TDL temp = parent.SubTDL[index];
                    parent.SubTDL.RemoveAt(index);
                    parent.SubTDL.Insert(index - 1, temp);
                }
            }
        }

        public void MoveDownTDL()
        {
            if (mainVM.CurrentDataBase.TDLList.Contains(mainVM.SelectedTDL))
            {
                int index = mainVM.CurrentDataBase.TDLList.IndexOf(mainVM.SelectedTDL);
                if (index < mainVM.CurrentDataBase.TDLList.Count -1)
                {
                    TDL temp=mainVM.CurrentDataBase.TDLList[index];
                    mainVM.CurrentDataBase.TDLList.RemoveAt(index);
                    mainVM.CurrentDataBase.TDLList.Insert(index + 1, temp);
                }
            }
            else
            {
                TDL parent = myFunctions.GetParentTDL(mainVM.SelectedTDL, mainVM.CurrentDataBase.TDLList);
                int index = parent.SubTDL.IndexOf(mainVM.SelectedTDL);
                if (index < parent.SubTDL.Count - 1)
                {
                    TDL temp = parent.SubTDL[index];
                    parent.SubTDL.RemoveAt(index);
                    parent.SubTDL.Insert(index + 1, temp);
                }
            }
        }

        public void ChangePathTDL()
        {
            ObservableCollection<TDL> list= myFunctions.GetNonSelectedTDLs(mainVM.SelectedTDL, mainVM.CurrentDataBase.TDLList);
        
            Window newWindow= new ChangeTDLPathWindow();
            ChangeTDLPathVM newClass = new ChangeTDLPathVM(newWindow, mainVM, list);
            newWindow.DataContext = newClass;
            newWindow.Show();
        }

        //// Task functions
        public void CreateNewTask()
        {
            Window newWindow = new CreateTaskWindow();
            CreateTaskVM newClass = new CreateTaskVM(newWindow, mainVM, "root");
            newWindow.DataContext = newClass;
            newWindow.Show();
        }

        public void EditTask()
        {
            Window newWindow = new CreateTaskWindow();
            CreateTaskVM newClass = new CreateTaskVM(newWindow, mainVM, "edit");
            newWindow.DataContext = newClass;
            newWindow.Show();
        }

        public void MoveUpTask()
        {
            int index = mainVM.SelectedTDL.TaskList.IndexOf(mainVM.SelectedTask);
            if (index > 0)
            {
                Task temp = mainVM.SelectedTDL.TaskList[index];
                mainVM.SelectedTDL.TaskList.RemoveAt(index);
                mainVM.SelectedTDL.TaskList.Insert(index - 1, temp);
            }
        }

        public void MoveDownTask()
        {
            int index = mainVM.SelectedTDL.TaskList.IndexOf(mainVM.SelectedTask);
            if (index < mainVM.SelectedTDL.TaskList.Count - 1)
            {
                Task temp = mainVM.SelectedTDL.TaskList[index];
                mainVM.SelectedTDL.TaskList.RemoveAt(index);
                mainVM.SelectedTDL.TaskList.Insert(index + 1, temp);
            }
        }

        public void DeleteTask()
        {
            mainVM.SelectedTDL.TaskList.Remove(mainVM.SelectedTask);
        }

        public void SetDoneTask()
        {
            mainVM.SelectedTask.Finished = true;
            mainVM.SelectedTask.FinishedLine = DateTime.Now;
        }

        public void ManageCategory()
        {
            Window newWindow = new ManageCategoryWindow();
            ManageCategoryVM newClass = new ManageCategoryVM(newWindow, mainVM, mainVM.CurrentDataBase.CategoryList);
            newWindow.DataContext = newClass;
            newWindow.Show();
        }

        public void FindTask()
        {
            Window newWindow = new FindTaskWindow();
            FindTaskVM newClass = new FindTaskVM(newWindow, mainVM);
            newWindow.DataContext = newClass;
            newWindow.Show();
        }

        ///Sort and filter functions

        public void SortTasksByDeadline()
        {
            mainVM.FilteredTaskList = new ObservableCollection<Task>(mainVM.SelectedTDL.TaskList.OrderBy(task => task.DeadLine));
        }

        public void SortTasksByPriority()
        {
            var priorityOrder = new Dictionary<string, int>()
            {
                {"High", 0},
                {"Medium", 1},
                {"Low", 2},
                {"", 3 }
            };
            mainVM.FilteredTaskList = new ObservableCollection<Task>(mainVM.SelectedTDL.TaskList.OrderBy(task => priorityOrder[task.Priority]));
        }

        public void OnCategorySelected(Category category)
        {
            mainVM.SelectedCategory = category.Name;
            FilterTasksByCategory();
        }

        public void FilterTasksByCategory()
        {
            ObservableCollection<Task> temp = new ObservableCollection<Task>();
            foreach(var item in mainVM.SelectedTDL.TaskList)
            {
                if (item.Type.Name == mainVM.SelectedCategory)
                    temp.Add(item);
            }
            mainVM.FilteredTaskList=temp;

        }

        public void FilterTasksByFinished()
        {
            ObservableCollection<Task> temp = new ObservableCollection<Task>();
            foreach (var item in mainVM.SelectedTDL.TaskList)
            {
                if (item.Finished == true )
                    temp.Add(item);
            }
            mainVM.FilteredTaskList = temp;
        }

        public void FilterTasksByLateFinished()
        {
            ObservableCollection<Task> temp = new ObservableCollection<Task>();
            foreach (var item in mainVM.SelectedTDL.TaskList)
            {
                if (item.Finished ==true && item.FinishedLine > item.DeadLine)
                    temp.Add(item);
            }
            mainVM.FilteredTaskList = temp;

        }

        public void FilterTasksByDue()
        {
            ObservableCollection<Task> temp = new ObservableCollection<Task>();
            foreach (var item in mainVM.SelectedTDL.TaskList)
            {
                if (item.Finished == false && DateTime.Now > item.DeadLine)
                    temp.Add(item);
            }
            mainVM.FilteredTaskList = temp;

        }

        public void FilterTasksByUnfinished()
        {
            ObservableCollection<Task> temp = new ObservableCollection<Task>();
            foreach (var item in mainVM.SelectedTDL.TaskList)
            {
                if (item.Finished == false)
                    temp.Add(item);
            }
            mainVM.FilteredTaskList = temp;

        }

        public void UpdateStats()
        {
            mainVM.CurrentDataBase.Stat1 = myFunctions.CountTasksWithDeadlineToday(mainVM.CurrentDataBase.TDLList);
            mainVM.CurrentDataBase.Stat2 = myFunctions.CountTasksWithDeadlineTomorrow(mainVM.CurrentDataBase.TDLList);
            mainVM.CurrentDataBase.Stat3 = myFunctions.CountTasksFinishedLate(mainVM.CurrentDataBase.TDLList);
            mainVM.CurrentDataBase.Stat4 = myFunctions.CountTasksUnfinished(mainVM.CurrentDataBase.TDLList);
        }

    }
}
