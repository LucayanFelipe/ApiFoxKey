using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace ApiLocadora.Swagger
{
    public class TimeSpanSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(TimeSpan) || context.Type == typeof(TimeSpan?))
            {
                schema.Type = "string";
                schema.Format = "time";
                schema.Example = new OpenApiString("14:30:00");
            }
        }
    }
}
