namespace Xamarin.App.ViewModels.Models
{
    public class ToDoItemModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDescriptionVisible { get; set; } = false;
    }
}