﻿using System.Threading.Tasks;
using Xamarin.App.ViewModels;

namespace Xamarin.App.Extensibility.Services
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}