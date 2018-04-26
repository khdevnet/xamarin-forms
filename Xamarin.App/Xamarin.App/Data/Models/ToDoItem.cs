using SQLite;

namespace Xamarin.App.Data.Models
{
    public class ToDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsDone { get; set; } = false;

        public string Name { get; set; }

        public string Description { get; set; }
    }
}