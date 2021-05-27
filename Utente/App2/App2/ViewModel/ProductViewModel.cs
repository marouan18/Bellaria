using System;
using App2.Model;
using App2.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using App2.Views;

namespace App2.ViewModel
{
    class ProductViewModel : BaseViewModel
    {
        private string _UserName;
        public string UserName
        {
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }

            get
            {
                return _UserName;
            }
        }

        private int _UserCartItemsCount;
        public int UserCartItemsCount
        {
            set
            {
                _UserCartItemsCount = value;
                OnPropertyChanged();
            }

            get
            {
                return _UserCartItemsCount;
            }
        }

        private string _SearchText;
        public string SearchText
        {
            set
            {
                _SearchText = value;
                OnPropertyChanged();
            }

            get
            {
                return _SearchText;
            }
        }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<FoodItem> LatestItems { get; set; }
        public List<Menu> MyMenu { get; set; }

        public Command ViewCartCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command ViewOrdersHistoryCommand { get; set; }
        public Command SearchViewCommand { get; set; }

        public ProductViewModel()
        {
            var uname = Preferences.Get("Username", String.Empty);
            if (String.IsNullOrEmpty(uname))
                UserName = "Guest";
            else
                UserName = uname;

            UserCartItemsCount = new CartItemService().GetUsercartCount();
            MyMenu = GetMenus();
            Categories = new ObservableCollection<Category>();
            LatestItems = new ObservableCollection<FoodItem>();

            ViewCartCommand = new Command(async () => await ViewCartAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
            ViewOrdersHistoryCommand = new Command(async () => await ViewOrderHistoryAsync());
            SearchViewCommand = new Command(async () => await SearchViewAsync());

            GetCategories();
            GetLatestItems();
        }
        private List<Menu> GetMenus()
        {
            return new List<Menu>
            {
                new Menu{ Name = "Home", Icon = "home.png"},
                new Menu{ Name = "Profile", Icon = "user.png"},
                new Menu{ Name = "Cronologia Ordini", Icon = "bell.png"},
                new Menu{ Name = "Logout", Icon = "envelope.png"},
                new Menu{ Name = "My Tasks", Icon = "tasks.png"},
            };
        }
        private async Task SearchViewAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(
                new SearchResultsView(SearchText));
        //    await Application.Current.MainPage.DisplayAlert("Success", "User Registered", "OK");
        }

        private async Task ViewOrderHistoryAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersHistoryView());
        }

        private async Task ViewCartAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private async Task LogoutAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
        }

        private async void GetCategories()
        {
            var data = await new CategoryDataService().GetCategoriesAsync();
            Categories.Clear();
            foreach (var item in data)
            {
                Categories.Add(item);
            }
        }

        private async void GetLatestItems()
        {
            var data = await new FoodItemService().GetLatestFoodItem();
            LatestItems.Clear();
            foreach (var item in data)
            {
                LatestItems.Add(item);
            }
        }
    }
    public class Menu
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }

}
