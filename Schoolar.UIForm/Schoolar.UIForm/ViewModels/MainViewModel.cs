using GalaSoft.MvvmLight.Command;
using Schoolar.Common.Models;
using Schoolar.UIForm.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Schoolar.UIForm.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private static MainViewModel instance;

        public LoginViewModel Login { get; set; }

        public TeachersViewModel Teachers { get; set; }

        public AddTeacherViewModel AddTeacher { get; set; }

        public CoordinatorViewModel Coordinators { get; set; }

        public ICommand AddTeacherCommand { get { return new RelayCommand(this.GoAddTeacher); } }

        public MainViewModel()
        {
            instance = this; 
            this.LoadMenu();
        }

        private async void GoAddTeacher()
        {
            this.AddTeacher = new AddTeacherViewModel();
            await App.Navigator.PushAsync(new AddTeacherPage());
        }

        private void LoadMenu()
        {
            var menus = new List<Menu>()
            {
                new Menu
                {
                    Icon = "ic_info_outline",
                    PageName = "AboutPage",
                    Title = "About"
                },
                new Menu
                {
                    Icon = "ic_phonelink_setup",
                    PageName = "SetupPage",
                    Title = "Setup"
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Close session"
                }
            };
            this.Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }        
        
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
