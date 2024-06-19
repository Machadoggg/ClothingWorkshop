using ClothingWorkshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingWorkshop.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        // DbSet para otras entidades...
    }
}
