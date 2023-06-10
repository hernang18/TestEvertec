using AppEverTec.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppEverTec.MVVM.ViewModels.ItemViewModel
{
    public class ContactPersonItemViewModel:ContactPerson
    {
        public ICommand SelectedItem => new Command(() => Show());

        private async void Show()
        {
            await Application.Current.MainPage.DisplayAlert(this.name, this.TecText, "Aceptar");
        }
    }
}
