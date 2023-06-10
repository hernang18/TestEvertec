using AppEverTec.MVVM.Models;
using AppEverTec.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEverTec.Services
{
    public class ChangeColor : IChangeColor
    {
        HomeViewModel IChangeColor.ChangeColor(bool dark, HomeViewModel homeViewModel, IDataService dataService)
        {
            if(dark)
            {
                homeViewModel.ColorText = Color.FromArgb("#FFFFFF");
                homeViewModel.TextSwitch = "Oscuro";
                homeViewModel.ColorBack = Color.FromArgb("#FF485056");
                homeViewModel.ColorRect = Color.FromArgb("#FF353E43");
                homeViewModel.ImageContact = "contactwhite.png";
                homeViewModel.ImageMulti = "multiwhite.png";

            }
            else
            {
                homeViewModel.ColorText = Color.FromArgb("#FF485056");
                homeViewModel.TextSwitch = "Claro";
                homeViewModel.ColorBack = Color.FromArgb("#FFFFFF");
                homeViewModel.ColorRect = Color.FromArgb("#FFF2F2F2");
                homeViewModel.ImageContact = "contact.png" ;
                homeViewModel.ImageMulti ="multi.png" ;
            }           
            var registerChange = new RegisterChange
            {
                Id = 1,
                DateRegister = DateTime.Now.ToString("dd MMMM,yyyy")
                //DateRegister=DateTime.Now.ToString()
            };
            dataService.Update<RegisterChange>(registerChange);
            homeViewModel.LastAdjustment = registerChange.DateRegister;
            return homeViewModel;
        }
    }
}
