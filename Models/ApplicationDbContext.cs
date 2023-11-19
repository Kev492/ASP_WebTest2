using Microsoft.EntityFrameworkCore;

namespace AspWebTest2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> CUSTOMER { get; set; }
        public DbSet<Product> PRODUCT { get; set; }
    }
}
