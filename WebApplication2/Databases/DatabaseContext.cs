using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Databases
{
    public class DatabaseContext : DbContext
    {
        
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoHistory> TodoHistories { get; set; }

        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
