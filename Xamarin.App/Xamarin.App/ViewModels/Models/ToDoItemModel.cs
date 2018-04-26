using Xamarin.Forms;

namespace Xamarin.App.ViewModels.Models
{
    public class ToDoItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDescriptionVisible { get; set; } = false;

        public bool IsDone { get; set; } = false;

        public Color BulletColor { get; set; } = Color.BlueViolet;
    }
}