using System;
using System.ComponentModel.DataAnnotations;
using Utilities.Types;

namespace MicroTester.Db
{
    public class TestCaseStep
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Include(Groups.All)]
        public HttpRequest Request { get; set; }

        [Required]
        [Include(Groups.All)]
        public HttpResponse Response { get; set; }

        TestCaseStep() 
        { 
        
        }
        public TestCaseStep(HttpRequest request, HttpResponse response)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
}
