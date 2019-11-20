using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using PhoneBook.ViewModels;

namespace PhoneBook.Services
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainPageViewModel>();
        }

        public MainPageViewModel MainPageViewModel
        {
            get
            {
                if (!SimpleIoc.Default.IsRegistered<MainPageViewModel>())
                {
                    SimpleIoc.Default.GetInstance<MainPageViewModel>();
                }

                return SimpleIoc.Default.GetInstance<MainPageViewModel>();
            }
        }
    }
}
