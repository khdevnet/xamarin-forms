using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.App.Extensibility.Services;
using Xamarin.Forms;

namespace Xamarin.App.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public SignInViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public ICommand OpenProfileCommand => new Command(OpenProfileAsync);

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            IsBusy = false;
        }

        private void OpenProfileAsync()
        {
            navigationService.NavigateToAsync<ProfileViewModel>();
        }
    }
}