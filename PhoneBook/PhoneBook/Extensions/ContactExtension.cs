using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PhoneBook.Model;

namespace PhoneBook.Extensions
{
    public static class ContactExtension
    {
        public static List<Contact> OrderByName(this List<Contact> contacts)
        {
            return contacts.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
        }

        public static ObservableCollection<Contact> OrderByName(this ObservableCollection<Contact> contacts)
        {
            List<Contact> orderedList = contacts.ToList().OrderByName();
            return new ObservableCollection<Contact>(orderedList);
        }
    }
}
