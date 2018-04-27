using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.App.Extensibility.Enum;
using Xamarin.Forms;

using Xamarin.App.Extensibility.Services;
using Xamarin.App.ViewModels.Models;

namespace Xamarin.App.ViewModels
{
    public class ToDoItemsViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IToDoItemService toDoItemService;

        public ToDoItemsViewModel(INavigationService navigationService, IToDoItemService toDoItemService)
        {
            this.navigationService = navigationService;
            this.toDoItemService = toDoItemService;

            ItemSelectedCommand = new Command<ToDoItemModel>(HandleItemSelectedAsync);
            RemoveItemCommand = new Command<ToDoItemModel>(HandleRemoveItem);
            ShowOrHideItemDescriptionCommand = new Command<ToDoItemModel>(ShowOrHideItemDescription);
            AddItemCommand = new Command(HandleAddItemAsync);
        }

        public ICommand ItemSelectedCommand { get; }

        public ICommand RemoveItemCommand { get; }

        public ICommand ShowOrHideItemDescriptionCommand { get; }

        public ICommand AddItemCommand { get; }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        private ObservableCollection<ToDoItemModel> items;
        public ObservableCollection<ToDoItemModel> Items
        {
            get => items;
            set
            {
                items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            ToDoItemStatus itemStatus = (ToDoItemStatus)navigationData;
            Title = itemStatus.ToString();

            IsBusy = true;
            Items = GetList(itemStatus);
            IsBusy = false;
        }

        private async void HandleItemSelectedAsync(ToDoItemModel item)
        {
            await navigationService.NavigateToAsync<ToDoItemViewModel>(item.Id);
        }

        private async void HandleAddItemAsync()
        {
            await navigationService.NavigateToAsync<ToDoItemViewModel>();
        }

        private void HandleRemoveItem(ToDoItemModel item)
        {
            Items.Remove(item);
        }

        private void ShowOrHideItemDescription(ToDoItemModel item)
        {
            item.IsDescriptionVisible = !item.IsDescriptionVisible;
            UpdateList(item);
        }

        private void UpdateList(ToDoItemModel item)
        {
            int position = Items.IndexOf(item);
            Items.Remove(item);
            Items.Insert(position, item);
        }

        private ObservableCollection<ToDoItemModel> GetList(ToDoItemStatus status)
        {
            IEnumerable<ToDoItemModel> todoItems = toDoItemService.GetItems(status)
                .Select(item => new ToDoItemModel
                {
                    BulletColor = item.IsDone ? Color.DeepPink : Color.DeepSkyBlue,
                    Name = item.Name,
                    Id = item.Id,
                    IsDone = item.IsDone,
                    Description = item.Description,
                    IsDescriptionVisible = false
                });
            return new ObservableCollection<ToDoItemModel>(todoItems);
        }
    }
}