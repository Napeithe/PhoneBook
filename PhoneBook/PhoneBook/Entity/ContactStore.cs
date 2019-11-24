using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PhoneBook.Model;
using PhoneBook.Services;
using SQLite;

namespace PhoneBook.Entity
{
    public class ContactStore : IDataStore<Contact>
    {
        private SQLiteAsyncConnection _database;
        private AsyncTableQuery<ContactEntity> _contactEntityTable;


        public ContactStore(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ContactEntity>().Wait();
            _contactEntityTable = _database.Table<ContactEntity>();
        }


        public async Task<List<Contact>> LoadDataAsync()
        {
            return (await _contactEntityTable.ToListAsync())
                .Select(Contact.From).ToList();
        }

        public async Task CreateOrUpdateContactAsync(Contact contact)
        {
            if (string.IsNullOrEmpty(contact.Id))
            {
                contact.Id = Guid.NewGuid().ToString();
            }

            ContactEntity contactEntity = await _contactEntityTable.FirstOrDefaultAsync(x => x.Id == contact.Id);
            if (contactEntity is null)
            {
                ContactEntity entity = contact.CreateEntity();
                await _database.InsertAsync(entity);
            }
            else
            {
                contactEntity.Update(contact);
                await _database.UpdateAsync(contactEntity);
            }
        }

        public async Task<Contact> LoadDataAsync(string contactId)
        {
            var contact = await GetContactById(contactId);
            if (contact is null)
            {
                throw new ContactNotFoundException("Contact was not found");
            }

            return Contact.From(contact);
        }

        public async Task DeleteAsync(string id)
        {
            ContactEntity contact = await GetContactById(id);
            if (contact is null)
            {
                throw new ContactNotFoundException("Contact was not found");
            }
            await _database.DeleteAsync(contact);
        }

        private async Task<ContactEntity> GetContactById(string contactId)
        {
            ContactEntity contact = await _contactEntityTable.FirstOrDefaultAsync(x => x.Id == contactId);
            return contact;
        }
    }

    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException(string message):base(message)
        {
            
        }
    }
}
