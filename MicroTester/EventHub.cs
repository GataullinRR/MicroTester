using System;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace MicroTester
{
    class EventHub : IEventHub
    {
        public event Func<Task> CasesStateChangedAsync = () => Task.CompletedTask;

        public async Task FireCasesStateChangedAsync()
        {
            await CasesStateChangedAsync.InvokeAndWaitAsync();
        }
    }
}
