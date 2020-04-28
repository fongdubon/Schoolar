namespace Schoolar.UIForm.ViewModels
{
    using Schoolar.Common.Models;
    using Schoolar.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class CoordinatorViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Coordinator> coordinators;

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ObservableCollection<Coordinator> Coordinators
        {
            get { return this.coordinators; }
            set { this.SetValue(ref this.coordinators, value); }
        }

        public CoordinatorViewModel()
        {
            this.apiService = new ApiService();
            this.LoadCoordinators();
        }

        private async void LoadCoordinators()
        {
            this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Coordinator>(
               url,
               "/API",
               "/Coordinators");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myCoordinators = (List<Coordinator>)response.Result;
            this.Coordinators = new ObservableCollection<Coordinator>(myCoordinators);
        }
    }
}
