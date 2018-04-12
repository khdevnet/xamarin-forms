using System;
using Xamarin.App.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.App.Models;
using Xamarin.App.ViewModels;
namespace Xamarin.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoItemPage : ContentPage
    {
        private readonly ToDoItem item;

        public ToDoItemPage() { }

        public ToDoItemPage(ToDoItem item)
        {
            this.item = item;
            InitializeComponent();
            BindingContext = new ToDoItemViewModel(item);

        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (!DataContext.ToDoItems.Contains(item))
            {
                DataContext.ToDoItems.Add(item);
            }
            await Navigation.PushAsync(new MainPage());
        }
    }
}