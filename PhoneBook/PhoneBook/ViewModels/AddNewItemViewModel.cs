using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneBook.Model;

namespace PhoneBook.ViewModels
{
    public class AddNewItemViewModel : BaseViewModel
    {

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

        public void SetupAction(IActionDelegate actionDelegate)
        {
            CancelCommand = new RelayCommand(execute: async () => await actionDelegate.CloseAsync());
            RaisePropertyChanged(nameof(CancelCommand));
            SaveCommand = new RelayCommand(async () =>
            {
                await AddNewItemAsync();
                await actionDelegate.CloseAsync();;
            });
            RaisePropertyChanged(nameof(SaveCommand));
        }

        private async Task AddNewItemAsync()
        {
            Contact contact = new Contact
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber
            };
            await ContactsStore.AddContactAsync(contact);

            MessengerInstance.Send<Contact>(contact);
        }
    }
}
