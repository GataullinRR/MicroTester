using MicroTester.Db;
using System.Collections.Generic;
//using Microsoft.AspNetCore.Components.WebAssembly.Server;

namespace MicroTester.Integration
{
    public interface ITestCaseExtractor
    {
        IAsyncEnumerable<TestCase> TryExtractAsync(IList<TestCaseStep> steps);
    }
}
