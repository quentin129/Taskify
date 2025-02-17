using GongSolutions.Wpf.DragDrop;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Taskify.Modules.ToDo.Models;
using Taskify.Modules.ToDo.Services;
using System.Windows.Input;

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

        private TaskItemViewModel _selectedTask;
        public TaskItemViewModel SelectedTask
        {
            get { return _selectedTask; }
            set { SetProperty(ref _selectedTask, value); }
        }

        private bool _isTaskSelected;
        public bool IsTaskSelected
        {
            get { return _isTaskSelected; }
            set { SetProperty(ref _isTaskSelected, value); }
        }

        private DelegateCommand<TaskItemViewModel> _taskSelectedCommand;
        public DelegateCommand<TaskItemViewModel> TaskSelectedCommand =>
            _taskSelectedCommand ??= new DelegateCommand<TaskItemViewModel>(OnTaskSelected);

        private DelegateCommand _addTaskCommand;
        public DelegateCommand AddTaskCommand =>
            _addTaskCommand ??= new DelegateCommand(AddTask);

        public TaskListViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _eventAggregator.GetEvent<TaskDeletedEvent>().Subscribe(OnTaskDeleted);
            _eventAggregator.GetEvent<ShellClosedEvent>().Subscribe(OnShellClosed);
            _eventAggregator.GetEvent<SaveEvent>().Subscribe(OnSave);

            _addTaskCommand = new DelegateCommand(AddTask);
            _taskSelectedCommand = new DelegateCommand<TaskItemViewModel>(OnTaskSelected);

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
            TaskItemViewModels.Add(new TaskItemViewModel(new TaskItem { Title = "Test", Description = "Beschreibung 1", Priority = (Priority)new Random().Next(0, 3), Deadline = DateTime.Now.AddDays(3) }, _eventAggregator, _dialogService));

            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            foreach (TaskItemViewModel taskVM in TaskItemViewModels)
            {
                tasks.Add(taskVM.Task);
            }

            tasks = ToDoDataService.ConvertListOfTaskItemVmsToTaskItems(TaskItemViewModels);

            ToDoDataService.SaveTaskItems(tasks);
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
        private void OnTaskSelected(TaskItemViewModel selectedTask)
        {
            SelectedTask = selectedTask;
            IsTaskSelected = selectedTask != null;  // Wenn eine Aufgabe ausgewählt ist, setze IsTaskSelected auf true
        }

        private void OnShellClosed()
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            tasks = ToDoDataService.ConvertListOfTaskItemVmsToTaskItems(TaskItemViewModels);
            ToDoDataService.SaveTaskItems(tasks);
        }

        private void OnSave()
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
            tasks = ToDoDataService.ConvertListOfTaskItemVmsToTaskItems(TaskItemViewModels);
            ToDoDataService.SaveTaskItems(tasks);
        }



        #region DragDrop


        #endregion
    }

    public class ShellClosedEvent : PubSubEvent { }
    public class SaveEvent : PubSubEvent { }
}
