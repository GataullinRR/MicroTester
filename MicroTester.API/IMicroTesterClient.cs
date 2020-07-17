using System.Threading.Tasks;

namespace MicroTester.API
{
    public interface IMicroTesterClient
    {
        Task<ListCasesResponse> ListCasesAsync(ListCasesRequest request);
        Task<UpdateCasesResponse> UpdateCasesAsync(UpdateCasesRequest request);
    }
}
