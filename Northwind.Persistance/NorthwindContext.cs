using System;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;

namespace Northwind.Persistance
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}
