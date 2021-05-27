using Admin.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Admin.Views
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {

            InitializeComponent();


        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
            // MainSwipeView.Close();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            MainSwipeView.Open(OpenSwipeItem.LeftItems);
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            if(l.Text=="Home")
                MainSwipeView.Close();
            else if(l.Text=="Nuovi Ordini")
                await Navigation.PushModalAsync(new OldOrdersView(),false);
           
        }
    }

}