using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.ViewModels;
using Xamarin.Forms;

namespace PhoneBook.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, MainPageViewModel.IMainPageNavigator
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = App.ViewModelLocator.MainPageViewModel;
            BindingContext = _viewModel;

            _viewModel.SetupNavigator(this);
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

        public async Task AddNewContactNavAsync()
        {
            await Navigation.PushAsync(new AddContactPage());
        }

        public async Task EditContactNavAsync(int contactId)
        {
            throw new NotImplementedException();
        }
    }
}
