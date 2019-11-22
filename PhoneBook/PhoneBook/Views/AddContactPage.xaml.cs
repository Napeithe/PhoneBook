using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddContactPage : ContentPage, AddNewItemViewModel.IActionDelegate
    {
        private AddNewItemViewModel _viewModel;

        public AddContactPage()
        {
            InitializeComponent();
            _viewModel = App.ViewModelLocator.AddNewItemViewModel;
            _viewModel.SetupAction(this);
            BindingContext = _viewModel;
        }


        async void Save_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public Task CloseAsync()
        {
            throw new NotImplementedException();
        }
    }
}