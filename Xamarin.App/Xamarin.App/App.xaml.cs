using System.Threading.Tasks;
using AutoMapper;
using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Services;
using Xamarin.App.ViewModels;
using Xamarin.App.ViewModels.Models;
using Xamarin.App.Views;
using Xamarin.Forms;

namespace Xamarin.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppServiceLocator.Initialize();
            ConfigureMapper();
        }

        protected override async void OnStart()
        {

            base.OnStart();

            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private Task InitNavigation()
        {
            return AppServiceLocator
                .Resolve<INavigationService>()
                .InitializeAsync<LayoutViewModel, MenuViewModel, ProfileViewModel>();
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ToDoItem, ToDoItemModel>();
            });
        }
    }
}