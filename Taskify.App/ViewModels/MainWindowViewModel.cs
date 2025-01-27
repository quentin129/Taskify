using Prism.Mvvm;
using Prism.Regions;
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

        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RequestNavigate("ContentRegion", nameof(Taskify.Modules.ToDo.Views.TaskListView));
        }
    }
}
