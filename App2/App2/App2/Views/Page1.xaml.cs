
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
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            MyMenu = GetMenus();
            this.BindingContext = this;
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
            //MainSwipeView.Open(OpenSwipeItem.LeftItems);

        }

        private void CloseSwipe(object sender, EventArgs e)
        {
       //     MainSwipeView.Close();

        }

      

        //private void CloseRequested(object sender, EventArgs e)
        //{
        //    CloseAnimation();
        //}
    }

    /*public class Menu
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }*/
}