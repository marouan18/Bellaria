using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Model;
using Admin.Services;
using Admin.Views;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Admin.ViewModel
{
    class OrderDetailsModel : BaseViewModel
    {
        private string _OrderId;
        public string OrderId
        {
            set
            {
                _OrderId = value;
                OnPropertyChanged();
            }

            get
            {
                return _OrderId;
            }
        }

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


        public ObservableCollection<OrderDetails> Items { set; get; }
        FirebaseClient client;
        List<OrderDetails> UserOrders;
        public Command CompleteOrder { get; set; }
        public OrderDetailsModel() { }
        public OrderDetailsModel(string Id)
        {
            OrderId = Id;
            CompleteOrder = new Command(async () => await CompleteOrderAsync());
            client = new FirebaseClient("https://bellaria-10f58-default-rtdb.europe-west1.firebasedatabase.app/");
            UserOrders = new List<OrderDetails>();
            Items = new ObservableCollection<OrderDetails>();
            LoadItems(Id);
        }
        private async Task CompleteOrderAsync()
        {
            var order = (await client.Child("Orders")
              .OnceAsync<Order>())
              .Where(o => o.Object.Confermato == false && o.Object.OrderId == OrderId)
              .FirstOrDefault();
            if (order != null)
            {
                order.Object.Confermato = true;
                await client.Child("Orders").Child(order.Key).PutAsync(order.Object);

                await Application.Current.MainPage.Navigation.PushModalAsync(new NewOrdersView());
            }
        }
        public async Task<List<OrderDetails>> GetOrderDetailsAsync()
        {
            var Fis = new FoodItemService();
            var orders = (await client.Child("Orders")
                .OnceAsync<Order>())
                .Where(o => o.Object.Confermato == false && o.Object.OrderId==OrderId)
                .Select(o => new Order
                {
                    OrderId = o.Object.OrderId,
                    ReceiptId = o.Object.ReceiptId,
                    TotalCost = o.Object.TotalCost
                }).ToList();

            foreach (var order in orders)
            {
                var orderDetails = (await client.Child("OrderDetails")
                .OnceAsync<OrderDetails>())
                .Where(o => o.Object.OrderId.Equals(order.OrderId))
                .Select( o =>  new OrderDetails
                {
                    OrderId = o.Object.OrderId,
                    OrderDetailId = o.Object.OrderDetailId,
                    ProductID = o.Object.ProductID,
                    ProductName = o.Object.ProductName,
                    Quantity = o.Object.Quantity,
                    Price = o.Object.Price,
                  
                }).ToList();

                foreach (var ordine in orderDetails)
                {
                    var prova = await Fis.GetFoodFotoByProductId(ordine.ProductID);
                    ordine.Immagine = prova;
                    UserOrders.Add(ordine);
                }
            }

            return UserOrders;
        }
        private async void LoadItems(string Id)
        {
            var items = await GetOrderDetailsAsync();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
                TotalCost += (item.Price * item.Quantity);
            }
            TotalFoodItems = Items.Count;
        }
    }
}
