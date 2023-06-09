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
using System.Diagnostics;
using System.Xml.Linq;

namespace Tema_2_Notite.ViewModel
{
    public class CreateTaskVM : BaseNotification
    {
        private readonly Window window;
        private MainVM mainVM;
        private string action;

        private string taskName="";

        public string TaskName
        {
            get { return taskName; }
            set
            {
                taskName = value;
                NotifyPropertyChanged("TaskName");
            }
        }

        private string taskPriority="";

        public string TaskPriority
        {
            get { return taskPriority; }
            set
            {
                taskPriority = value;
                NotifyPropertyChanged("TaskPriority");
            }
        }
        private string taskDescription = "";

        public string TaskDescription
        {
            get { return taskDescription; }
            set
            {
                taskDescription = value;
                NotifyPropertyChanged("TaskDescription");
            }
        }

        private Category taskCategory;

        public Category TaskCategory
        {
            get { return taskCategory; }
            set
            {
                taskCategory = value;
                NotifyPropertyChanged("TaskCategory");
            }
        }

        private DateTime taskDate= DateTime.Now;

        public DateTime TaskDate
        {
            get { return taskDate; }
            set
            {
                taskDate = value;
                NotifyPropertyChanged("TaskDate");
            }
        }

        private List<string> priorityList;

        public List<string> PriorityList
        {
            get { return priorityList; }
            set
            {
                priorityList = value;
                NotifyPropertyChanged("PriorityList");
            }
        }

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

        public CreateTaskVM(Window window, MainVM mainVM, string action)
        {
            this.mainVM = mainVM;
            this.action = action;
            this.window = window;
            PriorityList = new List<string> { "Low", "Medium", "High" };
            CategoryList = mainVM.CurrentDataBase.CategoryList;
            if( action == "edit")
            {
                TaskName = mainVM.SelectedTask.Name;
                TaskDescription = mainVM.SelectedTask.Description;
                TaskPriority = mainVM.SelectedTask.Priority;
                TaskCategory = mainVM.SelectedTask.Type;
                TaskDate = mainVM.SelectedTask.DeadLine;
            }
        }

        public CreateTaskVM()
        {
            PriorityList = new List<string> { "Low", "Medium", "High" };
            
        }

        public void SaveTask()
        {
            if (action == "root")
            {
                if (TaskCategory == null)
                    taskCategory = mainVM.CurrentDataBase.CategoryList[0];
                if (TaskPriority == null)
                    taskPriority = priorityList[0];
                    
                Task newTask = new Task() { Name = TaskName, Description=TaskDescription, Priority=TaskPriority.ToString(), Type=TaskCategory, DeadLine=TaskDate };
                mainVM.SelectedTDL.TaskList.Add(newTask);
            }
            else if (action == "edit")
            {
                mainVM.SelectedTask.Name = TaskName;
                mainVM.SelectedTask.Description = TaskDescription;
                mainVM.SelectedTask.Priority = TaskPriority;
                mainVM.SelectedTask.Type = TaskCategory;
                mainVM.SelectedTask.DeadLine = TaskDate;
            }
            window.Close();
         

        }

        private ICommand saveTaskCommand;
        public ICommand SaveTaskCommand
        {
            get
            {
                if (saveTaskCommand == null)
                {
                    saveTaskCommand = new SendCommand(SaveTask);
                }
                return saveTaskCommand;
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
    }
}
