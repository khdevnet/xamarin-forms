using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Validations;
using Xamarin.App.Validations;
using Xamarin.Forms;

namespace Xamarin.App.ViewModels.Models
{
    public class ToDoItemDetailModel : ExtendedBindableObject
    {
        public ToDoItemDetailModel(ToDoItem item)
        {
            Id = item.Id;
            IsDone = item.IsDone;

            name = new ValidatableObject<string>
            {
                Value = item.Name
            };
            name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });

            description = new ValidatableObject<string>
            {
                Value = item.Description
            };
            description.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A description is required." });
        }

        public ToDoItemDetailModel()
        {

            name = new ValidatableObject<string>();
            name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });

            description = new ValidatableObject<string>();
            description.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A description is required." });
        }

        public int Id { get; set; }

        private ValidatableObject<string> name;
        public ValidatableObject<string> Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private ValidatableObject<string> description;

        public ValidatableObject<string> Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public bool IsDone { get; set; }
    }
}