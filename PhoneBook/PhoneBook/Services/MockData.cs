using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.Model;

namespace PhoneBook.Services
{
    public class MockData : IDataStore<Contact>
    {
        private readonly List<Contact> _contacts;

        public MockData()
        {
            _contacts = new List<Contact>
            {
                new Contact(){Id = Guid.NewGuid().ToString(), FirstName = "Jan", LastName = "Kowalski",PhoneNumber = "32493432"},
                new Contact(){Id = Guid.NewGuid().ToString(), FirstName = "Alan", LastName = "Jankiewicz",PhoneNumber = "434"},
                new Contact(){Id = Guid.NewGuid().ToString(), FirstName = "Kacper", LastName = "Nowak",PhoneNumber = "4443"},
                new Contact(){Id = Guid.NewGuid().ToString(), FirstName = "Adam", LastName = "Doe",PhoneNumber = "423"},
                new Contact(){Id = Guid.NewGuid().ToString(), FirstName = "Piotr", LastName = "Majan",PhoneNumber = "544"},
            };
        }

        public Task<List<Contact>> LoadDataAsync()
        {
            List<Contact> orderedContacts = _contacts.OrderBy(x=>x.FirstName).ThenBy(x=>x.LastName).ToList();
            return Task.FromResult(orderedContacts);
        }

        public Task AddContactAsync(Contact contact)
        {
            contact.Id = Guid.NewGuid().ToString();
            _contacts.Add(contact);

            return Task.CompletedTask;
        }

        public Task EditContactAsync(Contact contact)
        {
            Contact modifiedContact = _contacts.First(x => x.Id == contact.Id);
            modifiedContact.FirstName = contact.FirstName;
            modifiedContact.LastName = contact.LastName;
            modifiedContact.PhoneNumber = contact.PhoneNumber;

            return Task.CompletedTask;
        }

        public Task<Contact> LoadDataAsync(string contactId)
        {
            return Task.FromResult(_contacts.First(x => x.Id == contactId));
        }

        public Task DeleteAsync(string id)
        {
            Contact contact = _contacts.First(x=>x.Id == id);
            _contacts.Remove(contact);
            return Task.CompletedTask;
        }
    }
}
