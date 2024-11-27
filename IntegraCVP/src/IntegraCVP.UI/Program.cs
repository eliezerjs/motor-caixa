using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntegraCVP API", Version = "v1" });
    c.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
});


builder.Services.AddControllers();

builder.Services.AddScoped<IBoletoM1Service, BoletoM1Service>();
builder.Services.AddScoped<IBoletoM2Service, BoletoM2Service>();
builder.Services.AddScoped<IBoletoM3Service, BoletoM3Service>();
builder.Services.AddScoped<IBoletoM4Service, BoletoM4Service>();

builder.Services.AddScoped<IPrestamistaService, PrestamistaService>();

builder.Services.AddScoped<IReturnDataConverterService, ReturnDataConverterService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IntegraCVP API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.Run();
