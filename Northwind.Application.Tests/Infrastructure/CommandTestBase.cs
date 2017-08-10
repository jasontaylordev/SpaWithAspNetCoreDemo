using System;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Tests.Infrastructure;
using Northwind.Domain;
using Northwind.Persistance;


namespace Northwind.Application.Tests
{
    public class CommandTestBase : IDisposable
    {
        protected readonly NorthwindContext _context;

        public CommandTestBase()
        {
            _context = NorthwindContextFactory.Create();
        }

        public void Dispose()
        {
            NorthwindContextFactory.Destroy(_context);
        }
    }
}