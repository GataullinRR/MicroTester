using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace MicroTester.Db
{
    [Owned]
    public class HttpRequest : HttpMessage
    {
        [Required]
        public Uri Query { get; set; }
    }
}
