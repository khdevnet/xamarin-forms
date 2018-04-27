using System.Threading.Tasks;
using Xamarin.App.Extensibility.Services;

namespace Xamarin.App.ViewModels
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly INavigationService NavigationService;

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;

            set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            NavigationService = AppServiceLocator.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}