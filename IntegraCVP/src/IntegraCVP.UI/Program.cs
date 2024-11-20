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

builder.Services.AddScoped<IBoletoService, BoletoService>();
builder.Services.AddScoped<IBoletoV2Service, BoletoV2Service>();
builder.Services.AddScoped<ISeguroService, SeguroService>();
builder.Services.AddScoped<ISeguroGrupoService, SeguroGrupoService>();
builder.Services.AddScoped<IDataConverterService, DataConverterService>();

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
