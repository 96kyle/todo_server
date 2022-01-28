using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Databases
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Index { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public virtual Collection<Todo> TodoList { get; set; }
    }
}
