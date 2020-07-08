using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace MicroTester.Db
{
    [Owned]
    public abstract class HttpMessage
    {
        public DateTime CreationTime { get; set; }

        [Required]
        public string Headers { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public HttpMethod Method { get; set; }
    }
}
