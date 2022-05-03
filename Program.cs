using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Nudes.Retornator.AspnetCore;
using SChallengeAPI;
using SChallengeAPI.Services;
using System.Reflection;

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

var builder = WebApplication.CreateBuilder(args);

var x = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly(), true);

x.ForEach(validator => builder.Services.AddTransient(validator.InterfaceType, validator.ValidatorType));

builder.Services.AddScoped(typeof(UserUniqueValidator<>));
builder.Services.AddTransient(typeof(IValidator<>), typeof(Nudes.Paginator.FluentValidation.PageRequestValidator<>));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

builder.Services
    .AddControllers()
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
        Description = "API to enable Frontnend Schallenge API",
        Version = "1",
        Contact = new OpenApiContact
        {
            Email = "patrick.ens@sp.senai.br",
            Name = "Patrick",
        }
    });

    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml"));
});



builder.Services.AddDbContextPool<Db>(options => options
    .UseInMemoryDatabase("db")
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging());

builder.Services.AddSingleton<PasswordHasher>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Security.SetupSecurityDetails(options));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwaggerUI(d => d.SwaggerEndpoint("schallenge/swagger.json", "schallenge"));
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapSwagger();
app.MapControllers();

app.Urls.Clear();
app.Urls.Add("https://localhost:5000");
app.Urls.Add("http://localhost:5001");

app.Run();