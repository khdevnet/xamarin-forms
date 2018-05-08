using System;
using System.Threading.Tasks;
using AutoMapper;
using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Services;
using Xamarin.App.ViewModels;
using Xamarin.App.ViewModels.Models;
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
                .InitializeAsync<LayoutViewModel, MenuViewModel, SignInViewModel>();
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ToDoItem, ToDoItemModel>();
                cfg.CreateMap<ToDoItem, ToDoItemDetailModel>()
                   .ConvertUsing(src => new ToDoItemDetailModel
                   {
                       Id = src.Id,
                       IsDone = src.IsDone,
                       Name = { Value = src.Name },
                       Description = { Value = src.Description }
                   });

                cfg.CreateMap<ToDoItemDetailModel, ToDoItem>()
                   .ConvertUsing(src => new ToDoItem
                   {
                       Id = src.Id,
                       IsDone = src.IsDone,
                       Name = src.Name?.Value ?? String.Empty,
                       Description = src.Description?.Value ?? String.Empty
                   });
            });
        }
    }
}