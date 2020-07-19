using System;
using MicroTester.Db;
using System.Collections.Generic;

namespace MicroTester.Integration
{
    internal class SimpleCaseExtractor : ITestCaseExtractor
    {
        public async IAsyncEnumerable<TestCase> TryExtractAsync(IList<TestCaseStep> steps)
        {
            foreach (var step in steps)
            {
                yield return new TestCase(
                    DateTime.UtcNow, 
                    $"{step.Request.Method} {step.Request.URI.Scheme} {step.Request.URI.PathAndQuery}", 
                    new List<TestCaseStep> { step });
            }

            steps.Clear();
        }
    }
}
