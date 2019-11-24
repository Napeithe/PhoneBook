using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Model;

namespace PhoneBook.Services
{
    public interface IDataStore<T>
    {
        Task<List<T>> LoadDataAsync();
        Task CreateOrUpdateContactAsync(Contact contact);
        Task EditContactAsync(Contact contact);
        Task<Contact> LoadDataAsync(string contactId);
        Task DeleteAsync(string id);
    }
}
