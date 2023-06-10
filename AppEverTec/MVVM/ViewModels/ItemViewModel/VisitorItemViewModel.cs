using AppEverTec.MVVM.Models;
using AppEverTec.MVVM.Views;
using AppEverTec.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppEverTec.MVVM.ViewModels.ItemViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class VisitorItemViewModel : Visitor
    {

        public string DateVisit { get; set; }

        public string HourVisit { get; set; }

        public ImageSource imageSource { get; set; }

        public ICommand EditCommand => new Command(() => Edit());

        public ICommand DeleteCommand => new Command(() => Delete());

        private IDataService dataService;

        private async void Edit()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddVisitorPage(new DataService(), this.id));

        }
        private async void Delete()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Advertencia", "Está seguro que desea eliminar este registro", "Aceptar", "Cancelar");
            if (!answer)
                return;
            dataService= new DataService();

            var visitor = new Visitor
            {
                id = this.id,
                DateTimeVist=this.DateTimeVist,
                LocalPath = this.LocalPath,
                Name = this.Name
            };
            var visitorItemViewModel = new VisitorItemViewModel
            {
                id = visitor.id,
                DateTimeVist = visitor.DateTimeVist,
                DateVisit = visitor.DateTimeVist.ToString("dd MMMM de yyyy"),
                HourVisit = visitor.DateTimeVist.ToString("HH:mm"),
                imageSource = ImageSource.FromFile(visitor.LocalPath),
                LocalPath = visitor.LocalPath,
                Name = visitor.Name,
            };
            await dataService.Delete<Visitor>(visitor);
            File.Delete(visitorItemViewModel.LocalPath);
            var visitorViewModel =VisitorViewModel.GetInstance();
            visitorViewModel.Load();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
