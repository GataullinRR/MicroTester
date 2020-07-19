using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MicroTester.Integration;
using Microsoft.AspNetCore.ResponseCompression;
using MicroTester.Integration.ASPCore;

namespace MicroTester.ExampleApp
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
            services.AddMicroTester(Configuration.GetSection("MicroTester"), Environment.IsDevelopment());
            // Try also:
            //services.AddMicroTester<StepTestCaseExtractor>(Configuration.GetSection("MicroTester"), Environment.IsDevelopment());

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMicroTester(env.IsDevelopment());
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMicroTester(env.IsDevelopment());
            });
        }
    }
}
