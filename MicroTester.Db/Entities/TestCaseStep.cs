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
        public string Name { get; set; }

        [Required]
        [Include(Groups.All)]
        public HttpRequest Request { get; set; }

        [Required]
        [Include(Groups.All)]
        public HttpResponse Response { get; set; }

        TestCaseStep() 
        { 
        
        }

        public TestCaseStep(string name, HttpRequest request, HttpResponse response)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
}
