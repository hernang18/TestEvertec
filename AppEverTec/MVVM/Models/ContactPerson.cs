using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppEverTec.MVVM.Models
{
    public class ContactPerson
    {
        public DateTime createdAt { get; set; }
        public string name { get; set; }

        public string avatar { get; set; }

        public int id { get; set; }

        public bool IsEnabled { get; set; }

        [JsonIgnore]
        public string Title { get { return $"Bloqueado:{IsEnabled.ToString()} | Creado:{createdAt.ToString("yyyy-MM-dd HH:mm")}"; } }

        [JsonIgnore]
        public string TecText { get; set; }

        [JsonIgnore]
        public Color ColorCell { get; set; }

        [JsonIgnore]
        public Color ColorTitleText { get; set; }

        [JsonIgnore]
        public Color ColorText { get; set; }
    }
}
