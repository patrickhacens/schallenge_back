using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

namespace SChallengeAPI;

static class Security
{
    public const string Key = "ahjfjkashf8osahdskajmdas";
    public static void SetupSecurityDetails(JwtBearerOptions options)
    {
        options.Audience = "schallenge";
        options.SaveToken = false;
        options.IncludeErrorDetails = true;

        options.TokenValidationParameters.ValidIssuer = "http://localhost:5000";
        options.TokenValidationParameters.IgnoreTrailingSlashWhenValidatingAudience = true;
        options.TokenValidationParameters.RequireAudience = true;
        options.TokenValidationParameters.RequireExpirationTime = true;
        options.TokenValidationParameters.ValidateActor = false;
        options.TokenValidationParameters.ValidateAudience= true;
        options.TokenValidationParameters.ValidateIssuer = true;
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        options.TokenValidationParameters.ValidateIssuerSigningKey = true;
        options.TokenValidationParameters.ValidateLifetime = true;
        options.TokenValidationParameters.ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 };
    }

}
