using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Taskify.Modules.ToDo.Models;
using Taskify.Modules.ToDo.ViewModels;
using Taskify.Modules.ToDo.Views;

namespace Taskify.Modules.ToDo
{
    public class ToDoModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ToDoModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(TaskListView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TaskListView, TaskListViewModel>();
            containerRegistry.RegisterForNavigation<TaskItemView, TaskItemViewModel>();
            containerRegistry.RegisterDialog<TaskDetailView, TaskDetailViewModel>();

            containerRegistry.RegisterDialogWindow<CustomDialogWindow>();
        }
    }
}
