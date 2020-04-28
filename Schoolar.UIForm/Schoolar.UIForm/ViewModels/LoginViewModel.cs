namespace Schoolar.UIForm.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Schoolar.UIForm.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel:BaseViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tou must entar an email",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tou must entar a Password",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tou must entar a Password",
                    "Accept");
                return;
            }
            if (!this.Email.Equals("eduardofongdubon@hotmail.com") || !this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "user or Password incorrect",
                    "Accept");
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Coordinator = new CoordinatorViewModel();
            Application.Current.MainPage = new MasterPage(); 
        }
        public LoginViewModel()
        {
            this.Email = "eduardofongdubon@hotmail.com";
            this.Password = "123456";
        }
    }
}
