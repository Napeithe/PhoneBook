﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhoneBook.Entity
{
    public class DatabaseInstance
    {
        private static ContactStore _database;
        public static ContactStore Database =>
            _database ?? (_database = new ContactStore(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ContactStore.db3")));
    }
}