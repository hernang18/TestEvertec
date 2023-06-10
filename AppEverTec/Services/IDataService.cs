using AppEverTec.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEverTec.Services
{
    public interface IDataService
    {
        Task<Visitor> GetVisitor(int id);
        Task<List<Visitor>> GetVisitor();    

        Task Update<T>(T model) where T : class;    

        Task Insert<T>(T model) where T : class;

        Task Delete<T>(T model) where T : class;
    }
}
