using SChallengeAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SChallengeAPI.Features.Events;

class AddEventHandler : IRequestHandler<AddEventRequest, ResultOf<RegisterResult>>
{
    private readonly Db db;
    private readonly ILogger<AddEventHandler> logger;
    private HttpContext context;

    public AddEventHandler(Db db, ILogger<AddEventHandler> logger, IHttpContextAccessor accessor)
    {
        this.db=db;
        this.logger=logger;
        this.context = accessor.HttpContext;
    }

    public async Task<ResultOf<RegisterResult>> Handle(AddEventRequest request, CancellationToken cancellationToken)
    {
        var _event = request.Adapt<Domain.Event>();
        if (Guid.TryParse(context.User.Claims.FirstOrDefault(d => d.Type == JwtRegisteredClaimNames.Sub)?.Value, out var userId))
            _event.CreatorId = userId;
        else
            logger.LogCritical("Add event called without an user associated");

        db.Events.Add(_event);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Event {name} ({id}) created by {userid}", _event.Name, _event.Id, _event.CreatorId);

        return new RegisterResult(_event.Id);
    }
}
