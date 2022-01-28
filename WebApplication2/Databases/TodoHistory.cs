using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Databases
{
    [Table("todo_history")]
    public class TodoHistory
    {
        [Key]
        public int Id { get; set; }

        public int TodoId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedTime { get; set; }

    }
}
