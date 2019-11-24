using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneBook.Model;

namespace PhoneBook.ViewModels
{
    public class AddNewItemViewModel : BaseViewModel
    {

        private string _title;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName)
                {
                    return;
                }

                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title)
                {
                    return;
                }

                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName)
                {
                    return;
                }

                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (value == _phoneNumber)
                {
                    return;
                }

                _phoneNumber = value;
                RaisePropertyChanged(nameof(PhoneNumber));
            }
        }


        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public interface IActionDelegate
        {
            Task CloseAsync();
        }

        public async Task AsEdit(string contactId, IActionDelegate actionDelegate)
        {
            Title = "Edycja";

            await SetContact(contactId);
        }

        private async Task SetContact(string contactId)
        {
            Contact loadDataAsync = await ContactsStore.LoadDataAsync(contactId);
            FirstName = loadDataAsync.FirstName;
            LastName = loadDataAsync.LastName;
            PhoneNumber = loadDataAsync.PhoneNumber;
            Id = contactId;
        }

        public string Id { get; set; }

        public void AsNew(IActionDelegate actionDelegate)
        {
            CancelCommand = new RelayCommand(execute: async () => await actionDelegate.CloseAsync());
            RaisePropertyChanged(nameof(CancelCommand));
            SaveCommand = new RelayCommand(async () =>
            {
                await CreateOrUpdate();
                await actionDelegate.CloseAsync(); ;
            });
            RaisePropertyChanged(nameof(SaveCommand));
        }

        private async Task CreateOrUpdate()
        {
            Contact contact = new Contact
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Id = Id
            };
            await ContactsStore.AddContactAsync(contact);

            MessengerInstance.Send<Contact>(contact);
        }
    }
}
