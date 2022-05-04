using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SChallengeAPI;

class DocumentsFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach(var key in swaggerDoc.Components.Schemas.Keys.ToArray())
        {
            if (key.StartsWith("ResultOf["))
                swaggerDoc.Components.Schemas.Remove(key);
        }
    }
}
