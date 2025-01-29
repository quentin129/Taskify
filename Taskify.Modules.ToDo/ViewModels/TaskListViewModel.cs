using GongSolutions.Wpf.DragDrop;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using Taskify.Modules.ToDo.Models;

namespace Taskify.Modules.ToDo.ViewModels
{
    public class TaskListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;
        private Random Random = new Random();
        private ObservableCollection<TaskItemViewModel> _taskItemViewModels;
        public ObservableCollection<TaskItemViewModel> TaskItemViewModels
        {
            get => _taskItemViewModels;
            set => SetProperty(ref _taskItemViewModels, value);
        }

        private DelegateCommand _addTaskCommand;
        public DelegateCommand AddTaskCommand =>
            _addTaskCommand ??= new DelegateCommand(AddTask);

        public TaskListViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _eventAggregator.GetEvent<TaskDeletedEvent>().Subscribe(OnTaskDeleted);

            _addTaskCommand = new DelegateCommand(AddTask);

            TaskItemViewModels = new ObservableCollection<TaskItemViewModel>();

            LoadToDoVMs();
           
                //{
            //    new TaskItemViewModel(new TaskItem { Title = "Aufgabe 1", Description = "Beschreibung 1", Priority = Priority.High, Deadline = DateTime.Now.AddDays(3)}, _eventAggregator, _dialogService),
            //    new TaskItemViewModel(new TaskItem { Title = "Aufgabe 2", Description = "Beschreibung 2", Priority = Priority.Medium, Deadline = DateTime.Now.AddDays(1)}, _eventAggregator, _dialogService),
            //    new TaskItemViewModel(new TaskItem { Title = "Aufgabe 3", Description = "Beschreibung 3", Priority = Priority.Low, Deadline = DateTime.Now.AddDays(5)}, _eventAggregator, _dialogService)
            //};
        }

        private void AddTask()
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();

            TaskItemViewModels.Add(new TaskItemViewModel(new TaskItem { Title = "Test", Description = "Beschreibung 1", Priority = (Priority)new Random().Next(0, 3), Deadline = DateTime.Now.AddDays(3) }, _eventAggregator, _dialogService));
            foreach (TaskItemViewModel taskVM in TaskItemViewModels)
            {
                tasks.Add(taskVM.Task);
            }

            ToDoDataService.SaveToDos(tasks);
        }

        private void LoadToDoVMs()
        {
            ObservableCollection<TaskItem> tasks = ToDoDataService.LoadToDos();

            foreach (TaskItem task in tasks)
            {
                TaskItemViewModels.Add(new TaskItemViewModel(task, _eventAggregator, _dialogService));
            }
        }

        private void OnTaskDeleted(TaskItemViewModel taskItemViewModel)
        {
            TaskItemViewModels.Remove(taskItemViewModel);
        }

        #region DragDrop
        #endregion
    }
}
