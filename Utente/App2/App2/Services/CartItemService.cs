using App2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App2.Services
{
    class CartItemService
    {
        public int GetUsercartCount()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var count = cn.Table<CartItem>().Count();
            cn.Close();
            return count;
        }

        public void RemoveItemsFromCart()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            cn.DeleteAll<CartItem>();
            cn.Commit();
            cn.Close();
        }
    }
}
