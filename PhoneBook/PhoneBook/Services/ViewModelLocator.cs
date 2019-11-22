using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using PhoneBook.Model;
using PhoneBook.ViewModels;

namespace PhoneBook.Services
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<AddNewItemViewModel>();
            SimpleIoc.Default.Register<IDataStore<Contact>>(() => new MockData());
        }

        public MainPageViewModel MainPageViewModel => SimpleIoc.Default.GetInstance<MainPageViewModel>();
        public AddNewItemViewModel AddNewItemViewModel => SimpleIoc.Default.GetInstance<AddNewItemViewModel>();

    }
}
