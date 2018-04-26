using System;
using CommonServiceLocator;
using Unity;
using Unity.ServiceLocation;
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
            unityContainer.RegisterInstance<IDataContext>(new DataContext(dbPath));
            unityContainer.RegisterType<INavigationService, NavigationService>();

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