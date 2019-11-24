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
         
            BindingContext = _viewModel;
        }

        public void AsNew()
        {
            _viewModel.AsNew(this);
            _viewModel.ClearError();
        }

        public async Task AsEdit(string contactId)
        {
            await _viewModel.AsEdit(contactId, this);
            _viewModel.ClearError();

        }

        public async Task CloseAsync()
        {
            await Navigation.PopAsync(true);
        }
    }
}