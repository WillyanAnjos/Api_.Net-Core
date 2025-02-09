using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using ApiTeste;
using ApiTeste.Application.Mapping;
using ApiTeste.Infra;
using ApiTeste.Application.Swagger;
using ApiTeste.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddOpenApi();
    ConfigureApiVersioning(services);
    ConfigureDependencies(services);
    ConfigureSwagger(services);
    ConfigureAuthentication(services);

    builder.Services.AddCors(options => {
    options.AddPolicy(name: "MinhaPoliticaDeCors",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080")//Origem do front
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddAutoMapper(typeof(DomainToDTOMapping));


}

void ConfigureApiVersioning(IServiceCollection services)
{
    services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
    });

    services.AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
}

void ConfigureDependencies(IServiceCollection services)
{
    services.AddTransient<IEmployeeRepository, EmployeeRepository>();
    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(options =>
    {
        options.OperationFilter<SwaggerDefaultValues>();
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });
}

void ConfigureAuthentication(IServiceCollection services)
{
    var key = Encoding.ASCII.GetBytes(ApiTeste.Key.Secret);

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
}

void ConfigureMiddleware(WebApplication app)
{
    var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    if (app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/error-development");
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"Web API - {description.GroupName.ToUpper()}");
            }
        });
    }
    else
    {
        app.UseExceptionHandler("/error");
    }

    app.UseCors("MinhaPoliticaDeCors");
    app.UseAuthorization();
    app.UseHttpsRedirection();
    app.MapControllers();
}
