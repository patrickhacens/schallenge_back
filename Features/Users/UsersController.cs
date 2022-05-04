using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SChallengeAPI.Models;

namespace SChallengeAPI.Features.Users;

/// <summary>
/// User resources
/// </summary>
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public UsersController(IMediator mediator)
    {
        this.mediator=mediator;
    }


    /// <summary>
    /// Action to add user
    /// </summary>
    [HttpPost]
    public Task<ResultOf<RegisterResult>> Add([FromBody] AddUserRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);

    /// <summary>
    /// Action to list user, paginated
    /// </summary>
    [HttpGet]
    public Task<ResultOf<PageResult<Reference>>> List([FromQuery] ListUsersRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);

    /// <summary>
    /// Action to remove user
    /// </summary>
    [HttpDelete("{Id}")]
    public Task<Result> Remove([FromRoute] RemoveUserRequest request, CancellationToken cancellation)
        => mediator.Send(request, cancellation);



}
