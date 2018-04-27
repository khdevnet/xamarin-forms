using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.App.ViewModels.Models;

namespace Xamarin.App.ViewModels
{
    public class LayoutViewMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MenuItemModel> MenuItems { get; set; }

        public LayoutViewMasterViewModel()
        {
            MenuItems = new ObservableCollection<MenuItemModel>(new[]
            {
                new MenuItemModel { Id = 0, Title = "Profile" },
                new MenuItemModel { Id = 1, Title = "Dashboard" },
                new MenuItemModel { Id = 2, Title = "Stats" },
                new MenuItemModel { Id = 3, Title = "Log Out" },
            });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}