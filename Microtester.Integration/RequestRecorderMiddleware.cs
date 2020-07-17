using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MicroTester.Db;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Net;
using MicroTester.API;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Components.WebAssembly.Server;

namespace Microtester.Integration
{
    // See: https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
    // See also: MS response caching
    public class RequestRecorderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<TestCaseStep> _unhandledSteps = new List<TestCaseStep>();

        public RequestRecorderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICaseExtractor caseExtractor, TestContext db, IOptions<MicroTesterOptions> options, ILogger<RequestRecorderMiddleware> logger)
        {
            var request = await getRequest();

            var originalResponse = context.Response.Body;
            context.Response.Body = new MemoryStream();

            var sw = Stopwatch.StartNew();
            await _next(context);
            var responseDuration = sw.Elapsed.TotalMilliseconds;

            context.Response.Body.Position = 0;
            await context.Response.Body.CopyToAsync(originalResponse);

            var responseBodyLength = (int)context.Response.Body.Length;
            var response = await getResponse();
            
            context.Response.Body = originalResponse;

            if (context.Request.Path != new PathString("/" + MicroTesterClient.ListCasesEndpointPath) 
                && responseBodyLength <= options.Value.MaxBodySize)
            {
                _unhandledSteps.Add(new TestCaseStep(request, response));
                var cases = caseExtractor.TryExtractAsync(_unhandledSteps);
                await foreach (var testCase in cases)
                {
                    await db.Cases.AddAsync(testCase);
                }
                await db.SaveChangesAsync();
            }
            else
            {
                logger.LogWarning("Response wont be recorded");
            }

            async Task<MicroTester.Db.HttpRequest> getRequest()
            {
                var requestStream = new MemoryStream();
                await context.Request.Body.CopyToAsync(requestStream);
                context.Request.Body = requestStream;
                context.Request.Body.Position = 0;
                var body = requestStream.Length == 0
                    ? null
                    : await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
                context.Request.Body.Position = 0;
                var headers = getHeadersString(context.Request.Headers);
                var query = new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/{context.Request.QueryString}");

                return new MicroTester.Db.HttpRequest(DateTime.UtcNow, query, headers, body, (int)requestStream.Length, context.Request.Method);
            }

            async Task<MicroTester.Db.HttpResponse> getResponse()
            {
                context.Response.Body.Position = 0;
                var body = responseBodyLength == 0
                    ? null
                    : new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEnd();
                var headers = getHeadersString(context.Request.Headers);

                return new MicroTester.Db.HttpResponse(DateTime.UtcNow, headers, body, responseBodyLength, (HttpStatusCode)context.Response.StatusCode, responseDuration);
            }

            string getHeadersString(IHeaderDictionary headers)
            {
                var sb = new StringBuilder();
                foreach (var kvp in context.Request.Headers)
                {
                    sb.AppendLine($"{kvp.Key}:{kvp.Value}");
                }

                return sb.ToString();
            }
        }
    }
}
