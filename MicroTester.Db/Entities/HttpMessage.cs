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

        public string? Body { get; set; }
        public int BodyLength { get; set; }

        protected HttpMessage() 
        {
            
        }

        protected HttpMessage(DateTime creationTime, string headers, string? body, int bodyLength)
        {
            CreationTime = creationTime;
            Headers = headers ?? throw new ArgumentNullException(nameof(headers));
            Body = body;
            BodyLength = bodyLength;
        }
    }
}
