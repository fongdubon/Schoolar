namespace Schoolar.UIForm.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Schoolar.UIForm.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel
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
            MainViewModel.GetInstance().Teachers = new TeachersViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new TeachersPage());
            return;
        }
        public LoginViewModel()
        {
            this.Email = "eduardofongdubon@hotmail.com";
            this.Password = "123456";
        }
    }
}
