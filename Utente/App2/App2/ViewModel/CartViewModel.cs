using App2.Model;
using App2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2.ViewModel
{
    class CartViewModel :BaseViewModel
    {

        public ObservableCollection<UserCartItem> CartItems { get; set; }
        public ObservableCollection<string> Foto{ get; set; }

        private decimal _TotalCost;
        public decimal TotalCost
        {
            set
            {
                _TotalCost = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalCost;
            }
        }

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

        public Command PlaceOrdersCommand { get; set; }

        public CartViewModel()
        {
            CartItems = new ObservableCollection<UserCartItem>();
            Foto = new ObservableCollection<string>();
            LoadItems();
            PlaceOrdersCommand = new Command(async () => await PlaceOrdersAsync());
        }
        private async Task PlaceOrdersAsync()
        {
            var orderService = new OrderService();
            await orderService.PlaceOrderAsync();
        }
        private void LoadItems()
        {
            var Fis = new FoodItemService();
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var items = cn.Table<CartItem>().ToList();
            CartItems.Clear();
            Foto.Clear();
            foreach (var item in items)
            {
                Foto.Add(Fis.GetFoodFotoByProductId(item.ProductId).ToString());
                CartItems.Add(new UserCartItem()
                {
                    CartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Cost = item.Price * item.Quantity,
                    Photo=item.Photo
                });
                TotalCost += (item.Price * item.Quantity);
            }
            TotalFoodItems = CartItems.Count;
        }
    }
}
