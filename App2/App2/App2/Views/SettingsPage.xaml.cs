using App2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async  void  ButtonProducts_Clicked(object sender, EventArgs e)
        {
            var acd = new AddCategoryData();
            await acd.AddCategoriesAsync();
        }

        private async void ButtonCategories_Clicked(object sender, EventArgs e)
        {
            var afd = new AddFoodItemsData();
            await afd.AddFoodItemAsync();
        }
    }
}