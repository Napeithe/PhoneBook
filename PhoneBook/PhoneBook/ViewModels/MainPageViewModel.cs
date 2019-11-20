using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PhoneBook.Model;

namespace PhoneBook.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Contact> _contacts;

        public RelayCommand LoadContactsCommand { get; set; }

        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                if (_contacts == value) return;
                _contacts = value;
                RaisePropertyChanged(nameof(Contacts));

            }
        }

        public MainPageViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            LoadContactsCommand = new RelayCommand(async () => await ExecuteLoadContactsCommand());
        }

        private async Task ExecuteLoadContactsCommand()
        {
            _contacts.Clear();
            List<Contact> contacts = await ContactsStore.LoadDataAsync();
            contacts.ForEach(x =>
            {
                _contacts.Add(x);
            });
        }

    }
}
