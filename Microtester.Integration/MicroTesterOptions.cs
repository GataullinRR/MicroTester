using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Components.WebAssembly.Server;

namespace Microtester.Integration
{
    public class MicroTesterOptions
    {
        [Required]
        public string DbConnectionString { get; set; } = "Server=(localdb)\\mssqllocaldb;Database=MicroTesterDb;Trusted_Connection=True;";

        public int PendingStepsLength { get; set; } = 10 * 1024 * 1024;
    }
}
