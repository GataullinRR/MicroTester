using MicroTester.Db;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MicroTester.API
{
    public class UpdateCasesRequest
    {
        [Required]
        public TestCase[] Cases { get; set; }

        public UpdateCasesRequest()
        {

        }

        public UpdateCasesRequest(params TestCase[] cases)
        {
            Cases = cases ?? throw new ArgumentNullException(nameof(cases));
        }
    }
}
