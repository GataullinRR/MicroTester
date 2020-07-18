using System;
using MicroTester.Db;
using System.Collections.Generic;

namespace Microtester.Integration
{
    class SimpleCaseExtractor : ITestCaseExtractor
    {
        public async IAsyncEnumerable<TestCase> TryExtractAsync(IList<TestCaseStep> steps)
        {
            foreach (var step in steps)
            {
                yield return new TestCase(
                    DateTime.UtcNow, 
                    $"{step.Request.Method} {step.Request.URI}", 
                    new List<TestCaseStep> { step });
            }

            steps.Clear();
        }
    }
}
