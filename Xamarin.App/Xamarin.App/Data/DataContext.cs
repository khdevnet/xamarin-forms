using System.Collections.Generic;

using Xamarin.App.Data.Models;

namespace Xamarin.App.Data
{
    public static class DataContext
    {
        private static readonly List<ToDoItem> items = new List<ToDoItem>
        {
            new ToDoItem { Name = "Go to the shop", Description = "Socks, apples etc" },
            new ToDoItem { Name = "Wash a car", Description = "Wash a car" },
            new ToDoItem { Name = "Send a gift to friend", Description = "Send a gift to friend" }
        };

        public static List<ToDoItem> ToDoItems { get; } = items;
    }
}