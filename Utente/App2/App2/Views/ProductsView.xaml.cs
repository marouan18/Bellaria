using App2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsView : ContentPage
    {
        public ProductsView()
        {
            InitializeComponent();

        }

        private void OpenSwipe(object sender, EventArgs e)
        {
            MainSwipeView.Open(OpenSwipeItem.LeftItems);

        }

        private void CloseSwipe(object sender, EventArgs e)
        {
            MainSwipeView.Close();
        }

        async void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var category = e.CurrentSelection.FirstOrDefault() as Category;
            if (category == null)
                return;

            await Navigation.PushModalAsync(new CategoryView(category));

            ((CollectionView)sender).SelectedItem = null;

        }


        async void CV_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var selectedProduct = e.CurrentSelection.FirstOrDefault() as FoodItem;
            if (selectedProduct == null)
                return;
            await Navigation.PushModalAsync(new ProductDetailsView(selectedProduct));
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            if(l.Text=="Cronologia Ordini")
                await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersHistoryView(),false);
        else if (l.Text=="Logout")
                await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView(), false);
        }
    }
}
