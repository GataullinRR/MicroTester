using MicroTester.Db;
using System;
using System.Linq;

namespace MicroTester
{
    public class TestCaseContext
    {
        public int Id => TestCase.Id;

        public TestCase TestCase { get; private set; }
        public TestCaseStep? SelectedStep { get; set; }

        public TestCaseContext(TestCase testCase, TestCaseStep? selectedStep)
        {
            TestCase = testCase ?? throw new ArgumentNullException(nameof(testCase));
            SelectedStep = selectedStep;
        }

        public void Reload(TestCase updatedTestCase)
        {
            var oldSteps = TestCase.Steps;
            TestCase = updatedTestCase;
            SelectedStep = TestCase.Steps.FirstOrDefault(s => s.Id == SelectedStep?.Id);
            foreach (var step in TestCase.Steps)
            {
#warning actually it can lead to bugs, because request could change in this case response wont be actual... but unlikely
                step.ActualResponse = oldSteps.FirstOrDefault(s => s.Id == step.Id)?.ActualResponse;
            }
        }
    }
}
