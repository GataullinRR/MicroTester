using System.ComponentModel.DataAnnotations;
using Utilities.Types;

namespace MicroTester.Db
{
    public class Step
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Include(Groups.All)]
        public HttpRequest Request { get; set; }

        [Required]
        [Include(Groups.All)]
        public HttpResponse Response { get; set; }
    }
}
