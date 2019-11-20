using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using PhoneBook.Model;

namespace PhoneBook.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private List<Contact> _list;

        public List<Contact> Test
        {
            get => _test;
            set
            {
                if(_test == value) return;
                _test = value;
                RaisePropertyChanged(nameof(Test));

            }
        }
    }
}
