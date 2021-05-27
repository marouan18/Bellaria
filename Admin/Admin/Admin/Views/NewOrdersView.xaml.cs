using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewOrdersView : ContentPage
    {
        public NewOrdersView()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Grid g = (Grid)sender;
            Label l = (Label)g.Children[1];
            await Navigation.PushModalAsync(new OrderDetailsView(l.Text), false);


        }
    }
}