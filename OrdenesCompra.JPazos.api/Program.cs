using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OrdenesCompra.JPazos.application.Configs;
using OrdenesCompra.JPazos.application.IServices;
using OrdenesCompra.JPazos.application.Services;
using OrdenesCompra.JPazos.domain.IRepositories;
using OrdenesCompra.JPazos.infrastructure.Context;
using OrdenesCompra.JPazos.infrastructure.Repositories;
using OrdenesCompra.JPazos.api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OrdenesCompra.JPazos.application.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MappingConfig.RegisterMappings(TypeAdapterConfig.GlobalSettings);
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddMapster();
builder.Services.AddDbContext<OrdenesCompraContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("OrdenesCompraDB")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IOrdenService, OrdenService>();
builder.Services.AddTransient<IOrdenRepository, OrdenRepository>();
builder.Services.AddTransient<ISecurityService, SecurityService>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddScoped<OrdenesCompraContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey")!))
    };
}
);

//Para habilitar la autenticacion por token en swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "OrdenesCompra.JPazos.api",
        Version = "1.0",
        Description = "Gestionar órdenes de compra con un modelo de cabecera y detalle",
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Ingrese el token en el siguiente formato: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS
app.UseCors("AllowAllOrigins");

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();