using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utilities.Types;

namespace MicroTester.Db
{
    public class TestCase
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public bool IsPinned { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Include(Groups.All)]
        public List<TestCaseStep> Steps { get; set; } = new List<TestCaseStep>();

        public TestCase() // For EF
        {

        }

        public TestCase(DateTime creationTime, string name, List<TestCaseStep> steps)
        {
            CreationTime = creationTime;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }
    }
}
