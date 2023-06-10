using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEverTec.MVVM.Models
{
    [SQLite.Table("Visitor")]
    public class Visitor
    {
        [Key]
        [SQLite.PrimaryKey,AutoIncrement]
        public int id { get; set; }

        public string Name { get; set; }

        public string LocalPath { get; set; }

        public DateTime DateTimeVist { get; set; }

    }
}
