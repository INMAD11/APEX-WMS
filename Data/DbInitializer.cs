using APEX_WMS.Models;
using Microsoft.AspNetCore.Identity;

namespace APEX_WMS.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create database if it doesn't exist
            context.Database.EnsureCreated();

            // Add default roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Manager"))
            {
                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            if (!await roleManager.RoleExistsAsync("Operator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Operator"));
            }

            // Add seed data if database is empty
            if (!context.Suppliers.Any())
            {
                var suppliers = new Supplier[]
                {
                    new Supplier
                    {
                        SupplierName = "TechSupply Inc.",
                        ContactPerson = "John Doe",
                        Email = "john@techsupply.com",
                        Phone = "+1-555-0100",
                        Address = "123 Tech Street",
                        City = "San Francisco",
                        PostalCode = "94102",
                        Country = "USA",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsActive = true
                    },
                    new Supplier
                    {
                        SupplierName = "Global Parts Ltd.",
                        ContactPerson = "Jane Smith",
                        Email = "jane@globalparts.com",
                        Phone = "+1-555-0101",
                        Address = "456 Parts Avenue",
                        City = "New York",
                        PostalCode = "10001",
                        Country = "USA",
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsActive = true
                    }
                };

                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var products = new Product[]
                {
                    new Product
                    {
                        ProductName = "Industrial Motor",
                        SKU = "MTR-001",
                        Description = "High-efficiency 3-phase industrial motor",
                        UnitPrice = 450.00m,
                        SupplierId = 1,
                        ReorderLevel = 10,
                        ReorderQuantity = 25,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsActive = true
                    },
                    new Product
                    {
                        ProductName = "Control Panel",
                        SKU = "CPL-001",
                        Description = "Programmable control panel with touchscreen",
                        UnitPrice = 750.00m,
                        SupplierId = 2,
                        ReorderLevel = 5,
                        ReorderQuantity = 15,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsActive = true
                    },
                    new Product
                    {
                        ProductName = "Power Supply Unit",
                        SKU = "PSU-001",
                        Description = "Regulated 24V DC power supply",
                        UnitPrice = 120.00m,
                        SupplierId = 1,
                        ReorderLevel = 20,
                        ReorderQuantity = 50,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsActive = true
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
