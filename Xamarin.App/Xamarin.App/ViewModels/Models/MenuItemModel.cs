using System;
using Xamarin.App.Views;

namespace Xamarin.App.ViewModels.Models
{

    public class MenuItemModel
    {
        public MenuItemModel()
        {
            TargetType = typeof(ProfileView);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}