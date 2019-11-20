using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.ViewModels;
using Xamarin.Forms;

namespace PhoneBook
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = App.ViewModelLocator.MainPageViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!_viewModel.Contacts.Any())
            {
                _viewModel.LoadContactsCommand.Execute(null);
            }
        }

        private void AddItem_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
