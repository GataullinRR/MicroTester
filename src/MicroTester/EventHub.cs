using System;
using System.Linq;
using System.Threading.Tasks;

namespace MicroTester
{
    class EventHub : IEventHub
    {
        public event Func<Task> CasesStateChangedAsync = () => Task.CompletedTask;

        public async Task FireCasesStateChangedAsync()
        {
            foreach (var func in @CasesStateChangedAsync?.GetInvocationList()?.Cast<Func<Task>>() ?? Enumerable.Empty<Func<Task>>())
            {
                await func();
            }
        }
    }
}
