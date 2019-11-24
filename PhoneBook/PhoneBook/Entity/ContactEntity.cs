using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using PhoneBook.Model;
using SQLite;

namespace PhoneBook.Entity
{
    public class ContactEntity
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public void Update(Contact contact)
        {
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            PhoneNumber = contact.PhoneNumber;
        }
    }
}
