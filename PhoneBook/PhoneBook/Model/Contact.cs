using PhoneBook.Entity;

namespace PhoneBook.Model
{
    public class Contact
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }


        public static Contact From(ContactEntity contactEntity)
        {
            return new Contact()
            {
                Id = contactEntity.Id,
                FirstName = contactEntity.FirstName,
                LastName = contactEntity.LastName,
                PhoneNumber = contactEntity.PhoneNumber
            };
        }

        public ContactEntity CreateEntity()
        {
            return new ContactEntity
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber
            };
        }
    }
}
