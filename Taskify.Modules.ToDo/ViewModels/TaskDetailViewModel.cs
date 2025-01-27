using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Taskify.Modules.ToDo.Models;
using Taskify.Modules.ToDo.Services;
using Taskify.Modules.ToDo.Views;

namespace Taskify.Modules.ToDo.ViewModels
{
    public class TaskDetailViewModel : BindableBase, IDialogAware
    {
        private TaskItemViewModel _task;
        public TaskItemViewModel Task
        {
            get => _task;
            set => SetProperty(ref _task, value);
        }

        public string Title => "Task Detail";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand<string> CloseDialogCommand { get; }

        public TaskDetailViewModel()
        {
            CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
        }

        private void CloseDialog(string parameter)
        {
            var result = new DialogResult(ButtonResult.None);
            if (bool.TryParse(parameter, out var isConfirmed) && isConfirmed)
            {
                result = new DialogResult(ButtonResult.OK, new DialogParameters { { "Task", parameter } });
            }
            RequestClose?.Invoke(result);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Task = parameters.GetValue<TaskItemViewModel>("Task");
        }
    }
}