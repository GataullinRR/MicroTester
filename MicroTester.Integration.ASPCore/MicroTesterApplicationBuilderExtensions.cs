using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;
using Utilities;
using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using MicroTester.Integration;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MicroTester.Db;
using Microsoft.EntityFrameworkCore;

namespace MicroTester.Integration.ASPCore
{
    public static class MicroTesterApplicationBuilderExtensions
    {
        /// <summary>
        /// Should be placed at the beginning of Configure method. 
        /// 
        /// Adds: <see cref="Integration.MicroTesterApplicationBuilderExtensions.UseMicroTesterCore(IApplicationBuilder)"/>,
        /// <see cref="ComponentsWebAssemblyApplicationBuilderExtensions.UseBlazorFrameworkFiles(IApplicationBuilder)"/> and
        /// <see cref="StaticFileExtensions.UseStaticFiles(IApplicationBuilder)"/>.
        /// 
        /// Also must be added: <see cref="MicroTesterServiceCollectionExtensions.AddMicroTester{TExtractor}(IServiceCollection, IConfiguration)"/> and 
        /// <see cref="MicroTesterEndpointRouteBuilderExtensions.MapMicroTester(IEndpointRouteBuilder)"/>
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMicroTester(this IApplicationBuilder app, bool isEnabled = true)
        {
            if (isEnabled)
            {
                app.UseMicroTesterCore();
                app.UseBlazorFrameworkFiles();
                app.UseStaticFiles();
            }

            return app;
        }
    }
}
