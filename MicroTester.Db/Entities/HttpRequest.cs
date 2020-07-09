using Microsoft.EntityFrameworkCore;
using System;
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
        public HttpRequest(DateTime creationTime, Uri query, string headers, string body, string method) : base(creationTime, headers, body)
        {
            URI = query ?? throw new ArgumentNullException(nameof(query));
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }
    }
}
