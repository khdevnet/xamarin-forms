using System.Collections.ObjectModel;
using Xamarin.App.Extensibility.Services;
using Xamarin.App.ViewModels.Models;
using Xamarin.Forms;

namespace Xamarin.App.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public MenuViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            MenuItems = new ObservableCollection<MenuItemModel>(new[]
            {
                new MenuItemModel { Id = 0, Title = "Profile", TargetType = typeof(ProfileViewModel) },
                new MenuItemModel { Id = 1, Title = "Dashboard" },
                new MenuItemModel { Id = 2, Title = "Stats" },
                new MenuItemModel { Id = 3, Title = "Log Out" },
            });

            NavigateToPageCommand = new Command<MenuItemModel>(NavigateToPageAsync);
        }

        public Command NavigateToPageCommand { get; }

        private ObservableCollection<MenuItemModel> menuItems;
        public ObservableCollection<MenuItemModel> MenuItems
        {
            get => menuItems;
            set
            {
                menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }

        private async void NavigateToPageAsync(MenuItemModel menuItem)
        {
           await navigationService.NavigateToAsync(menuItem.TargetType);
        }
    }
}