using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using App2.Model;
using App2.Views;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace App2.Services
{
    class OrderService
    {
        FirebaseClient client;
        public OrderService()
        {
            client = new FirebaseClient("");

        }
        public async Task<string> PlaceOrderAsync()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var data = cn.Table<CartItem>().ToList();

            var orderId = Guid.NewGuid().ToString();
            var uname = Preferences.Get("Username", "Guest");
            decimal totalcost = 0;
            foreach(var item in data)
            {
                OrderDetails od = new OrderDetails()
                {
                    OrderId = orderId,
                    OrderDetailId = Guid.NewGuid().ToString(),
                    ProductID = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                totalcost += item.Quantity * item.Price;
                await client.Child("OrderDetails").PostAsync(od);

            }
            await client.Child("Orders").PostAsync(
             new Order()
             {
                 OrderId = orderId,
                 Username = uname,
                 TotalCost = totalcost
             });
            return orderId;

        }
    }
}
