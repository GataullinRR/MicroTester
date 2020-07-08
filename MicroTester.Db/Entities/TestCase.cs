using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utilities.Types;

namespace MicroTester.Db
{
    public class TestCase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        [Required]
        [Include(Groups.All)]
        public List<Step> Steps { get; set; }
    }
}
