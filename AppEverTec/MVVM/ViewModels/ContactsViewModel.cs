using AppEverTec.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppEverTec.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppEverTec.MVVM.ViewModels.ItemViewModel;
using System.Security.Principal;

namespace AppEverTec.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ContactsViewModel
    {
        private readonly IApiService _apiService;
        List<string> lsTecno=new List<string>();
        public bool IsRefreshing { get; set; }

        public int ContacCount { get; set; }

        public ObservableCollection<ContactPersonItemViewModel> OContacts { get; private set; }

        public ContactsViewModel(IApiService apiService)
        {
            IsRefreshing = false;
            _apiService = apiService;
            lsTecno.Add("Contacto de España, es profesor Con mas de 5 años de experiencia");
            lsTecno.Add("Contacto de Colombia, es programador senior 1 decada de experiencia");
            lsTecno.Add("Contacto de Colombia,arquitecto de software principiante");
            lsTecno.Add("Contacto de Colombia,adminstrador Base de datos");
            lsTecno.Add("Contacto de Colombia,arquitecto de infraestructura senior");
            Load();
        }
        public ICommand RefreshCommand => new Command(() => Refresh());
        private void Refresh()
        {
            IsRefreshing = true;
            List<ContactPersonItemViewModel> contactperson = new List<ContactPersonItemViewModel>();
            foreach (var item in OContacts)
            {
                var random = new Random();
                int r = random.Next(lsTecno.Count);
                item.TecText = lsTecno[r];
                contactperson.Add(item);
            }
            OContacts = new ObservableCollection<ContactPersonItemViewModel>(contactperson);
            ContacCount = OContacts.Count;
            IsRefreshing = false;
        }
        private async void Load()
        {
            var response = await _apiService.GetList<ContactPerson>("https://usersignin.free.beeceptor.com/GetUser");
            if (!response.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            var Contacts = (List<ContactPerson>)response.Result;
            foreach (var item in Contacts)
            {
                if (item.id % 2 == 0)
                    item.IsEnabled = true;
                else
                    item.IsEnabled = false;
                if(item.IsEnabled)
                {
                    item.ColorCell = Colors.DarkOrange;
                    item.ColorTitleText = Colors.White;
                    item.ColorText = Colors.White;
                }
                else
                {
                    item.ColorCell = Colors.WhiteSmoke;
                    item.ColorTitleText = Colors.Black;
                    item.ColorText = Colors.LightGray;
                }
                var random= new Random();
                int r = random.Next(lsTecno.Count);
                item.TecText = lsTecno[r];
            }
            var contactPersonItemViewModels = (from item in Contacts
                         select new ContactPersonItemViewModel
                         {
                             avatar = item.avatar,
                             ColorCell=item.ColorCell,
                             ColorTitleText=item.ColorTitleText,
                             ColorText=item.ColorText,
                             createdAt = item.createdAt,
                             IsEnabled=item.IsEnabled,
                             id=item.id,
                             name=item.name,
                             TecText=item.TecText,
                         }).ToList();
            OContacts = new ObservableCollection<ContactPersonItemViewModel>(contactPersonItemViewModels);
            ContacCount = OContacts.Count;
            return;
        }
    }

    
}
