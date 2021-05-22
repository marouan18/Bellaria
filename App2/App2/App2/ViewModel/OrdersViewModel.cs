using System;
using System.Collections.Generic;
using System.Text;

namespace App2.ViewModel
{
    class OrdersViewModel : BaseViewModel
    {
        private string _OrderID;
        public string OrderID
        {
            set
            {
                _OrderID = value;
                OnPropertyChanged();
            }

            get
            {
                return _OrderID;
            }
        }

        public OrdersViewModel(string id)
        {
            OrderID = id;
        }
    }
}
