using AppEverTec.MVVM.Models;
using AppEverTec.MVVM.ViewModels.ItemViewModel;
using AppEverTec.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppEverTec.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AddVisitorViewModel
    {
        private readonly IDataService _dataService;
        public ImageSource imageSource { get; set; }

        public int id { get; set; }

        public string Name { get; set; }

        public string LocalFilePath { get; set; }

        FileResult photo { get; set; }
        public AddVisitorViewModel(IDataService dataService,int id)
        {
            _dataService = dataService;
            Load(id);
        }

        public ICommand CommandChangePhoto=> new Command(() => ChangePhoto());

        public ICommand SaveCommand => new Command(() => Save());

        private async void Load(int id)
        {
            imageSource = "avatar3.png";
            LocalFilePath = string.Empty;
            if (id!= 0)
            {
                Visitor visitor = await _dataService.GetVisitor(id).ConfigureAwait(false);
                if (visitor != null)
                {
                    imageSource= ImageSource.FromFile(visitor.LocalPath);
                    LocalFilePath = visitor.LocalPath;
                    id = visitor.id;
                    Name = visitor.Name;
                }
            }
            return;
        }
        private async void ChangePhoto()
        {
            photo = null;
            var source = await Application.Current.MainPage.DisplayActionSheet("Origen de la imagen", "Cancelar", null, "Camara", "Galeria");
            switch (source)
            {
                case "Cancelar":                  
                    return;
                case "Camara":
                    if (MediaPicker.Default.IsCaptureSupported)
                    {
                        photo = await MediaPicker.Default.CapturePhotoAsync();
                    }
                    break;
                case "Galeria":
                        photo = await MediaPicker.Default.PickPhotoAsync();
                    break;
            }
            if (photo != null)
            {
               
                LocalFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(LocalFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                imageSource=ImageSource.FromFile(LocalFilePath);

               
            }
        }

        private async void Save()
        {
            if (LocalFilePath == string.Empty)
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Advertencia", "Está seguro que desea guardar Sin foto", "Aceptar", "Cancelar");
                if (!answer)
                    return;

            }
            var visitor = new Visitor
            {
                DateTimeVist = DateTime.Now,
                LocalPath = LocalFilePath,
                Name = Name,
                id=id,
            };
            if (id==0)
            {
                await _dataService.Insert<Visitor>(visitor);
            }
            else
            {
                await _dataService.Update<Visitor>(visitor);
            }
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
            var visitorViewModel = VisitorViewModel.GetInstance();
            visitorViewModel.Load();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
