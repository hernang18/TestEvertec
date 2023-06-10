using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEverTec.MVVM.Models
{
    public class Response<T>
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }

        public object Result { get; set; }
    }
}
