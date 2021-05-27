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
            MyMenu = GetMenus();
      //    this.BindingContext = this;
        }

        public List<Menu> MyMenu { get; set; }

        private List<Menu> GetMenus()
        {
            return new List<Menu>
            {
                new Menu{ Name = "Home", Icon = "home.png"},
                new Menu{ Name = "Profile", Icon = "user.png"},
                new Menu{ Name = "Notifications", Icon = "bell.png"},
                new Menu{ Name = "Messages", Icon = "envelope.png"},
                new Menu{ Name = "My Tasks", Icon = "tasks.png"},
            };
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
                await Application.Current.MainPage.Navigation.PushModalAsync(new CartView(),false);
        else if (l.Text=="Logout")
                await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView(), false);
        }

        //private void CloseRequested(object sender, EventArgs e)
        //{
        //    CloseAnimation();
        //}
    }

    public class Menu
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }


}
