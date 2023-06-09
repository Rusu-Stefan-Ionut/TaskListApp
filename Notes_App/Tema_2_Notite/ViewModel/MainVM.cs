using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema_2_Notite.Model;
using Tema_2_Notite.Resources;
using Task = Tema_2_Notite.Model.Task;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Tema_2_Notite.View;

namespace Tema_2_Notite.ViewModel
{
    public class MainVM : BaseNotification
    {
        private DataBase currentDataBase;
        private TDL selectedTDL;
        private Task selectedTask;

        private MenuOperations operations;
        public DataBase CurrentDataBase
        {
            get { return currentDataBase; }
            set
            {
                currentDataBase = value;
                if( currentDataBase != null)
                    BaseObject.LastOpenedDatabaseName = CurrentDataBase.Name;
                NotifyPropertyChanged("CurrentDataBase");
            }
        }

        public TDL SelectedTDL
        {
            get { return selectedTDL; }
            set
            {
                selectedTDL = value;
                IsTDLSelected = MyFunctions.CanExecuteOperation(SelectedTDL);
                if (selectedTDL != null)
                    FilteredTaskList = SelectedTDL.TaskList;
                NotifyPropertyChanged("SelectedTDL");
            }
        }

        public Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                IsTaskSelected = MyFunctions.CanExecuteOperation(SelectedTask);
                NotifyPropertyChanged("SelectedTask");
            }
        }

        public void GetTDL(TDL tdl)
        {
            SelectedTDL = tdl;
        }

        private ICommand changeTaskCommand;
        public ICommand ChangeTaskCommand
        {
            get
            {
                if (changeTaskCommand == null)
                {
                    changeTaskCommand = new RelayCommand<TDL>(GetTDL);
                }
                return changeTaskCommand;
            }
            set
            {
                changeTaskCommand = value;
            }
        }

        private ObjectToSerialize baseObject;

        public ObjectToSerialize BaseObject
        {
            get { return baseObject; }
            set { baseObject = value; }
        }

        public MainVM()
        {
            baseObject = new ObjectToSerialize();
            var serializationActions = new SerializationActions(baseObject.DataBases);
            serializationActions.DeserializeObject();

            string lastOpenedDatabaseName = serializationActions.FileName;
            BaseObject.LastOpenedDatabaseName = lastOpenedDatabaseName;
            if (lastOpenedDatabaseName != null)
            {
                CurrentDataBase = baseObject.DataBases.FirstOrDefault(db => db.Name == lastOpenedDatabaseName);
            }
            else
            {
                currentDataBase = new DataBase();
                BaseObject.DataBases.Add(CurrentDataBase);
                BaseObject.LastOpenedDatabaseName = CurrentDataBase.Name;
            }

            operations = new MenuOperations(this);
        }

        private ICommand openDataBaseCommand;

        public ICommand OpenDataBaseCommand
        {
            get
            {
                if (openDataBaseCommand == null)
                {
                    openDataBaseCommand = new SendCommand(operations.OpenDataBase);
                }
                return openDataBaseCommand;
            }
        }


        private bool isTDLSelected = false;
        public bool IsTDLSelected
        {
            get
            {
                return isTDLSelected;
            }
            set
            {
                if (isTDLSelected == value)
                {
                    return;
                }
                isTDLSelected = value;
            }
        }

        private bool isTaskSelected = false;
        public bool IsTaskSelected
        {
            get
            {
                return isTaskSelected;
            }
            set
            {
                if (isTaskSelected == value)
                {
                    return;
                }
                isTaskSelected = value;
            }
        }

        private ICommand createNewTDLCommand;

        public ICommand CreateNewTDLCommand
        {
            get
            {
                if (createNewTDLCommand == null)
                {
                    createNewTDLCommand = new SendCommand(operations.CreateNewTDL);
                }
                return createNewTDLCommand;
            }
        }

        private ICommand createNewSubTDLCommand;

        public ICommand CreateNewSubTDLCommand
        {
            get
            {
                if (createNewSubTDLCommand == null)
                {
                    createNewSubTDLCommand = new SendCommand(operations.CreateNewSubTDL, param => isTDLSelected);
                }
                return createNewSubTDLCommand;
            }
        }

        private ICommand editTDLCommand;

        public ICommand EditTDLCommand
        {
            get
            {
                if (editTDLCommand == null)
                {
                    editTDLCommand = new SendCommand(operations.EditTDL, param => isTDLSelected);
                }
                return editTDLCommand;
            }
        }

        private ICommand deleteTDLCommand;

        public ICommand DeleteTDLCommand
        {
            get
            {
                if (deleteTDLCommand == null)
                {
                    deleteTDLCommand = new SendCommand(operations.DeleteTDL, param => isTDLSelected);
                }
                return deleteTDLCommand;
            }
        }

        private ICommand moveUpTDLCommand;

        public ICommand MoveUpTDLCommand
        {
            get
            {
                if (moveUpTDLCommand == null)
                {
                    moveUpTDLCommand = new SendCommand(operations.MoveUpTDL, param => isTDLSelected);
                }
                return moveUpTDLCommand;
            }
        }

        private ICommand moveDownTDLCommand;

        public ICommand MoveDownTDLCommand
        {
            get
            {
                if (moveDownTDLCommand == null)
                {
                    moveDownTDLCommand = new SendCommand(operations.MoveDownTDL, param => isTDLSelected);
                }
                return moveDownTDLCommand;
            }
        }

        private ICommand changePathTDLCommand;

        public ICommand ChangePathTDLCommand
        {
            get
            {
                if (changePathTDLCommand == null)
                {
                    changePathTDLCommand = new SendCommand(operations.ChangePathTDL, param => isTDLSelected);
                }
                return changePathTDLCommand;
            }
        }

        //////TASK commands

        private ICommand createNewTaskCommand;

        public ICommand CreateNewTaskCommand
        {
            get
            {
                if (createNewTaskCommand == null)
                {
                    createNewTaskCommand = new SendCommand(operations.CreateNewTask, param => isTDLSelected);
                }
                return createNewTaskCommand;
            }
        }

        private ICommand editTaskCommand;

        public ICommand EditTaskCommand
        {
            get
            {
                if (editTaskCommand == null)
                {
                    editTaskCommand = new SendCommand(operations.EditTask, param => IsTaskSelected);
                }
                return editTaskCommand;
            }
        }

        private ICommand moveUpTaskCommand;

        public ICommand MoveUpTaskCommand
        {
            get
            {
                if (moveUpTaskCommand == null)
                {
                    moveUpTaskCommand = new SendCommand(operations.MoveUpTask, param => isTaskSelected);
                }
                return moveUpTaskCommand;
            }
        }

        private ICommand moveDownTaskCommand;

        public ICommand MoveDownTaskCommand
        {
            get
            {
                if (moveDownTaskCommand == null)
                {
                    moveDownTaskCommand = new SendCommand(operations.MoveDownTask, param => isTaskSelected);
                }
                return moveDownTaskCommand;
            }
        }

        private ICommand deleteTaskCommand;

        public ICommand DeleteTaskCommand
        {
            get
            {
                if (deleteTaskCommand == null)
                {
                    deleteTaskCommand = new SendCommand(operations.DeleteTask, param => isTaskSelected);
                }
                return deleteTaskCommand;
            }
        }

        private ICommand setDoneTaskCommand;

        public ICommand SetDoneTaskCommand
        {
            get
            {
                if (setDoneTaskCommand == null)
                {
                    setDoneTaskCommand = new SendCommand(operations.SetDoneTask, param => isTaskSelected);
                }
                return setDoneTaskCommand;
            }
        }


        private ICommand manageCategoryCommand;

        public ICommand ManageCategoryCommand
        {
            get
            {
                if (manageCategoryCommand == null)
                {
                    manageCategoryCommand = new SendCommand(operations.ManageCategory);
                }
                return manageCategoryCommand;
            }
        }

        private ICommand findTaskCommand;

        public ICommand FindTaskCommand
        {
            get
            {
                if (findTaskCommand == null)
                {
                    findTaskCommand = new SendCommand(operations.FindTask);
                }
                return findTaskCommand;
            }
        }

        /////View - filter
        
        private ObservableCollection<Task> filteredTaskList;

        public ObservableCollection<Task> FilteredTaskList
        {
            get { return filteredTaskList; }
            set
            {
                filteredTaskList = value;
                NotifyPropertyChanged("FilteredTaskList");
            }
        }

        private ICommand dueTasksCommnad;

        public ICommand DueTasksCommnad
        {
            get
            {
                if (dueTasksCommnad == null)
                {
                    dueTasksCommnad = new SendCommand(operations.FilterTasksByDue, param => IsTDLSelected);
                }
                return dueTasksCommnad;
            }
        }

        private ICommand unfinishedTasksCommand;

        public ICommand UnfinishedTasksCommand
        {
            get
            {
                if (unfinishedTasksCommand == null)
                {
                    unfinishedTasksCommand = new SendCommand(operations.FilterTasksByUnfinished, param => IsTDLSelected);
                }
                return unfinishedTasksCommand;
            }
        }

        private ICommand lateFinishedTasksCommand;
        public ICommand LateFinishedTasksCommand
        {
            get
            {
                if (lateFinishedTasksCommand == null)
                {
                    lateFinishedTasksCommand = new SendCommand(operations.FilterTasksByLateFinished, param => IsTDLSelected);
                }
                return lateFinishedTasksCommand;
            }
        }
        private ICommand finishedTasksCommand;
        public ICommand FinishedTasksCommand
        {
            get
            {
                if (finishedTasksCommand == null)
                {
                    finishedTasksCommand = new SendCommand(operations.FilterTasksByFinished, param => IsTDLSelected);
                }
                return finishedTasksCommand;
            }
        }

        private string selectedCategory;
        public string SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                
                NotifyPropertyChanged("SelectedCategory");
            }
        }

        private ICommand byCategoryCommand;

        public ICommand ByCategoryCommand
        {
            get
            {
                if (byCategoryCommand == null)
                {
                    byCategoryCommand = new RelayCommand<Category>(operations.OnCategorySelected, param => IsTDLSelected);
                }
                return byCategoryCommand;
            }
        }

        //View - sort

        private ICommand byDeadlineCommand;

        public ICommand ByDeadLineCommand
        {
            get
            {
                if (byDeadlineCommand == null)
                {
                    byDeadlineCommand = new SendCommand(operations.SortTasksByDeadline, param => IsTDLSelected);
                }
                return byDeadlineCommand;
            }
        }

        private ICommand byPriorityCommand;

        public ICommand ByPriorityCommand
        {
            get
            {
                if (byPriorityCommand == null)
                {
                    byPriorityCommand = new SendCommand(operations.SortTasksByPriority, param => IsTDLSelected);
                }
                return byPriorityCommand;
            }
        }

        

        private ICommand statisticsCommand;

        public ICommand StatisticsCommand
        {
            get
            {
                if (statisticsCommand == null)
                {
                    statisticsCommand = new SendCommand(operations.UpdateStats);
                }
                return statisticsCommand;
            }
        }

    }
}


