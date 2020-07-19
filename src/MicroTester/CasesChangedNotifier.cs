using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroTester
{
    class CasesChangedNotifier : IHostedService
    {
        readonly IEventHub _hub;

        public CasesChangedNotifier(IEventHub hub)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            daemon();
        }

        private async Task daemon()
        {
            while (true)
            {
                await Task.Delay(3000);

                try
                {
                    await _hub.FireCasesStateChangedAsync();
                }
                catch { }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            
        }
    }
}
