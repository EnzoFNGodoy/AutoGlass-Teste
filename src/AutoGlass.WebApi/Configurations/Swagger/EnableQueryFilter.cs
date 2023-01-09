using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AutoGlass.WebApi.Configurations.Swagger;

public class EnableQueryFilter : IOperationFilter
{
    public static List<OpenApiParameter> SParameters { get; set; } =
        new List<(string Name, string Description)>()
    {
        ( "$top", "O máximo número de registros."),
        ( "$skip", "O número de registros para pular / ignorar."),
        ( "$filter", "Função para validar algum filtro do registro retornado."),
        ( "$select", "Especifica os campos para retornar. Use uma lista com o nome dos campos separados por virgulas. Ex.: id, description"),
        ( "$orderby", "Ordena os registros.")
    }.Select(pair => new OpenApiParameter
    {
        Name = pair.Name,
        Required = false,
        Schema = new OpenApiSchema { Type = "String" },
        In = ParameterLocation.Query,
        Description = pair.Description,

    }).ToList();

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(em => em is Microsoft.AspNetCore.OData.Query.EnableQueryAttribute))
        {

            operation.Parameters ??= new List<OpenApiParameter>();
            foreach (var item in SParameters)
                operation.Parameters.Add(item);
        }
    }
}
