using System;
using System.Net;
using System.Net.Http;

namespace MicroTester.Db
{
    public class HttpResponse : HttpMessage
    {
        /// <summary>
        /// ms
        /// </summary>
        public double Duration { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public HttpResponse()
        {

        }

        public HttpResponse(DateTime creationTime, string headers, string? body, int bodyLength, HttpStatusCode statusCode, double duration) 
            : base(creationTime, headers, body, bodyLength)
        {
            StatusCode = statusCode;
            Duration = duration;
        }
    }
}
