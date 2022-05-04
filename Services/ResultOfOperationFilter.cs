using Microsoft.OpenApi.Models;
using Nudes.Retornator.Core;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SChallengeAPI;

class ResultOfOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var returnType = context.MethodInfo.ReturnType;
        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            returnType = returnType.GenericTypeArguments[0];

        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ResultOf<>))
        {
            var successReturnType = returnType.GenericTypeArguments[0];
            var successMediaType = new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Reference = new OpenApiReference
                    {
                        Id = ApiConfigurator.GetSchemaId(successReturnType),
                        Type = ReferenceType.Schema
                    }
                }
            };

            var errorMediaType = new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Reference = new OpenApiReference
                    {
                        Id = ApiConfigurator.GetSchemaId(typeof(Error)),
                        Type = ReferenceType.Schema
                    }
                }
            };

            operation.Responses["200"] = new OpenApiResponse
            {
                Content =
                    {
                        { "application/json",  successMediaType},
                    }
            };

            operation.Responses["400"] = new OpenApiResponse
            {
                Content =
                    {
                        { "application/json", errorMediaType }
                    }
            };
        }
    }
}
