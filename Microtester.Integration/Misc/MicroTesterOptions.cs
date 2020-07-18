using System;
using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Components.WebAssembly.Server;

namespace MicroTester.Integration
{
    public class MicroTesterOptions
    {
        [Required]
        public string DbConnectionString { get; set; } = "Server=(localdb)\\mssqllocaldb;Database=MicroTesterDb;Trusted_Connection=True;";

        public TimeSpan UnsavedStepLifetime { get; set; } = TimeSpan.FromMinutes(30);
        public int MaxBodySize { get; set; } = 10 * 1024;
    }
}
