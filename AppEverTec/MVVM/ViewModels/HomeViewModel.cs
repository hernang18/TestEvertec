using AppEverTec.MVVM.Views;
using AppEverTec.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppEverTec.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class HomeViewModel
    {
        private readonly IChangeColor _changeColor; 
        public Color ColorText { get; set; }
        public string TextSwitch { get; set; }
        public Color ColorBack { get; set; }
        public string LastAdjustment { get; set; }
        public Color ColorRect { get; set; }
        public string ImageContact { get; set; }
        public string ImageMulti { get; set; }

        public bool IsToggledSw { get; set; }

        public HomeViewModel(IChangeColor changeColor, bool isToggledSw, IDataService dataService)
        {
            _changeColor = changeColor;
            IsToggledSw = isToggledSw;
            var homeViewModel = _changeColor.ChangeColor(IsToggledSw, this, dataService);
            ColorText = homeViewModel.ColorText;
            TextSwitch = homeViewModel.TextSwitch;
            ColorBack = homeViewModel.ColorBack;
            ColorRect = homeViewModel.ColorRect;
            LastAdjustment = homeViewModel.LastAdjustment;
            ImageContact = homeViewModel.ImageContact;
            ImageMulti = homeViewModel.ImageMulti;
        }


        public ICommand CommandGoto => new Command<string>((e) => Goto(e));
        private void Goto(string page)
        {
            switch (page)
            {
                case "Login":
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
                case "Contact":
                    Application.Current.MainPage.Navigation.PushAsync(new ContacsPage());
                    break;
                case "Multi":
                    Application.Current.MainPage.Navigation.PushAsync(new VisitorsPage());
                    break;
                case "Temp":
                    Application.Current.MainPage.Navigation.PushAsync(new ClimatePage());
                    break;
            }
           
        }
    }
}
