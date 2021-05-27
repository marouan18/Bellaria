using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersHistoryView : ContentPage
    {
        public OrdersHistoryView()
        {
            InitializeComponent();
            LabelName.Text = @"Order's History of " + Preferences.Get("Username", "Guest") + ",";

        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}