using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace MicroTester.API
{
    public class ListCasesRequest
    {
        [BindRequired]
        public int From { get; set; }

        [BindRequired]
        public int To { get; set; }

        public ListCasesRequest()
        {

        }

        public ListCasesRequest(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}
