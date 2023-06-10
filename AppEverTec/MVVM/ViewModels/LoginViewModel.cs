using AppEverTec.MVVM.Models;
using AppEverTec.MVVM.Views;
using AppEverTec.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppEverTec.MVVM.ViewModels
{
    public class LoginViewModel
    {
        private IApiService apiService;
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand => new Command(() => _Login());

        public ICommand AlertCommand => new Command<string>((e) => App.Current.MainPage.DisplayAlert("Alerta", e, "Ok"));
          
        public LoginViewModel()
        {
            apiService= new ApiService();
        }

       private async void _Login()
        {
            if (Email == null || Email == string.Empty)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email obligatorio", "Accept");
                return;
            }
            if (this.Password == null || this.Password == string.Empty)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Contraseña obligatorio", "Accept");
                return;
            }
            var login = new LoginViewModel
            {
                Email = Email,
                Password = Password
            };
            if (Email == "user.demo@mail.com" && Password == "Password2023*")
            {
                var response = await apiService.Post<Response<string>>("https://usersignin.free.beeceptor.com/GetUser",new Response<string>());
                if (!response.IsSucces)
                {
                    Application.Current.MainPage = new NavigationPage(new HomePage())
                    {
                        BarBackgroundColor = Colors.White
                    };
                }
            }        
            else
            {
                if(Email == "user.demo2@mail.com" && Password == "Password2023*")
                {
                    var response = await apiService.Post<Response<string>>("https://usersignin.free.beeceptor.com/InvalidUser", "");
                    await Application.Current.MainPage.DisplayAlert("Error", "Usuario y/o contraseña invalidos", "Accept");
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Usuario y/o contraseña invalidos", "Accept");
                    return;
                }

            }
        }
    }
}
