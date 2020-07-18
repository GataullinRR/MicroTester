using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;
using Utilities;
using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;

namespace MicroTester.Integration
{
    public static class ApplicationEx
    {
        public static IApplicationBuilder UseMicroTesterCore(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestRecorderMiddleware>();

            return app;
        }
    }
}
