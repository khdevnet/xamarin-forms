using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Data;

namespace Xamarin.App.Data
{
    public class DataContext : IDataContext
    {
        private readonly SQLiteAsyncConnection database;

        public DataContext(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ToDoItem>().Wait();

            if (GetItems().Count == 0)
            {
                Items.ForEach(item => database.InsertAsync(item));
            }
        }

        public Task<List<ToDoItem>> GetItemsAsync()
        {
            return database.QueryAsync<ToDoItem>($"SELECT * FROM {nameof(ToDoItem)} ORDER BY IsDone ASC");
        }

        public List<ToDoItem> GetItems()
        {
            return GetItemsAsync().Result;
        }

        public Task<ToDoItem> GetItemAsync(int id)
        {
            return database.Table<ToDoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ToDoItem item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }

            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(ToDoItem item)
        {
            return database.DeleteAsync(item);
        }

        private static readonly List<ToDoItem> Items = new List<ToDoItem>
        {
            new ToDoItem { Name = "Go to the shop", Description = "Socks, apples etc" },
            new ToDoItem { Name = "Wash a car", Description = "Wash a car" },
            new ToDoItem { Name = "Send a gift to friend", Description = "Send a gift to friend" }
        };
    }
}