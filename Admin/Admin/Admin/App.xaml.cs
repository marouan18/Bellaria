using System;
using Xamarin.Forms;
using Admin.Views;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Admin
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] {
                "AppTheme_Experimental",
                "MediaElement_Experimental"
                });
            InitializeComponent();

            //string uname = Preferences.Get("Username", String.Empty);
            //if (String.IsNullOrEmpty(uname))
            //    MainPage = new Login();
            //else
            //    MainPage = new HomeView();
            MainPage = new HomeView();
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
