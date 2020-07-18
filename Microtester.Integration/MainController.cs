using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MicroTester.Db;
using System;
using MicroTester.API;
using Microsoft.EntityFrameworkCore;
using Utilities.Extensions;

namespace MicroTester.Integration
{
    [Controller]
    [Microsoft.AspNetCore.Components.Route("")]
    public class MainController : ControllerBase
    {
        readonly TestContext _db;

        public MainController(TestContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route(MicroTesterAPIKeys.ListCasesEndpointPath)]
        public async Task<IActionResult> ListCases([FromBody]ListCasesRequest request)
        {
            var cases = await _db.Cases
                .AsNoTracking()
                .IncludeGroup(Groups.All, _db)
                .OrderByDescending(c => c.IsPinned)
                .ThenByDescending(c => c.CreationTime)
                .Skip(request.From)
                .Take(request.To - request.From)
                .ToArrayAsync();

            return Ok(new ListCasesResponse(cases));
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route(MicroTesterAPIKeys.UpdateCasesEndpointPath)]
        public async Task<IActionResult> UpdateCases([FromBody] UpdateCasesRequest request)
        {
            foreach (var testCase in request.Cases)
            {
                _db.Entry(testCase).State = EntityState.Modified;
            }
            await _db.SaveChangesAsync();

            return Ok(new UpdateCasesResponse());
        }
    }
}
