using GalaSoft.MvvmLight.Ioc;
using PhoneBook.Entity;
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
            SimpleIoc.Default.Register<IDataStore<Contact>>(() => DatabaseInstance.Database);
        }

        public MainPageViewModel MainPageViewModel => SimpleIoc.Default.GetInstance<MainPageViewModel>();
        public AddNewItemViewModel AddNewItemViewModel => SimpleIoc.Default.GetInstance<AddNewItemViewModel>();

    }
}
