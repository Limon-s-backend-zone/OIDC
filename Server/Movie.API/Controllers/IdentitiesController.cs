using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Movie.API.Controllers;
[Route("api/[controller]")]
[ApiController]
// [Authorize("ClientIdPolicy")]
[Authorize]
public class IdentitiesController: ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetClaims()
    {
        var result = from c in User.Claims select new { c.Type, c.Value };
        await Task.CompletedTask;
        return Ok(result);
    }
}