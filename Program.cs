using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Nudes.Retornator.AspnetCore;
using Nudes.SeedMaster.Interfaces;
using Nudes.SeedMaster.Seeder;
using SChallengeAPI;
using SChallengeAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json.Serialization;

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
JwtSecurityTokenHandler.DefaultMapInboundClaims =false;
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();



var builder = WebApplication.CreateBuilder(args);

var x = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly(), true);

x.ForEach(validator => builder.Services.AddTransient(validator.InterfaceType, validator.ValidatorType));

builder.Services.AddScoped(typeof(UserUniqueValidator<>));
builder.Services.AddTransient(typeof(IValidator<>), typeof(Nudes.Paginator.FluentValidation.PageRequestValidator<>));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonTimeSpanConverter()))
    .AddRetornator();

builder.Services.AddErrorTranslator(ErrorHttpTranslatorBuilder.Default);

builder.Services.AddSwaggerGen(setup =>
{

    var jwtSecurityScheme = new OpenApiSecurityScheme()
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

    setup.SwaggerDoc("schallenge", new OpenApiInfo
    {
        Title = "SChallenge Front 01 API",
        Description = "API to enable Frontend Schallenge API",
        Version = "1",
        Contact = new OpenApiContact
        {
            Email = "patrick.ens@sp.senai.br",
            Name = "Patrick",
        }
    });


    setup.DocumentFilter<DocumentsFilter>();
    setup.OperationFilter<ResultOfOperationFilter>();
    setup.SchemaFilter<SchemaFilters>();

    setup.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type  = "string",
        Example = new OpenApiString("00:00:05")
    });
    setup.CustomSchemaIds((t) => ApiConfigurator.GetSchemaId(t));

    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml"));
});


builder.Services.AddDbContextPool<Db>(options => options
    .UseInMemoryDatabase("db")
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging());

builder.Services.AddScoped<DbContext>(sp => sp.GetService<Db>());
builder.Services.AddScoped<ISeed, FullSeed>();
builder.Services.AddScoped<ISeeder, EfCoreSeeder>();

builder.Services.AddSingleton<PasswordHasher>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Security.SetupSecurityDetails(options));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(setup =>setup
    .AddDefaultPolicy(policy =>policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

/////////////////////////////////////////////////////////////////////////////
var app = builder.Build();

app.UseSwaggerUI(d => d.SwaggerEndpoint("schallenge/swagger.json", "schallenge"));

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapSwagger();
app.MapControllers();

app.Urls.Clear();
app.Urls.Add("http://localhost:5000");


using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<ISeeder>();
    await seeder.Run();
}
app.Run();