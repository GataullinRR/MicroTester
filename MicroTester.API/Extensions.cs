using Microsoft.Extensions.DependencyInjection;

namespace MicroTester.API
{
    public static class Extensions
    {
        public static IServiceCollection AddMicroTesterClient(this IServiceCollection services)
        {
            services.AddScoped<IMicroTesterClient, MicroTesterClient>();

            return services;
        }
    }
}
