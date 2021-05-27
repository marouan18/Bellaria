using System;
using System.Collections.ObjectModel;
using App2.Model;
using App2.Services;

namespace App2.ViewModel
{
    class SearchResultsViewModel : BaseViewModel
    {
        public ObservableCollection<FoodItem> FoodItemsByQuery { get; set; }

        private int _TotalFoodItems;
        public int TotalFoodItems
        {
            set
            {
                _TotalFoodItems = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalFoodItems;
            }
        }

        public SearchResultsViewModel(string searchText)
        {
            FoodItemsByQuery = new ObservableCollection<FoodItem>();
            GetFoodItemsByQuery(searchText);
        }

        private async void GetFoodItemsByQuery(string searchText)
        {
            var data = await new FoodItemService().GetFoodItemsByQueryAsync(searchText);
            FoodItemsByQuery.Clear();
            foreach (var item in data)
            {
                FoodItemsByQuery.Add(item);
            }
            TotalFoodItems = FoodItemsByQuery.Count;
        }
    }
}
