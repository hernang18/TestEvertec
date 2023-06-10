using AppEverTec.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEverTec.Services
{
    public interface IApiService
    {
        Task<Response<T>> GetList<T>(string url);
        Task<Response<T>> Get<T>(string url);
        Task<Response<T>> Post<T>(string url, object model);
        Task<Response<T>> Put<T>(string url, object model);
        Task<Response<T>> Delete<T>(string url);
    }
}
