using System.Data.Entity;
using Tasklist.Domain.Entities;

namespace Tasklist.PersistentStorage.Contexto
{
    public class TaskContext : DbContext
    {
        public TaskContext() : base(ConnectionString.CONNECTION) {
        }
        public DbSet<Task> Tasks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().Property(p => p.Title).IsRequired();
        }
    }
}
