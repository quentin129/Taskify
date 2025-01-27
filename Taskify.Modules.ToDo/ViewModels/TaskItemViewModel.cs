using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using Taskify.Modules.ToDo.Models;

namespace Taskify.Modules.ToDo.ViewModels
{
    public class TaskItemViewModel : BindableBase
    {
        #region Properties
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;
        public TaskItem Task { get; private set; }

        public string Title
        {
            get => Task.Title;
            set
            {
                if (Task.Title != value)
                {
                    Task.Title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public string Description
        {
            get => Task.Description;
            set
            {
                if (Task.Description != value)
                {
                    Task.Description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        public bool IsCompleted
        {
            get => Task.IsCompleted;
            set
            {
                if (Task.IsCompleted != value)
                {
                    Task.IsCompleted = value;
                    RaisePropertyChanged(nameof(IsCompleted));
                }
            }
        }

        public DateTime Deadline
        {
            get => Task.Deadline;
            set
            {
                if (Task.Deadline != value)
                {
                    Task.Deadline = value;
                    RaisePropertyChanged(nameof(Deadline));
                }
            }
        }

        public Priority Priority
        {
            get => Task.Priority;
            set
            {
                if (Task.Priority != value)
                {
                    Task.Priority = value;
                    RaisePropertyChanged(nameof(Priority));
                }
            }
        }

        private DelegateCommand _deleteTaskCommand;
        public DelegateCommand DeleteTaskCommand =>
            _deleteTaskCommand ??= new DelegateCommand(DeleteTask);

        private DelegateCommand<TaskItemViewModel> _openDetailsCommand;
        public DelegateCommand<TaskItemViewModel> OpenDetailsCommand =>
            _openDetailsCommand ??= new DelegateCommand<TaskItemViewModel>(OpenDetailView);
        #endregion

        public TaskItemViewModel(TaskItem task, IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService)); ;
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator)); ;
            Task = task ?? throw new ArgumentNullException(nameof(task));
        }

        public void DeleteTask()
        {
            _eventAggregator.GetEvent<TaskDeletedEvent>().Publish(this);
        }

        public void OpenDetailView(object parameter)
        {
            if (parameter is TaskItemViewModel taskItemViewModel)
            {
                var parameters = new DialogParameters { { "Task", taskItemViewModel } };

                _dialogService.ShowDialog("TaskDetailView",parameters, result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        string userInput = result.Parameters.GetValue<string>("UserInput");
                        // Bearbeite das Ergebnis
                    }
                });
            }
        }
    }

    public class TaskDeletedEvent : PubSubEvent<TaskItemViewModel> { }
}
