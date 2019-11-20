using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services
{
    public interface IDataStore<T>
    {
        Task<List<T>> LoadDataAsync();
    }
}
