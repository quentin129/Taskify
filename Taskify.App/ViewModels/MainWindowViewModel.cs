using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Taskify.Modules.ToDo.ViewModels;
using Taskify.Modules.ToDo.Views;

namespace Taskify.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Properties
        private IEventAggregator _eventAggregator;
        private IRegionManager _regionManager;

        private string _currentView;
        public string CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView, value, nameof(CurrentView)); }
        }
        #endregion

        #region Commands
        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ??= new DelegateCommand<string>(Navigate);

        private DelegateCommand _windowClosingCommand;
        public DelegateCommand WindowClosingCommand =>
            _windowClosingCommand ??= new DelegateCommand(OnWindowClosing);

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ??= new DelegateCommand(SaveShell);
        #endregion

        #region Konstruktor
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Navigate(nameof(TaskListView));
        }
        #endregion

        #region Funktionen
        private void Navigate(string viewName)
        {
            _regionManager.RequestNavigate("ContentRegion", viewName);
            CurrentView = viewName;
        }

        private void OnWindowClosing()
        {
            // Publish Event um im TaskListViewModel das speichern anzustoßen
            _eventAggregator.GetEvent<ShellClosedEvent>().Publish();
        }

        private void SaveShell()
        {
            // Publish Events Content zu saven
            _eventAggregator.GetEvent<SaveEvent>().Publish();
        }
        #endregion
    }
}
