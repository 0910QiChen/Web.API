using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using Web.API.Models;

namespace Web.API.Contexts
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("ProductContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}