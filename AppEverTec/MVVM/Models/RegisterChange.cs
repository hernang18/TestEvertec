using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEverTec.MVVM.Models
{
    [SQLite.Table("RegisterChange")]
    public class RegisterChange
    {
        [Key]
        [SQLite.PrimaryKey]
        public int Id { get; set; }
        public string DateRegister { get; set; }
    }
}
