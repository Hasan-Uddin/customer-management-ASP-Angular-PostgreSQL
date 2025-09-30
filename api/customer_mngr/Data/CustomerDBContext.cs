using customer_mngr.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace customer_mngr.Data
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions options) : base(options)
        {
        }

        protected CustomerDBContext()
        {
        }

        public DbSet<Customer> Customers { get; set; }



    }
}
