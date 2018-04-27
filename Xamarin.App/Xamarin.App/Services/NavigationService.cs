using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.App.Extensibility.Services;
using Xamarin.App.ViewModels;
using Xamarin.App.Views;
using Xamarin.Forms;

namespace Xamarin.App.Services
{
    public class NavigationService : INavigationService
    {
        private Type layoutViewModelType;
        private Type menuViewModelType;

        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                Page mainPage = Application.Current.MainPage;
                //var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return mainPage.BindingContext as ViewModelBase;
            }
        }


        public Task InitializeAsync<TLayoutViewModel, TMenuViewModel, TViewModel>()
            where TLayoutViewModel : ViewModelBase
            where TMenuViewModel : ViewModelBase
            where TViewModel : ViewModelBase
        {
            layoutViewModelType = typeof(TLayoutViewModel);
            menuViewModelType = typeof(TMenuViewModel);
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            //var mainPage = Application.Current.MainPage as CustomNavigationView;

            //if (mainPage != null)
            //{
            //    mainPage.Navigation.RemovePage(
            //        mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            //}

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            //var mainPage = Application.Current.MainPage as CustomNavigationView;

            //if (mainPage != null)
            //{
            //    for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
            //    {
            //        var page = mainPage.Navigation.NavigationStack[i];
            //        mainPage.Navigation.RemovePage(page);
            //    }
            //}

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page layoutPage = CreatePage(layoutViewModelType, parameter);
            Page menuPage = CreatePage(menuViewModelType, parameter);
            Page page = CreatePage(viewModelType, parameter);

            if (layoutPage is MasterDetailPage masterPage)
            {
                if (masterPage.Detail is NavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    masterPage.Master = menuPage;
                    masterPage.Detail = new NavigationPage(page);
                    Application.Current.MainPage = masterPage;
                }
            }

            await (layoutPage.BindingContext as ViewModelBase).InitializeAsync(parameter);
            await (menuPage.BindingContext as ViewModelBase).InitializeAsync(parameter);
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            string viewName = viewModelType.FullName.Replace("Model", string.Empty);
            string viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            string viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            Type viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            return Activator.CreateInstance(pageType) as Page;
        }
    }
}