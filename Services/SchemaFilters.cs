using Microsoft.OpenApi.Models;
using Nudes.Retornator.Core;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SChallengeAPI;

class SchemaFilters : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(FieldErrors))
        {
            schema.Properties["field_name"] = new OpenApiSchema()
            {
                Type = "array",
                Items = new OpenApiSchema
                {
                    Type = "string"
                }
            };
        }
    }
}
