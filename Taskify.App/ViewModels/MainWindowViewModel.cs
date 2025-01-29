using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Taskify.Modules.ToDo;
using Taskify.Modules.ToDo.Views;

namespace Taskify.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Taskify";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand _windowClosingCommand;
        public DelegateCommand WindowClosingCommand =>
            _windowClosingCommand ??= new DelegateCommand(OnWindowClosing);

        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RequestNavigate("ContentRegion", nameof(Taskify.Modules.ToDo.Views.TaskListView));
        }

        private void OnWindowClosing()
        {
            // Publish Event um im TaskListViewModel das speichern anzustoßen
        }
    }
}
