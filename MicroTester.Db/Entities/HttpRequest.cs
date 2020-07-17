using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace MicroTester.Db
{
    [Owned]
    public class HttpRequest : HttpMessage
    {
        [Required]
        public Uri URI { get; set; }

        [Required]
        public string Method { get; set; }

        public HttpRequest()
        {

        }

        public HttpRequest(DateTime creationTime, Uri query, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, string? body, int bodyLength, string method) 
            : base(creationTime, headers, body, bodyLength)
        {
            URI = query ?? throw new ArgumentNullException(nameof(query));
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }
    }
}
