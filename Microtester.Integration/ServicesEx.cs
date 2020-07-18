using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicroTester.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Microtester.Integration
{
    public static class ServicesEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"><see cref="MicroTesterOptions"/></param>
        /// <returns></returns>
        public static IServiceCollection AddMicroTester(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MicroTesterOptions>(configuration);
            services.AddTransient(sp => sp.GetRequiredService<IOptions<MicroTesterOptions>>().Value);
            services.AddDbContext<TestContext>((sp, dbOptions) =>
            {
                var options = sp.GetRequiredService<MicroTesterOptions>();
                dbOptions.UseSqlServer(options.DbConnectionString);
            });

            return services;
        }

        public static IServiceCollection AddBasicTestCaseExtractor(this IServiceCollection services)
        {
            services.AddTestCaseExtractor<SimpleCaseExtractor>();

            return services;
        }

        public static IServiceCollection AddTestCaseExtractor<TImplementation>(this IServiceCollection services)
            where TImplementation : class, ITestCaseExtractor
        {
            services.AddScoped<ITestCaseExtractor, TImplementation>();

            return services;
        }
    }
}
