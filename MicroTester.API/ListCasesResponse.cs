using Newtonsoft.Json;
using MicroTester.Db;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net.Http.Headers;

namespace MicroTester.API
{
    public class ListCasesResponse
    {
        [Required]
        public TestCase[] Cases { get; set; }

        public ListCasesResponse()
        {

        }

        public ListCasesResponse(TestCase[] cases)
        {
            Cases = cases ?? throw new ArgumentNullException(nameof(cases));
        }
    }
}
