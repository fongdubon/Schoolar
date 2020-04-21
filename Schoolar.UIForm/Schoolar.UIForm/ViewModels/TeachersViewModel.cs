namespace Schoolar.UIForm.ViewModels
{
    using Schoolar.Common.Models;
    using Schoolar.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class TeachersViewModel: BaseViewModel
    {
        private ApiService apiService;

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        private ObservableCollection<Teacher> teachers;

        public ObservableCollection<Teacher> Teachers
        {
            get { return this.teachers; }
            set { this.SetValue(ref this.teachers, value); }
        }


        public TeachersViewModel()
        {
            this.apiService = new ApiService();
            this.LoadTeachers();
        }

        private async void LoadTeachers()
        {
            this.IsRefreshing = true;

            var response = await this.apiService.GetListAsync<Teacher>(
               "https://schoolarumad.azurewebsites.net",
               "/API",
               "/Teachers");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            var myProducts = (List<Teacher>)response.Result;
            this.Teachers = new ObservableCollection<Teacher>(myProducts);
        }
    }
}
