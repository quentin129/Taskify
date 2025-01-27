using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using Taskify.App.Views;
using Taskify.Modules.ToDo;
using Taskify.Modules.ToDo.ViewModels;
using Taskify.Modules.ToDo.Views;

namespace Taskify.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TaskListView, TaskListViewModel>();
            containerRegistry.RegisterForNavigation<TaskItemView, TaskItemViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ToDoModule>();
        }
    }
}
