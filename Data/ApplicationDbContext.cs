
using Microsoft.EntityFrameworkCore;
using AcxiomCRM.Models;

namespace AcxiomCRM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Lead> Leads { get; set; }
    }
}
