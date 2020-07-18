using MicroTester.Db;
using System.Collections.Generic;
//using Microsoft.AspNetCore.Components.WebAssembly.Server;

namespace Microtester.Integration
{
    public interface ITestCaseExtractor
    {
        IAsyncEnumerable<TestCase> TryExtractAsync(IList<TestCaseStep> steps);
    }
}
