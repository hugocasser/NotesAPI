using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Notes.WebApi;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
        _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var apiVersion = description.ApiVersion.ToString();
            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo
                {
                    Version = apiVersion,
                    Title = $"Notes API {apiVersion}",
                    Description = " A simple example ASP NEt Core Web API.",
                    TermsOfService = new Uri("https://github.com/hugocasser"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gerasimuik Artem",
                        Email = "hugochacass@gmail.com",
                        Url = new Uri("https://t.me/oposuuum")
                    }
                });
            options.AddSecurityDefinition($"AuthToken {apiVersion}",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Name = "Authorization",
                    Description = "Authorization token"
                });
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = $"AuthToken{apiVersion}"
                            }
                        },
                        new string[] { }
                    }
                });
            options.CustomOperationIds(apiDescription => 
                apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) 
                    ? methodInfo.Name
                    : null);
        }
    }
}