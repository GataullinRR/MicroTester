using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroTester.Db
{
    public class TestContext : DbContext
    {
        public DbSet<TestCase> Cases { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
