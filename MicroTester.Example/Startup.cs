using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microtester.Integration;
using Microsoft.AspNetCore.ResponseCompression;

namespace MicroTester.Example
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; } 

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddMicroTester(Configuration.GetSection("MicroTester"));
                //services.AddBasicTestCaseExtractor();
                services.AddTestCaseExtractor<StepTestCaseExtractor>();
                services.AddRazorPages();
            }

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBlazorFrameworkFiles();
                app.UseStaticFiles();
                app.UseMicroTester();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                if (env.IsDevelopment())
                {
                    endpoints.MapRazorPages();
                    endpoints.MapFallbackToFile("", "index.html");
                }
            });
        }
    }
}
