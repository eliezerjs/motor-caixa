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

builder.Services.AddScoped<ITerminalConverterService, TerminalConverterService>();
builder.Services.AddScoped<IImportFileConverterService, ImportFileConverterService>();

builder.Services.AddScoped<IBoletoM1Service, BoletoM1Service>();
builder.Services.AddScoped<IBoletoM2Service, BoletoM2Service>();
builder.Services.AddScoped<IBoletoM3Service, BoletoM3Service>();
builder.Services.AddScoped<IBoletoM4Service, BoletoM4Service>();

builder.Services.AddScoped<ICartaRecusaService, CartaRecusaService>();
builder.Services.AddScoped<IImportFilePrevConverterService, ImportFilePrevConverterService>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBoasVindasService, BoasVindasService>();
builder.Services.AddScoped<IPrestamistaService, PrestamistaService>();

builder.Services.AddScoped<IPrevidenciaM1Service, PrevidenciaM1Service>();
builder.Services.AddScoped<IPrevidenciaM2Service, PrevidenciaM2Service>();
builder.Services.AddScoped<IPrevidenciaM3Service, PrevidenciaM3Service>();
builder.Services.AddScoped<IPrevidenciaM4Service, PrevidenciaM4Service>();
builder.Services.AddScoped<IPrevidenciaM5Service, PrevidenciaM5Service>();
builder.Services.AddScoped<IPrevidenciaM6Service, PrevidenciaM6Service>();


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
