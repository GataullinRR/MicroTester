using System;
using System.Threading.Tasks;

namespace MicroTester
{
    // Other ways: https://chrissainty.com/3-ways-to-communicate-between-components-in-blazor/
    public interface IEventHub
    {
        event Func<Task> CasesStateChangedAsync;

        Task FireCasesStateChangedAsync();
    }
}
