using Microsoft.AspNetCore.Mvc.ModelBinding;

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
