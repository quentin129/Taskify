using System;
using System.Collections.ObjectModel;
using System.Linq;
using Taskify.Modules.ToDo.Models;
using Taskify.Modules.ToDo.ViewModels;

namespace Taskify.Modules.ToDo.Services
{
    public interface ITaskService
    {
        ObservableCollection<TaskItem> GetTasks();
        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(TaskItem task);
    }

    public class TaskService : ITaskService
    {
        private ObservableCollection<TaskItem> _tasks = new ObservableCollection<TaskItem>();

        public ObservableCollection<TaskItem> GetTasks() => _tasks;

        public void AddTask(TaskItem task) => _tasks.Add(task);

        public void UpdateTask(TaskItem task)
        {
            TaskItem existingTaskItem = _tasks.FirstOrDefault(t => t.Guid == task.Guid);
            if (existingTaskItem != null)
            {
                existingTaskItem.Title = task.Title;
                existingTaskItem.Description = task.Description;
                existingTaskItem.Deadline = task.Deadline;
                existingTaskItem.IsCompleted = task.IsCompleted;
                existingTaskItem.Priority = task.Priority;
                existingTaskItem.Reminders = task.Reminders;
            }
        }

        public void DeleteTask(TaskItem task) => _tasks.Remove(task);
    }
}
