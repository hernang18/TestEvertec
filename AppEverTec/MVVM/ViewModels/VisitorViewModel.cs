using AppEverTec.MVVM.Models;
using AppEverTec.MVVM.ViewModels.ItemViewModel;
using AppEverTec.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppEverTec.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VisitorViewModel
    {
        private readonly IDataService _dataService;
        public bool IsRefreshing { get; set; }

        public ObservableCollection<VisitorItemViewModel> OVisitors { get; set; }

        public VisitorViewModel(IDataService dataService)
        {
            IsRefreshing = false;
            _dataService = dataService;
            Load();
            instance = this;
        }
        #region Singleton
        private static VisitorViewModel instance { get; set; }

        public static VisitorViewModel GetInstance()
        {
            if (instance == null)
            {
                return new VisitorViewModel(new DataService());
            }
            return instance;
        }

        #endregion

        public async void Load()
        {
            var visitors = await _dataService.GetVisitor();
            var visitorItemViewModels = (from item in visitors
                        select new VisitorItemViewModel
                        {
                            id =item.id,
                            DateTimeVist = item.DateTimeVist,
                            DateVisit = item.DateTimeVist.ToString("dd MMMM de yyyy"),
                            HourVisit = item.DateTimeVist.ToString("HH:mm"),
                            imageSource = ImageSource.FromFile(item.LocalPath),
                            LocalPath = item.LocalPath,
                            Name = item.Name,
                        }).ToList();
            OVisitors = new ObservableCollection<VisitorItemViewModel>(visitorItemViewModels);
            return;
        }

    
    }
}
