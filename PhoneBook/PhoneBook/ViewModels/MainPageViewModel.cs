using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using PhoneBook.Extensions;
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
        public RelayCommand<string> EditContactCommand { get; set; }

        public MainPageViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            LoadContactsCommand = new RelayCommand(async () => await ExecuteLoadContactsCommand());
            MessengerInstance.Register<Contact>(this, UpdateContactList);
        }

        public void SetupNavigator(IMainPageNavigator navigator)
        {
            AddNewContactCommand = new RelayCommand(async () => await navigator.AddNewContactNavAsync());
            RaisePropertyChanged(nameof(AddNewContactCommand));
            EditContactCommand = new RelayCommand<string>(async (contactId) => await navigator.EditContactNavAsync(contactId));
            RaisePropertyChanged(nameof(EditContactCommand));
        }

        public async Task DeleteAsync(string id)
        {
            await ContactsStore.DeleteAsync(id);
            Contact contact = Contacts.First(x=>x.Id == id);
            Contacts.Remove(contact);
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


        private void UpdateContactList(Contact contact)
        {
            Contact oldContact = Contacts.FirstOrDefault(x => x.Id == contact.Id);
            if (oldContact is null)
            {
                Contacts.Add(contact);
            }
            else
            {
                Contacts.Remove(oldContact);
                Contacts.Add(contact);
            }

            Contacts = _contacts.OrderByName();
        }

        public interface IMainPageNavigator
        {
            Task AddNewContactNavAsync();
            Task EditContactNavAsync(string contactId);
        }
    }
}
