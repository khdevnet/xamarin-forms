using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using Xamarin.App.Extensibility.Data;
using Xamarin.App.Extensibility.Services;
using Xamarin.App.ViewModels.Models;

namespace Xamarin.App.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IDataContext dataContext;

        public ProfileViewModel(INavigationService navigationService, IDataContext dataContext)
        {
            this.navigationService = navigationService;
            this.dataContext = dataContext;

            ItemSelectedCommand = new Command<ToDoItemModel>(HandleItemSelectedAsync);
            RemoveItemCommand = new Command<ToDoItemModel>(HandleRemoveItem);
            ShowOrHideItemDescriptionCommand = new Command<ToDoItemModel>(ShowOrHideItemDescription);
            AddItemCommand = new Command(HandleAddItemAsync);
        }

        private int completedTasksCount;
        public int CompletedTasksCount
        {
            get => completedTasksCount;
            set
            {
                completedTasksCount = value;
                RaisePropertyChanged(() => CompletedTasksCount);
            }
        }

        private int remainingTasksCount;
        public int RemainingTasksCount
        {
            get => remainingTasksCount;
            set
            {
                remainingTasksCount = value;
                RaisePropertyChanged(() => RemainingTasksCount);
            }
        }

        public ICommand ItemSelectedCommand { get; }

        public ICommand RemoveItemCommand { get; }

        public ICommand ShowOrHideItemDescriptionCommand { get; }

        public ICommand AddItemCommand { get; }

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

            IsBusy = true;
            Items = GetList();
            RemainingTasksCount = Items?.Count(item => !item.IsDone) ?? 0;
            CompletedTasksCount = Items?.Count(item => item.IsDone) ?? 0;
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

        private ObservableCollection<ToDoItemModel> GetList()
        {
            IEnumerable<ToDoItemModel> todoItems = dataContext.GetItems().Select(item => new ToDoItemModel
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