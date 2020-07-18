using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace MicroTester.Db
{
    public class TestContext : DbContext
    {
        public DbSet<TestCase> Cases { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TestCaseStep>()
                .OwnsOne(s => s.Request)
                .Property(r => r.QueryValues)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v.ToArray()),
                    v => JsonConvert.DeserializeObject<KeyValuePair<string, StringValues>[]>(v));
        }
    }
}
