using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace Xamarin.App.ViewModels
{
    public class ViewModelLocator
    {
        public ProfileViewModel ProfileViewModel { get; } = AppServiceLocator.Resolve<ProfileViewModel>();

        public ToDoItemViewModel ToDoItemViewModel { get; } = AppServiceLocator.Resolve<ToDoItemViewModel>();

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }
        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
            {
                return;
            }

            Type viewType = view.GetType();
            string viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            string viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            Type viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            object viewModel = AppServiceLocator.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}