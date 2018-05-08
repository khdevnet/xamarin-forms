using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Enum;

namespace Xamarin.App.Extensibility.Services
{
    public interface IToDoItemService
    {
        IEnumerable<ToDoItem> GetTopItems(int count);

        IEnumerable<ToDoItem> GetItems();

        IEnumerable<ToDoItem> GetItems(ToDoItemStatus status);

        Task<ToDoItem> GetItemAsync(int id);

        Task<int> SaveItemAsync(ToDoItem item);
    }
}