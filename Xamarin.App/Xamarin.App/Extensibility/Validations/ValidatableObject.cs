using System.Collections.Generic;
using System.Linq;
using Xamarin.App.ViewModels;

namespace Xamarin.App.Extensibility.Validations
{
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        private readonly List<IValidationRule<T>> validations;
        private List<string> errors;
        private T value;
        private bool isValid;

        public List<IValidationRule<T>> Validations => validations;

        public List<string> Errors
        {
            get => errors;
            set
            {
                errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ValidatableObject()
        {
            isValid = true;
            errors = new List<string>();
            validations = new List<IValidationRule<T>>();
        }

        public ValidatableObject(T value) : this()
        {
            this.value = value;
        }

        public bool Validate()
        {
            Errors.Clear();

            Errors = validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage)
                .ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}