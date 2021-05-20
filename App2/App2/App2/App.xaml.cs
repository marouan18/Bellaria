using System;
using Xamarin.Forms;
using App2.Views;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace App2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // MainPage = new MainPage();
            // MainPage = new Login();
              MainPage = new NavigationPage(new SettingsPage());
            /*  string uname = Preferences.Get("Username", String.Empty);
              if (String.IsNullOrEmpty(uname))
                  MainPage = new LogoutView();
              else
                  MainPage = new ProductsView();
          */
        
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
