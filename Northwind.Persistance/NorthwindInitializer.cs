using System.Linq;
using Northwind.Domain;

namespace Northwind.Persistance
{
    public class NorthwindInitializer
    {
        public static void Initialize(NorthwindContext context)
        {
            if (context.Customers.Any())
            {
                return; // Db already seeded
            }
            
            var customers = new[]
            {
                new Customer { Name = "Adam Cogan" },
                new Customer { Name = "Adam Stephenson" },
                new Customer { Name = "Brendan Richards" },
                new Customer { Name = "Thiago Passos" },
                new Customer { Name = "Duncan Hunter" },
                new Customer { Name = "Mr. Meeseeks" },
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}