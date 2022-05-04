using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SChallengeAPI.Models;
using SChallengeAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SChallengeAPI.Features.Auth;

class AuthHandler : IRequestHandler<AuthRequest, ResultOf<AuthResult>>
{
    private const string errorMessage = "User does not exists or not found";
    private readonly Db db;
    private readonly ILogger<AuthHandler> logger;
    private readonly JwtBearerOptions options;

    public AuthHandler(Db db,
        ILogger<AuthHandler> logger,
        IOptions<JwtBearerOptions> jwtOptions)
    {
        this.db=db;
        this.logger=logger;
        this.options=jwtOptions.Value;
        Security.SetupSecurityDetails(this.options);
    }


    public async Task<ResultOf<AuthResult>> Handle(AuthRequest request, CancellationToken cancellationToken)
    {
        var user = await db.Users.AsNoTracking().SingleOrDefaultAsync(d => d.Username == request.Username, cancellationToken);
        if (user == null)
        {
            logger.LogWarning("User {username} not found", request.Username);
            return new ForbiddenError()
            {
                Description = errorMessage
            };
        }
        if (!PasswordHasher.IsEquals(request.Password, user.Hash, user.Salt))
        {
            logger.LogWarning("User {username} tried to authenticate with incorrect password", request.Username);
            return new ForbiddenError()
            {
                Description = errorMessage
            };
        }

        logger.LogInformation("User {username} has sucessfully authenticated", request.Username);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, request.Username),
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Security.Key));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            options.TokenValidationParameters.ValidIssuer,
            options.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred);
        
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new AuthResult
        {
            AccessToken = accessToken,
        };
    }
}
