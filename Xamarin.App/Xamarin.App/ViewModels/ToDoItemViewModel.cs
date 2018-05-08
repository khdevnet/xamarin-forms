using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Common;
using Xamarin.App.Extensibility.Services;
using Xamarin.App.ViewModels.Models;
using Xamarin.Forms;

namespace Xamarin.App.ViewModels
{
    public class ToDoItemViewModel : ViewModelBase
    {
        private readonly IToDoItemService toDoItemService;
        private readonly INavigationService navigationService;
        private readonly IEntityMapper entityMapper;

        public ToDoItemViewModel(
            IToDoItemService toDoItemService,
            INavigationService navigationService,
            IEntityMapper entityMapper)
        {
            this.toDoItemService = toDoItemService;
            this.navigationService = navigationService;
            this.entityMapper = entityMapper;
            item = new ToDoItemDetailModel();
        }

        private ToDoItemDetailModel item;
        public ToDoItemDetailModel Item
        {
            get => item;
            set
            {
                item = value;
                RaisePropertyChanged(() => Item);
            }
        }

        private bool isValid;
        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        private bool canFinish;
        public bool CanFinish
        {
            get => canFinish;
            set
            {
                canFinish = value;
                RaisePropertyChanged(() => CanFinish);
            }
        }

        #region Commands

        public ICommand ValidateCommand => new Command(() => IsValid = Validate());

        public Command SaveItemCommand => new Command(SaveItemAsync);

        public Command FinishItemCommand => new Command(FinishItemAsync);
        #endregion commands

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            ToDoItem itemData = await (navigationData is int id
                ? toDoItemService.GetItemAsync(id)
                : Task.FromResult(new ToDoItem()));

            Item = entityMapper.Map<ToDoItem, ToDoItemDetailModel>(itemData);

            CanFinish = Item.Id > 0;
            IsValid = Validate();

            IsBusy = false;
        }

        private bool Validate()
        {
            Item.Name.Validate();
            Item.Description.Validate();
            return Item.Name.IsValid && Item.Description.IsValid;
        }

        private async void SaveItemAsync()
        {
            await SaveToDoItemAsync();
            await NavigateToProfilePage();
        }

        private async void FinishItemAsync()
        {
            item.IsDone = true;
            await SaveToDoItemAsync();
            await NavigateToProfilePage();
        }

        private async Task SaveToDoItemAsync()
        {
            await toDoItemService.SaveItemAsync(entityMapper.Map<ToDoItemDetailModel, ToDoItem>(item));
        }

        private async Task NavigateToProfilePage()
        {
            await navigationService.NavigateToAsync<ProfileViewModel>();
        }
    }
}