using System;
using System.Collections.Generic;
using System.Linq;
using MicroTester.Integration;
using MicroTester.Db;
using Newtonsoft.Json;
using MicroTester.Example.Controllers;
using Microsoft.Extensions.Primitives;

namespace MicroTester.Example
{
    class StepTestCaseExtractor : ITestCaseExtractor
    {
        public async IAsyncEnumerable<TestCase> TryExtractAsync(IList<TestCaseStep> steps)
        {
            var cases = from step in steps
                        where step.Request.Body != null
                        let clientId = JsonConvert.DeserializeObject<StepRequest>(step.Request.Body)?.ClientId
                        where clientId != null
                        group step by clientId into sequence
                        let isFinished = sequence.Any(s => s.Request.URI.ToString().EndsWith("step2/"))
                        where isFinished
                        select sequence;
            foreach (var unsavedCase in cases)
            {
                var stepsCount = unsavedCase
                    .TakeWhile(rs => !rs.Request.URI.ToString().EndsWith("step2/"))
                    .Count() + 1;
                var pendingSteps = unsavedCase
                    .Take(stepsCount)
                    .ToList();
                foreach (var step in pendingSteps)
                {
                    steps.Remove(step);
                }

                yield return new TestCase(DateTime.UtcNow, $"Multistate for cid:{unsavedCase.Key}", pendingSteps);
            }
        }
    }
}
