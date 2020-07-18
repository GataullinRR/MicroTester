using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace MicroTester.Integration.ASPCore
{
    public static class MicroTesterServiceCollectionExtensions
    {
        /// <summary>
        /// Alias for <see cref="AddMicroTester{TExtractor}(IServiceCollection, IConfiguration)"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMicroTester(this IServiceCollection services, IConfiguration configuration, bool isEnabled = true)
        {
            if (isEnabled)
            {
                services.AddMicroTesterCore(configuration);
                services.AddBasicTestCaseExtractor();
                services.AddRazorPages();
            }
             
            return services;
        }

        /// <summary>
        /// Adds: <see cref="ServicesEx.AddMicroTesterCore(IServiceCollection, IConfiguration)"/>,
        /// <see cref="ServicesEx.AddTestCaseExtractor{TImplementation}(IServiceCollection)"/> and
        /// <see cref="MvcServiceCollectionExtensions.AddRazorPages(IServiceCollection)"/>.
        /// 
        /// Also must be added: <see cref="MicroTesterEndpointRouteBuilderExtensions.MapMicroTester(IEndpointRouteBuilder)"/> and 
        /// <see cref="MicroTesterApplicationBuilderExtensions.UseMicroTester(IApplicationBuilder)"/>
        /// 
        /// </summary>
        /// <typeparam name="TExtractor"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMicroTester<TExtractor>(this IServiceCollection services, IConfiguration configuration, bool isEnabled = true)
            where TExtractor : class, ITestCaseExtractor
        {
            if (isEnabled)
            {
                services.AddMicroTesterCore(configuration);
                services.AddTestCaseExtractor<TExtractor>();
                services.AddRazorPages();
            }

            return services;
        }
    }
}
