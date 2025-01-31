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

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value, nameof(Title));
        }

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand<TaskItemViewModel> CloseDialogCommand { get; }

        public TaskDetailViewModel()
        {
            CloseDialogCommand = new DelegateCommand<TaskItemViewModel>(CloseDialog);
        }

        private void CloseDialog(object parameter)
        {
            var result = new DialogResult(ButtonResult.None);

           
            
                result = new DialogResult(ButtonResult.OK, new DialogParameters
                     {
                         { "Task", Task } // Hier kannst du Werte zurückgeben
                     });
            

            RequestClose?.Invoke(result);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Task = parameters.GetValue<TaskItemViewModel>("Task");
            Title = Task.Title;
        }
    }
}