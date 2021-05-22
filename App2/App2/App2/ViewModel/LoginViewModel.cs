using System;
using System.Threading.Tasks;
using App2.Services;
using App2.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App2.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;
        public string Username
        {
            set
            {
                _Username = value;
                OnPropertyChanged();
            }
            get
            {
                return _Username;
            }
        }

        private string _Password;
        public string Password
        {
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
            get
            {
                return _Password;
            }
        }


        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return _IsBusy;
            }
        }

        private bool _Result;
        public bool Result
        {
            set
            {
                _Result = value;
                OnPropertyChanged();
            }
            get
            {
                return _Result;
            }
        }

        private bool _Disable;
        public bool Disable
        {
            set
            {
                _Disable = value;
                OnPropertyChanged();
            }

            get
            {
                return _Disable;
            }
        }

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public LoginViewModel()
        {
            Disable = false;
            
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new Userservice();
                //var userService = DependencyService.Get<IUserService>();
                Result = await userService.RegisterUser(Username, Password);
                if (Result)
                    await Application.Current.MainPage.DisplayAlert("Success", "User Registered", "OK");
                else
                    await Application.Current.MainPage.DisplayAlert("Error",
                        "User already exists with this credentials", "OK");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new Userservice();
               // var userService = DependencyService.Get<IUserService>();
                Result = await userService.LoginUser(Username, Password);
                if( Result)
                {
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid Username or Password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
