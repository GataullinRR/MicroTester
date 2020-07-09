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

        protected HttpMessage() 
        {
            
        }
        protected HttpMessage(DateTime creationTime, string headers, string body)
        {
            CreationTime = creationTime;
            Headers = headers ?? throw new ArgumentNullException(nameof(headers));
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }
    }
}
