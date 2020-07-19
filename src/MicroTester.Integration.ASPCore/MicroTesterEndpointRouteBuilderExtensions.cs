using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace MicroTester.Integration.ASPCore
{
    public static class MicroTesterEndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Serves Blazor WASM UI. 
        /// 
        /// Adds: <see cref="RazorPagesEndpointRouteBuilderExtensions.MapRazorPages(IEndpointRouteBuilder)"/> and 
        /// <see cref="StaticFilesEndpointRouteBuilderExtensions.MapFallbackToFile(IEndpointRouteBuilder, string, string)"/>.
        /// 
        /// Also must be added: <see cref="MicroTesterServiceCollectionExtensions.AddMicroTester{TExtractor}(IServiceCollection, IConfiguration)"/> and 
        /// <see cref="MicroTesterApplicationBuilderExtensions.UseMicroTester(IApplicationBuilder)"/>.
        /// </summary>
        /// <param name="endpoints"></param>
        /// <returns></returns>
        public static IEndpointRouteBuilder MapMicroTester(this IEndpointRouteBuilder endpoints, bool isEnabled = true)
        {
            if (isEnabled)
            {
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("", "index.html");
            }

            return endpoints;
        }
    }
}
