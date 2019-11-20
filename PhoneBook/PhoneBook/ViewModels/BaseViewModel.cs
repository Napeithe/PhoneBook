using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using PhoneBook.Model;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public IDataStore<Contact> ContactsStore => SimpleIoc.Default.GetInstance<IDataStore<Contact>>();

    }
}
