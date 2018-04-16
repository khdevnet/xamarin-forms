using AutoMapper;
using Xamarin.App.Data.Models;
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
            ConfigureMapper();
            MainPage = new NavigationPage(new SignInPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
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