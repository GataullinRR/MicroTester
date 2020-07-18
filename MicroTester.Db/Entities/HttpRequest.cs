using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using Utilities.Types;

namespace MicroTester.Db
{
    [Owned]
    public class HttpRequest : HttpMessage
    {
        [Required]
        public Uri URI { get; set; }

        [Required]
        public string Method { get; set; }

        public KeyValuePair<string, StringValues>[]? QueryValues { get; set; }

        public HttpRequest()
        {

        }

        public HttpRequest(DateTime creationTime, 
            Uri uri,
            KeyValuePair<string, StringValues>[]? queryValues,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, 
            string? body, 
            int bodyLength, 
            string method) 
            : base(creationTime, headers, body, bodyLength)
        {
            QueryValues = queryValues;
            URI = uri ?? throw new ArgumentNullException(nameof(uri));
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }
    }
}
