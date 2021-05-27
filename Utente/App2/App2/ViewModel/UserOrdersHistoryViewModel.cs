using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using App2.Model;
using App2.Services;
using Xamarin.Forms;
namespace App2.ViewModel
{
    class UserOrdersHistoryViewModel : BaseViewModel
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

        public UserOrdersHistoryViewModel()
        {
            OrderDetails = new ObservableCollection<UserOrdersHistory>();
            LoadUserOrders();
        }

        private async void LoadUserOrders()
        {
            try
            {
                IsBusy = true;
                OrderDetails.Clear();
                var service = new UserOrderHistoryService();
                var details = await service.GetOrderDetailsAsync();
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
