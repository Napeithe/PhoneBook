using System;
using System.Collections.Generic;
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
            return Task.FromResult(_contacts);
        }
    }
}
