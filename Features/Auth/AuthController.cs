using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Auth;

/// <summary>
/// Controller for authentication
/// </summary>
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public AuthController(IMediator mediator)
    {
        this.mediator=mediator;
    }

    /// <summary>
    /// Authentication endpoint
    /// </summary>
    [HttpPost]
    public Task<ResultOf<AuthResult>> Auth([FromBody] AuthRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);

    /// <summary>
    /// Checking authorization and claims
    /// </summary>
    [Authorize]
    [HttpGet]
    public IActionResult Check()
    {
        return Ok(User.Identities.SelectMany(d => d.Claims).Select(d => new
        {
            d.Type,
            d.Value
        }).ToList());
    }
}
