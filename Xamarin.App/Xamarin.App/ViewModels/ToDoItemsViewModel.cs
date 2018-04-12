using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.App.Data;
using Xamarin.App.Models;
using Xamarin.App.Views;
using Xamarin.Forms;

namespace Xamarin.App.ViewModels
{
    public class ToDoItemsViewModel : INotifyPropertyChanged
    {
        private readonly INavigation navigation;

        public ToDoItemsViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            ItemSelectedCommand = new Command<ToDoItem>(HandleItemSelectedAsync);
            RemoveItemCommand = new Command<ToDoItem>(HandleRemoveItem);
            AddItemCommand = new Command(HandleAddItemAsync);
        }

        public ICommand ItemSelectedCommand { get; }

        public ICommand RemoveItemCommand { get; }

        public ICommand AddItemCommand { get; }

        public ObservableCollection<ToDoItem> Items { get; set; } = new ObservableCollection<ToDoItem>(DataContext.ToDoItems);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async void HandleItemSelectedAsync(ToDoItem item)
        {
            await navigation.PushAsync(new ToDoItemPage(item));
        }

        private async void HandleAddItemAsync()
        {
            await navigation.PushAsync(new ToDoItemPage(new ToDoItem()));
        }

        private void HandleRemoveItem(ToDoItem item)
        {
            Items.Remove(item);
        }
    }
}