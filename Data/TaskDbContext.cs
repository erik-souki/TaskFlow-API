using Microsoft.EntityFrameworkCore;

namespace TaskFlow_API.Data
{
    public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
    {
        public DbSet<Chore> Tasks => Set<Chore>();



    }
}
