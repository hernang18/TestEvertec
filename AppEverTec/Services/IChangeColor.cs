using AppEverTec.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEverTec.Services
{
    public interface IChangeColor
    {
        HomeViewModel ChangeColor(bool dark,HomeViewModel homeViewModel, IDataService dataService);
    }
}
