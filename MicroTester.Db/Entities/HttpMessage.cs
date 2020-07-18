using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using Utilities.Types;

namespace MicroTester.Db
{
    [Owned]
    public abstract class HttpMessage
    {
        [Required]
        public string HeadersString { get; set; }

        public DateTime CreationTime { get; set; }

        [Required]
        [NotMapped]
        [JsonIgnore]
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers 
        {
            get => from header in HeadersString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                   let nameLength = header.TakeWhile(c => c != ':').Count()
                   let name = header[0..nameLength]
                   let value = header[(nameLength + 1)..]
                   group value by name into grouped
                   select new KeyValuePair<string, IEnumerable<string>>(grouped.Key, grouped.ToArray());
           
            set => HeadersString = string.Join(
                        Environment.NewLine, 
                        from header in value
                        from headerValue in header.Value.ToArray()
                        select $"{header.Key}:{headerValue}"
                    );
        }

        public string? Body { get; set; }
        /// <summary>
        /// In bytes
        /// </summary>
        public int BodyLength { get; set; }

        protected HttpMessage() 
        {
            
        }

        protected HttpMessage(DateTime creationTime, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, string? body, int bodyLength)
        {
            CreationTime = creationTime;
            Headers = headers ?? throw new ArgumentNullException(nameof(headers));
            Body = body;
            BodyLength = bodyLength;
        }
    }
}
