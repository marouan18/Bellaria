using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OldOrdersView : ContentPage
    {
        public OldOrdersView()
        {
            InitializeComponent();
            LabelName.Text = "Cronologia ordini";

        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}