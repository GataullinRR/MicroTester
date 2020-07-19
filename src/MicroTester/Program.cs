using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MicroTester.API;

namespace MicroTester
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var services = builder.Services;
            services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services.AddMicroTesterClient();
            services.AddSingleton<IEventHub, EventHub>();
            services.AddHostedService<CasesChangedNotifier>(); // Doesn't work for Blazor out of the box, see workaround inside App.razor

            await builder.Build().RunAsync();
        }
    }
}
