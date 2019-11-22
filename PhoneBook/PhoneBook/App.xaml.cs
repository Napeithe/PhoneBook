using System;
using PhoneBook.Services;
using PhoneBook.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBook
{
    public partial class App : Application
    {
        private static ViewModelLocator _viewModelLocator;
        public static ViewModelLocator ViewModelLocator => _viewModelLocator ?? (_viewModelLocator = new ViewModelLocator());

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
