using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AutoMapper;

using Xamarin.App.Data;
using Xamarin.App.Data.Models;
using Xamarin.App.ViewModels.Models;
using Xamarin.App.Views;
using Xamarin.Forms;

namespace Xamarin.App.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly INavigation navigation;

        public ProfileViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            ItemSelectedCommand = new Command<ToDoItemModel>(HandleItemSelectedAsync);
            RemoveItemCommand = new Command<ToDoItemModel>(HandleRemoveItem);
            ShowOrHideItemDescriptionCommand = new Command<ToDoItemModel>(ShowOrHideItemDescription);
            AddItemCommand = new Command(HandleAddItemAsync);
        }

        public ICommand ItemSelectedCommand { get; }

        public ICommand RemoveItemCommand { get; }

        public ICommand ShowOrHideItemDescriptionCommand { get; }

        public ICommand AddItemCommand { get; }

        public ObservableCollection<ToDoItemModel> Items { get; set; } =
            new ObservableCollection<ToDoItemModel>(Mapper.Map<IEnumerable<ToDoItemModel>>(DataContext.ToDoItems));

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async void HandleItemSelectedAsync(ToDoItemModel item)
        {
            await navigation.PushAsync(new ToDoItemPage(Mapper.Map<ToDoItem>(item)));
            item = null;
        }

        private async void HandleAddItemAsync()
        {
            await navigation.PushAsync(new ToDoItemPage(new ToDoItem()));
        }

        private void HandleRemoveItem(ToDoItemModel item)
        {
            Items.Remove(item);
        }

        private void ShowOrHideItemDescription(ToDoItemModel item)
        {
            item.IsDescriptionVisible = !item.IsDescriptionVisible;
            UpdateList(item);
        }

        private void UpdateList(ToDoItemModel item)
        {
            int position = Items.IndexOf(item);
            Items.Remove(item);
            Items.Insert(position, item);
        }
    }
}