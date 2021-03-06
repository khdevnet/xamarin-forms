﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.App.Data.Models;
using Xamarin.App.Extensibility.Enum;
using Xamarin.Forms;

using Xamarin.App.Extensibility.Services;
using Xamarin.App.ViewModels.Models;

namespace Xamarin.App.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private const int TopRemainingItemsCount = 5;
        private readonly INavigationService navigationService;
        private readonly IToDoItemService toDoItemService;

        public ProfileViewModel(INavigationService navigationService, IToDoItemService toDoItemService)
        {
            this.navigationService = navigationService;
            this.toDoItemService = toDoItemService;

            ItemSelectedCommand = new Command<ToDoItemModel>(HandleItemSelectedAsync);
            OpenToDoItemsCommand = new Command<ToDoItemStatus>(OpenToDoItems);
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

        public ICommand OpenToDoItemsCommand { get; }

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
            Items = GetTopList();
            IEnumerable<ToDoItem> allItems = toDoItemService.GetItems().ToArray();
            RemainingTasksCount = allItems.Count(item => !item.IsDone);
            CompletedTasksCount = allItems.Count(item => item.IsDone);
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

        private async void OpenToDoItems(ToDoItemStatus itemStatus)
        {
            await navigationService.NavigateToAsync<ToDoItemsViewModel>(itemStatus);
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

        private ObservableCollection<ToDoItemModel> GetTopList()
        {
            IEnumerable<ToDoItemModel> todoItems = toDoItemService.GetTopItems(TopRemainingItemsCount)
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