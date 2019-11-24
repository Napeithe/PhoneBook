using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using PhoneBook.Model;

namespace PhoneBook.ViewModels
{
    public class AddNewItemViewModel : BaseViewModel
    {
        private string _id;
        private string _title;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private bool _firstNameShowError;
        private bool _lastNameShowError;
        private bool _phoneNumberShowError;

           

        public bool IsValid => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) &&
            !string.IsNullOrEmpty(PhoneNumber);

        public bool FirstNameShowError
        {
            get => _firstNameShowError;
            set
            {

                _firstNameShowError = value;
                RaisePropertyChanged(nameof(FirstNameShowError));
                RaisePropertyChanged(nameof(IsValid));
            }
        }
        public bool LastNameShowError
        {
            get => _lastNameShowError;
            set
            {

                _lastNameShowError = value;
                RaisePropertyChanged(nameof(LastNameShowError));
                RaisePropertyChanged(nameof(IsValid));
            }
        }

        public bool PhoneNumberShowError
        {
            get => _phoneNumberShowError;
            set
            {
                _phoneNumberShowError = value;
                RaisePropertyChanged(nameof(PhoneNumberShowError));
                RaisePropertyChanged(nameof(IsValid));
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                FirstNameShowError = string.IsNullOrEmpty(value);
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
                LastNameShowError = string.IsNullOrEmpty(value);
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
                PhoneNumberShowError = string.IsNullOrEmpty(value);
                if (value == _phoneNumber)
                {
                    return;
                }

                _phoneNumber = value;
                RaisePropertyChanged(nameof(PhoneNumber));
            }
        }

        public string Id
        {
            get => _id;
            set
            {
                if (value == _id)
                {
                    return;
                }

                _id = value;
                RaisePropertyChanged(nameof(Id));
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
            SetCommands(actionDelegate);
        }

        public void AsNew(IActionDelegate actionDelegate)
        {
            SetCommands(actionDelegate);
        }

        public void ClearError()
        {
            FirstNameShowError = false;
            LastNameShowError = false;
            PhoneNumberShowError = false;
        }

        private void SetCommands(IActionDelegate actionDelegate)
        {
            CancelCommand = new RelayCommand(execute: async () =>
            {
                    Clear();
                    await actionDelegate.CloseAsync();
            });
            RaisePropertyChanged(nameof(CancelCommand));
            SaveCommand = new RelayCommand(async () =>
            {
                if (IsValid)
                {
                    await CreateOrUpdate();
                    Clear();
                    await actionDelegate.CloseAsync();
                }

            });
            RaisePropertyChanged(nameof(SaveCommand));
        }

        private async Task SetContact(string contactId)
        {
            Contact loadDataAsync = await ContactsStore.LoadDataAsync(contactId);
            FirstName = loadDataAsync.FirstName;
            LastName = loadDataAsync.LastName;
            PhoneNumber = loadDataAsync.PhoneNumber;
            Id = contactId;
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
            await ContactsStore.CreateOrUpdateContactAsync(contact);

            MessengerInstance.Send<Contact>(contact);
        }

        private void Clear()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;

            ClearError();
        }
    }
}
