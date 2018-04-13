using Xamarin.App.Data.Models;

namespace Xamarin.App.ViewModels
{
    public class ToDoItemViewModel
    {
        public ToDoItemViewModel(ToDoItem item)
        {
            Item = item;
        }

        public ToDoItem Item { get; }

    }
}