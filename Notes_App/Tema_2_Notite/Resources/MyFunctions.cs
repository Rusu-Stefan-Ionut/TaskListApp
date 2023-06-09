using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tema_2_Notite.Model;
using Task = Tema_2_Notite.Model.Task;

namespace Tema_2_Notite.Resources
{
    internal class MyFunctions
    {
        public static bool CanExecuteOperation(TDL tdl)
        {
            if (tdl == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CanExecuteOperation(Task task)
        {
            if (task == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public TDL GetParentTDL(TDL subTDL, ObservableCollection<TDL> TDLList)
        {
            foreach (TDL tdl in TDLList)
            {
                if (tdl.SubTDL.Contains(subTDL))
                {
                    return tdl;
                }
                else
                {
                    TDL parentTDL = GetParentTDL(subTDL, tdl.SubTDL);
                    if (parentTDL != null)
                    {
                        return parentTDL;
                    }
                }
            }
            return null;
        }


        public ObservableCollection<TDL> GetNonSelectedTDLs(TDL selectedTDL, ObservableCollection<TDL> tdls)
        {
            var result = new ObservableCollection<TDL>();
            foreach (var tdl in tdls)
            {
                if (!ContainsTDL(selectedTDL, tdl))
                {
                    result.Add(tdl);
                    if (tdl.SubTDL != null && tdl.SubTDL.Any())
                    {
                        var subTDLs = GetNonSelectedTDLs(selectedTDL, tdl.SubTDL);
                        foreach (var subTDL in subTDLs)
                        {
                            result.Add(subTDL);
                        }
                    }
                }
            }
            return result;
        }

        private bool ContainsTDL(TDL selectedTDL, TDL tdl)
        {
            if (selectedTDL == tdl)
            {
                return true;
            }
            if (tdl.SubTDL != null && tdl.SubTDL.Any())
            {
                foreach (var subTDL in tdl.SubTDL)
                {
                    if (ContainsTDL(selectedTDL, subTDL))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public ObservableCollection<Task> FindTasks3(string taskName, ObservableCollection<TDL> tdlCollection, DateTime? deadline = null, ObservableCollection<string> paths = null, string pathPrefix = "")
        {
            var result = new ObservableCollection<Task>();

            foreach (var tdl in tdlCollection)
            {
                var subPaths = paths;
                //if (paths != null) subPaths.Add(tdl.Name); // Add the current TDL name to the path collection

                var subResult = FindTasks3(taskName, tdl.SubTDL, deadline, subPaths, $"{pathPrefix}{tdl.Name} >> ");
                foreach (var task in subResult)
                {
                    // If the task is part of a sub-TDL, add the sub-TDL path to the task's path
                    if (subPaths != null)
                    {
                        var path = $"{pathPrefix}{tdl.Name}";
                        foreach (var subPath in subPaths)
                        {
                            path += $" >> {subPath}";
                        }
                        //task.Path = path;
                    }
                    result.Add(task);
                }

                foreach (var task in tdl.TaskList)
                {
                    if (task.Name == taskName && (deadline == null || task.DeadLine == deadline))
                    {
                        //task.Path = $"{pathPrefix}{tdl.Name}"; // Set the path of the found task
                        result.Add(task);
                        if (paths != null) // If a path collection is provided, add the path to the found task
                        {
                            var path = $"{pathPrefix}{tdl.Name}";
                            foreach (var subPath in subPaths)
                            {
                                path += $" >> {subPath}";
                            }
                            paths.Add(path);
                        }
                    }
                }
            }

            return result;
        }

        /////Stats

        public int CountTasksWithDeadlineToday(ObservableCollection<TDL> tdlList)
        {
            int count = 0;

            foreach (TDL tdl in tdlList)
            {
                count += CountTasksWithDeadlineTodayRecursive(tdl);
            }

            return count;
        }

        private int CountTasksWithDeadlineTodayRecursive(TDL tdl)
        {
            int count = 0;

            foreach (Task task in tdl.TaskList)
            {
                if (task.DeadLine.Date == DateTime.Today.Date)
                {
                    count++;
                }
            }

            foreach (TDL subTDL in tdl.SubTDL)
            {
                count += CountTasksWithDeadlineTodayRecursive(subTDL);
            }

            return count;
        }

        //
        public int CountTasksWithDeadlineTomorrow(ObservableCollection<TDL> tdlList)
        {
            int count = 0;
            DateTime tomorrow = DateTime.Today.AddDays(1);
            foreach (TDL tdl in tdlList)
            {
                count += CountTasksWithDeadlineTomorrow(tdl, tomorrow);
            }
            return count;
        }

        private int CountTasksWithDeadlineTomorrow(TDL tdl, DateTime tomorrow)
        {
            int count = 0;
            foreach (Task task in tdl.TaskList)
            {
                if (task.DeadLine!=null && task.DeadLine.Date == tomorrow.Date)
                {
                    count++;
                }
            }
            foreach (TDL subTDL in tdl.SubTDL)
            {
                count += CountTasksWithDeadlineTomorrow(subTDL, tomorrow);
            }
            return count;
        }

        //
        public int CountTasksFinishedLate(ObservableCollection<TDL> tdlList)
        {
            int count = 0;
            foreach (TDL tdl in tdlList)
            {
                count += CountTasksFinishedLateRecursive(tdl);
            }
            return count;
        }

        private int CountTasksFinishedLateRecursive(TDL tdl)
        {
            int count = 0;
            foreach (Task task in tdl.TaskList)
            {
                if (task.Finished && task.FinishedLine > task.DeadLine)
                {
                    count++;
                }
            }
            foreach (TDL subTDL in tdl.SubTDL)
            {
                count += CountTasksFinishedLateRecursive(subTDL);
            }
            return count;
        }

        ///
        public int CountTasksUnfinished(ObservableCollection<TDL> tdlList)
        {
            int count = 0;
            foreach (TDL tdl in tdlList)
            {
                count += CountTasksUnfinishedRecursive(tdl);
            }
            return count;
        }

        private int CountTasksUnfinishedRecursive(TDL tdl)
        {
            int count = 0;
            foreach (Task task in tdl.TaskList)
            {
                if (task.Finished==false && DateTime.Now < task.DeadLine)
                {
                    count++;
                }
            }
            foreach (TDL subTDL in tdl.SubTDL)
            {
                count += CountTasksUnfinishedRecursive(subTDL);
            }
            return count;
        }
    }
}
