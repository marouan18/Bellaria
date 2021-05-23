using System;
using Xamarin.Forms;
using App2.Views;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using App2.Model;

namespace App2
{
    public partial class App : Application
    {
        public static bool IsCartTableCreated = Preferences.Get("IsCartTableCreated", false);
        public App()
        {
            Device.SetFlags(new string[] {
                "AppTheme_Experimental",
                "MediaElement_Experimental"
                });
            InitializeComponent();

            // MainPage = new MainPage();
            // MainPage = new Login();
            //MainPage = new NavigationPage(new SettingsPage());
            string uname = Preferences.Get("Username", String.Empty);
            if (String.IsNullOrEmpty(uname))
                MainPage = new Login();
            else
                MainPage = new ProductsView();


        }

        protected override void OnStart()
        {
            if (IsCartTableCreated == false)
            {
                var cn = DependencyService.Get<ISQLite>().GetConnection();
                cn.CreateTable<CartItem>();
                cn.Close();
                Preferences.Set("IsCartTableCreated", true);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
