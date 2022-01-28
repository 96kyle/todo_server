using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Databases
{
    [Table("todo")]
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool IsDone { get; set; }

        public bool IsTopFixed { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedTime { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public virtual Collection<TodoHistory> TodoHistories { get; set; }

    }
}
