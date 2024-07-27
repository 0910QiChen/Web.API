using System.Collections.Generic;
using System.Data.Entity;
using Web.API.Models;

namespace Web.API.Contexts
{
    public class ProductInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            var products = new List<Product>
            {
                new Product { Name = "Product1", Quantity = 10, Price = 9.99M, Description = "Description1" },
                new Product { Name = "Product2", Quantity = 15, Price = 14.99M, Description = "Description2" },
                new Product { Name = "Product3", Quantity = 20, Price = 19.99M, Description = "Description3" },
                new Product { Name = "Product4", Quantity = 25, Price = 24.99M, Description = "Description4" },
                new Product { Name = "Product5", Quantity = 30, Price = 29.99M, Description = "Description5" },
                new Product { Name = "Product6", Quantity = 35, Price = 34.99M, Description = "Description6" },
                new Product { Name = "Product7", Quantity = 40, Price = 39.99M, Description = "Description7" },
                new Product { Name = "Product8", Quantity = 45, Price = 44.99M, Description = "Description8" },
                new Product { Name = "Product9", Quantity = 50, Price = 49.99M, Description = "Description9" },
                new Product { Name = "Product10", Quantity = 55, Price = 54.99M, Description = "Description10" },
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            var users = new List<Users>
            {
                new Users { UserName = "Admin", UserPassword = "AdminPassword" },
                new Users { UserName = "abc@gmail.com", UserPassword = "123456789" },
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}