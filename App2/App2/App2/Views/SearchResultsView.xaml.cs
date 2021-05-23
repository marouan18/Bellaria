using System;
using System.Collections.Generic;
using System.Linq;
using App2.Model;
using App2.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultsView : ContentPage
    {
         SearchResultsViewModel srvm;
        public SearchResultsView(string searchText)
        {
            InitializeComponent();
            srvm = new SearchResultsViewModel(searchText);
            this.BindingContext = srvm;
            LabelName.Text = $"Welcome {Preferences.Get("Username", "Guest")}";
             //Application.Current.MainPage.DisplayAlert("Success", "User Registered", "OK");
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = e.CurrentSelection.FirstOrDefault() as FoodItem;
            if (selectedProduct == null)
                return;
            await Navigation.PushModalAsync(new ProductDetailsView(selectedProduct));
            ((CollectionView)sender).SelectedItem = null;

        }
    }
}