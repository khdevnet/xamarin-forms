using System;
using Xamarin.Forms;
using Xamarin.App.ViewModels;

namespace Xamarin.App.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ToDoItemsViewModel(Navigation);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}