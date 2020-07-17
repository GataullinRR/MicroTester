using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MicroTester.Db;
using System;
using MicroTester.API;
using Microsoft.EntityFrameworkCore;
using Utilities.Extensions;

namespace Microtester.Integration
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
        [Microsoft.AspNetCore.Mvc.Route(MicroTesterClient.ListCasesEndpointPath)]
        public async Task<IActionResult> ListCases([FromBody]ListCasesRequest request)
        {
            var cases = await _db.Cases
                .Include(c => c.Steps)
                .ThenInclude(s => s.Request)
                .Include(c => c.Steps)
                .ThenInclude(s => s.Response)
                //.IncludeGroup(Groups.All, _db)
                .OrderByDescending(c => c.CreationTime)
                .Skip(request.From)
                .Take(request.To - request.From)
                .ToArrayAsync();

            return Ok(new ListCasesResponse(cases));
        }
    }
}
