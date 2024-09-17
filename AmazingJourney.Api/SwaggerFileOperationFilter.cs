using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Collections.Generic;
using AmazingJourney.Application.DTOs;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParameters = context.MethodInfo.GetParameters().Where(p => p.ParameterType == typeof(IFormFile));

        if (fileParameters.Any())
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties =
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                },
                                ["roomDto"] = context.SchemaGenerator.GenerateSchema(typeof(RoomDTO), context.SchemaRepository)
                            },
                            Required = new HashSet<string> { "file", "roomDto" }
                        }
                    }
                }
            };
        }
    }
}
