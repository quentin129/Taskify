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
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        private string _selectedViewModel;
        public string SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                if (SetProperty(ref _selectedViewModel, value))
                {
                    _regionManager.RequestNavigate("ContentRegion", value);
                }
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> NavigateCommand { get; }
        public DelegateCommand WindowClosingCommand { get; }
        public DelegateCommand SaveCommand { get; }
        #endregion

        #region Konstruktor
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(view => SelectedViewModel = view);
            WindowClosingCommand = new DelegateCommand(OnWindowClosing);
            SaveCommand = new DelegateCommand(SaveShell);

            SelectedViewModel = nameof(TaskListView); // Standard-View setzen
        }
        #endregion

        #region Funktionen
        private void OnWindowClosing()
        {
            _eventAggregator.GetEvent<ShellClosedEvent>().Publish();
        }

        private void SaveShell()
        {
            _eventAggregator.GetEvent<SaveEvent>().Publish();
        }
        #endregion
    }
}
