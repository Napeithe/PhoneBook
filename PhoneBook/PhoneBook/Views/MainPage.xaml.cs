using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        public async Task AddNewContactNavAsync()
        {
            AddContactPage addContactPage = new AddContactPage();
            addContactPage.AsNew();
            await Navigation.PushAsync(addContactPage);
        }

        public async Task EditContactNavAsync(string contactId)
        {
            AddContactPage addContactPage = new AddContactPage();
            await addContactPage.AsEdit(contactId);
            await Navigation.PushAsync(addContactPage);
        }

        private async void Edit_OnClicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            await EditContactNavAsync(menuItem.CommandParameter.ToString());
        }

        private async void Delete_OnClicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem) sender;
            await _viewModel.DeleteAsync(menuItem.CommandParameter.ToString());
        }
    }
}
