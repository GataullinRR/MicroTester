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
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;
//using Microsoft.AspNetCore.Components.WebAssembly.Server;

namespace Microtester.Integration
{
    // See: https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
    // See also: MS response caching
    public class RequestRecorderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<TestCaseStep> _unhandledSteps = new List<TestCaseStep>();
        private readonly SemaphoreSlim _locker = new SemaphoreSlim(1);
        private readonly IOptions<MicroTesterOptions> _options;
        private readonly ILogger<RequestRecorderMiddleware> _logger;

        public RequestRecorderMiddleware(IOptions<MicroTesterOptions> options, RequestDelegate next, ILogger<RequestRecorderMiddleware> logger)
        {
            _next = next;
            _options = options;
            _logger = logger;

            CleanupDaemon();
        }

        private async void CleanupDaemon()
        {
            while (true)
            {
                try
                {
                    await Task.Delay(60 * 1000);

                    await _locker.WaitAsync();
                    _unhandledSteps.RemoveAll(s => (DateTime.UtcNow - s.Request.CreationTime) > _options.Value.UnsavedStepLifetime);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Having troubles removing unsaved tests. Memory consumption may rise.");
                }
                finally
                {
                    _locker.Release();
                }
            }
        }

        public async Task InvokeAsync(HttpContext context, ITestCaseExtractor caseExtractor, TestContext db)
        {
            var request = await GetRequest(context);

            var originalResponse = context.Response.Body;
            context.Response.Body = new MemoryStream();

            var sw = Stopwatch.StartNew();
            await _next(context);
            var responseDuration = sw.Elapsed.TotalMilliseconds;

            context.Response.Body.Position = 0;
            await context.Response.Body.CopyToAsync(originalResponse);

            var response = await GetResponse(context, responseDuration);
            
            context.Response.Body = originalResponse;

            var pathForbidden = MicroTesterAPIKeys.AllPaths.Any(p => p == context.Request.Path)
                || context.Request.Path == new PathString("/index.html");
            var requestBodyTooBig = request.BodyLength > (_options?.Value?.MaxBodySize ?? 0);
            var responseBodyTooBig = response.BodyLength > (_options?.Value?.MaxBodySize ?? 0);
            if (pathForbidden)
            {
                _logger.LogWarning($"Response to path {context.Request.Path} wont be recorded (the path is not subjected to be recorded)");
            }
            else if(requestBodyTooBig)
            {
                _logger.LogWarning($"Response to path {context.Request.Path} wont be recorded (request body is too big)");
            }
            else if (responseBodyTooBig)
            {
                _logger.LogWarning($"Response to path {context.Request.Path} wont be recorded (response body is too big)");
            }
            else
            {
                var ok = false;
                try
                {
                    await _locker.WaitAsync();

                    _unhandledSteps.Add(new TestCaseStep(request.URI.PathAndQuery, request, response));
                    var cases = caseExtractor.TryExtractAsync(_unhandledSteps);
                    await foreach (var testCase in cases)
                    {
                        await db.Cases.AddAsync(testCase);
                    }

                    ok = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Having troubles extracting test cases while using {caseExtractor?.GetType()} extractor. Cases weren't extracted, recorded steps may be lost.");
                }
                finally
                {
                    _locker.Release();
                }

                if (ok)
                {
                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Having troubles saving cases to database {_options?.Value?.DbConnectionString}. Cases were lost.");
                    }
                }
            }
        }

        private async Task<MicroTester.Db.HttpRequest> GetRequest(HttpContext context)
        {
            var requestStream = new MemoryStream();
            await context.Request.Body.CopyToAsync(requestStream);
            context.Request.Body = requestStream;
            context.Request.Body.Position = 0;
            var body = requestStream.Length == 0
                ? null
                : await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
            context.Request.Body.Position = 0;
            var headers = GetHeadersString(context.Request.Headers);
            var fullUri = new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/{context.Request.QueryString}");

            return new MicroTester.Db.HttpRequest(DateTime.UtcNow,
                fullUri,
                context.Request.Query.ToArray(),
                headers,
                body,
                (int)requestStream.Length,
                context.Request.Method);
        }

        private async Task<MicroTester.Db.HttpResponse> GetResponse(HttpContext context, double requestHandlingDuration)
        {
            var responseBodyLength = (int)context.Response.Body.Length;
            context.Response.Body.Position = 0;
            var body = responseBodyLength == 0
                ? null
                : new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEnd();
            var headers = GetHeadersString(context.Response.Headers);

            return new MicroTester.Db.HttpResponse(
                DateTime.UtcNow, 
                headers, 
                body, 
                responseBodyLength, 
                (HttpStatusCode)context.Response.StatusCode,
                requestHandlingDuration);
        }

        private IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetHeadersString(IHeaderDictionary headers)
        {
            foreach (var kvp in headers)
            {
                yield return new KeyValuePair<string, IEnumerable<string>>(kvp.Key, kvp.Value);
            }
        }
    }
}
