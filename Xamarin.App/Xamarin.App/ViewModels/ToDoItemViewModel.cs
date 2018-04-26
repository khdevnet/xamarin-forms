using System.Threading.Tasks;
using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Data;
using Xamarin.App.Extensibility.Services;
using Xamarin.Forms;

namespace Xamarin.App.ViewModels
{
    public class ToDoItemViewModel : ViewModelBase
    {
        private readonly IDataContext dataContext;
        private readonly INavigationService navigationService;

        private ToDoItem item;

        public ToDoItemViewModel(IDataContext dataContext, INavigationService navigationService)
        {
            this.dataContext = dataContext;
            this.navigationService = navigationService;
            SaveItemCommand = new Command(SaveItemAsync);
            FinishItemCommand = new Command(FinishItemAsync);
        }
        public ToDoItem Item
        {
            get => item;
            set
            {
                item = value;
                RaisePropertyChanged(() => Item);
            }
        }

        public Command SaveItemCommand { get; }

        public Command FinishItemCommand { get; }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is int)
            {
                IsBusy = true;
                Item = await dataContext.GetItemAsync((int)navigationData);
                IsBusy = false;
            }
        }

        private async void SaveItemAsync()
        {
            await SaveToDoItemAsync();
            await NavigateToProfilePage();
        }

        private async Task SaveToDoItemAsync()
        {
            await dataContext.SaveItemAsync(item);
        }

        private async void FinishItemAsync()
        {
            item.IsDone = true;
            await SaveToDoItemAsync();
            await NavigateToProfilePage();
        }

        private async Task NavigateToProfilePage()
        {
            await navigationService.NavigateToAsync<ProfileViewModel>();
        }
    }
}