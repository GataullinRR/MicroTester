using MicroTester.Db;
using System.Collections.Generic;
using System.Linq;

namespace MicroTester
{
    public class RootContext
    {
        public IEnumerable<TestCaseContext> Cases { get; private set; } = new List<TestCaseContext>();
        public TestCaseContext? SelectedCase { get; set; }

        public void Reload(IEnumerable<TestCase> cases)
        {
            var oldCases = Cases;
            var newCases = cases.Select(c => new TestCaseContext(c, c.Steps?.Take(1)?.SingleOrDefault())).ToList();
            foreach (var oldCase in oldCases)
            {
                var newPosition = newCases.FindIndex(c => c.Id == oldCase.Id);
                if (newPosition >= 0)
                {
                    var newCase = newCases[newPosition];
                    oldCase.Reload(newCase.TestCase);
                    newCases[newPosition] = oldCase;
                }
            }

            Cases = newCases;
        }
    }
}
