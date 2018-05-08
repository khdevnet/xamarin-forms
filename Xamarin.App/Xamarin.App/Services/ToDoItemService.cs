using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Data;
using Xamarin.App.Extensibility.Enum;
using Xamarin.App.Extensibility.Services;

namespace Xamarin.App.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemsDataContext datacontext;

        public ToDoItemService(IToDoItemsDataContext datacontext)
        {
            this.datacontext = datacontext;
        }

        public IEnumerable<ToDoItem> GetItems()
        {
            return datacontext.GetItems();
        }

        public IEnumerable<ToDoItem> GetItems(ToDoItemStatus status)
        {
            List<ToDoItem> items = datacontext.GetItems();
            return status == ToDoItemStatus.Remaining
                ? items.Where(item => !item.IsDone)
                : items.Where(item => item.IsDone);
        }

        public async Task<ToDoItem> GetItemAsync(int id)
        {
            return await datacontext.GetItemAsync(id);
        }

        public IEnumerable<ToDoItem> GetTopItems(int count)
        {
            return datacontext.GetItems()
                .OrderBy(item => item.IsDone)
                .ThenByDescending(item => item.Id)
                .Take(count)
                .ToList();
        }

        public async Task<int> SaveItemAsync(ToDoItem item)
        {
            return await datacontext.SaveItemAsync(item);
        }
    }
}