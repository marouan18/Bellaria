﻿using Admin.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Admin.Services
{
    class UserOrderHistoryService
    {
        FirebaseClient client;
        List<UserOrdersHistory> UserOrders;

        public UserOrderHistoryService()
        {
            client = new FirebaseClient("https://bellaria-10f58-default-rtdb.europe-west1.firebasedatabase.app/");
            UserOrders = new List<UserOrdersHistory>();
        }

        public async Task<List<UserOrdersHistory>> GetOrderDetailsAsync(bool Vecchi)
        {
            var uname = Preferences.Get("Username", "Guest");

            var orders = (await client.Child("Orders")
                .OnceAsync<Order>())
                .Where(o => o.Object.Confermato == Vecchi)
                .Select(o => new Order
                {
                    OrderId = o.Object.OrderId,
                    ReceiptId = o.Object.ReceiptId,
                    TotalCost = o.Object.TotalCost
                }).ToList();

            foreach (var order in orders)
            {
                UserOrdersHistory uoh = new UserOrdersHistory
                {
                    OrderId = order.OrderId,
                    ReceiptId = order.ReceiptId,
                    TotalCost = order.TotalCost
                };

                var orderDetails = (await client.Child("OrderDetails")
                .OnceAsync<OrderDetails>())
                .Where(o => o.Object.OrderId.Equals(order.OrderId))
                .Select(o => new OrderDetails
                {
                    OrderId = o.Object.OrderId,
                    OrderDetailId = o.Object.OrderDetailId,
                    ProductID = o.Object.ProductID,
                    ProductName = o.Object.ProductName,
                    Quantity = o.Object.Quantity,
                    Price = o.Object.Price
                }).ToList();

                uoh.AddRange(orderDetails);

                UserOrders.Add(uoh);
            }

            return UserOrders;
        }
    }
}
