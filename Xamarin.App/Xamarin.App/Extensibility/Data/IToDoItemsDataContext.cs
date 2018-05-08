using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.App.Data.Models;

namespace Xamarin.App.Extensibility.Data
{
    public interface IToDoItemsDataContext
    {
        Task<List<ToDoItem>> GetItemsAsync();

        List<ToDoItem> GetItems();

        Task<ToDoItem> GetItemAsync(int id);

        Task<int> SaveItemAsync(ToDoItem item);

        Task<int> DeleteItemAsync(ToDoItem item);
    }
}