using System.Text;
using FastEndpoints;
using FastEndpoints.Swagger;
using KanBanApi.Application.Dtos;
using KanBanApi.Infraestructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? String.Empty))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddFastEndpoints()
                .SwaggerDocument();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowLocalhostPolicy", bld =>
    {
        bld.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});

var googleConfig = new GoogleConfigDto(builder.Configuration["Google:ClientId"]!,
                                       builder.Configuration["Google:ClientSecret"]!);

builder.Services.AddSingleton(googleConfig);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(opt =>
{
    opt.Endpoints.RoutePrefix = "api";
});

app.UseCors("AllowLocalhostPolicy");

if (app.Environment.IsDevelopment()) app.UseSwaggerGen();

app.Run();
