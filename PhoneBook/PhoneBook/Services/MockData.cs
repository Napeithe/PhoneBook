﻿using System;
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

        public Task CreateOrUpdateContactAsync(Contact contact)
        {
            if (string.IsNullOrEmpty(contact.Id))
            {
                contact.Id = Guid.NewGuid().ToString();
            }

            Contact oldContact = _contacts.FirstOrDefault(x=>x.Id == contact.Id);
            if (oldContact is null)
            {
                _contacts.Add(contact);
            }
            else
            {
                EditContactAsync(contact, oldContact);
            }

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

        private Task EditContactAsync(Contact contact, Contact oldContact)
        {
            oldContact.FirstName = contact.FirstName;
            oldContact.LastName = contact.LastName;
            oldContact.PhoneNumber = contact.PhoneNumber;

            return Task.CompletedTask;
        }
    }
}
