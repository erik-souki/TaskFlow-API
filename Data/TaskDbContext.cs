using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Entities;

namespace TaskFlow_API.Data
{
    public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
    {
        public DbSet<Chore> Tasks => Set<Chore>();
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração adicional das entidades (se necessário)
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Chore>().HasKey(c => c.Id);
        }


    }
}
