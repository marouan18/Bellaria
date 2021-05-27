using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Admin.Model;
using Admin.Services;
using Xamarin.Forms;
namespace Admin.ViewModel
{
    class OrdersView : BaseViewModel
    {
        public ObservableCollection<UserOrdersHistory> OrderDetails { get; set; }

        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }

            get
            {
                return _IsBusy;
            }
        }

        public OrdersView(bool Vecchi)
        {
            OrderDetails = new ObservableCollection<UserOrdersHistory>();
            LoadUserOrders(Vecchi);
        }

        private async void LoadUserOrders(bool Vecchi)
        {
            try
            {
                IsBusy = true;
                OrderDetails.Clear();
                var service = new UserOrderHistoryService();
                var details = await service.GetOrderDetailsAsync(Vecchi);
                foreach (var order in details)
                {
                    OrderDetails.Add(order);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
