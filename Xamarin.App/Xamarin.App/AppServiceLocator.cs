using System;
using CommonServiceLocator;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using Xamarin.App.Common;
using Xamarin.App.Data;
using Xamarin.App.Extensibility.Common;
using Xamarin.App.Extensibility.Data;
using Xamarin.App.Extensibility.Services;
using Xamarin.App.Services;
using Xamarin.Forms;

namespace Xamarin.App
{
    public static class AppServiceLocator
    {
        public static void Initialize()
        {
            string dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3");
            UnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterInstance<IToDoItemsDataContext>(new ToDoItemsDataContext(dbPath));
            unityContainer.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IToDoItemService, ToDoItemService>();
            unityContainer.RegisterType<IEntityMapper, EntityMapper>();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
        }

        public static TInstance Resolve<TInstance>()
        {
            return ServiceLocator.Current.GetInstance<TInstance>();
        }

        public static object Resolve(Type type)
        {
            return ServiceLocator.Current.GetInstance(type);
        }
    }
}