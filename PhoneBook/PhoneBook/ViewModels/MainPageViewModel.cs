using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public RelayCommand AddNewContactCommand { get; set; }
        public RelayCommand<int> EditContactCommand { get; set; }

        public MainPageViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            LoadContactsCommand = new RelayCommand(async () => await ExecuteLoadContactsCommand());
            MessengerInstance.Register<Contact>(this, AddNewContactToList);
        }

        public void SetupNavigator(IMainPageNavigator navigator)
        {
            AddNewContactCommand = new RelayCommand(async () => await navigator.AddNewContactNavAsync());
            RaisePropertyChanged(nameof(AddNewContactCommand));
            EditContactCommand = new RelayCommand<int>(async (contactId) => await navigator.EditContactNavAsync(contactId));
            RaisePropertyChanged(nameof(EditContactCommand));
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

        private  void AddNewContactToList(Contact contact)
        {
            _contacts.Add(contact);
        }

        public interface IMainPageNavigator
        {
            Task AddNewContactNavAsync();
            Task EditContactNavAsync(int contactId);
        }
    }
}
