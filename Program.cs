using crudTest;
using crudTest.Funciones;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("validarConsumo", configuracion =>
    {        
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "API-Portal-Usuario",
            Description = "Servicio."
        }
        );
    }
);

builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<IFunctionsApi, FunctionsApi>();
var app = builder.Build();
app.UseCors("validarConsumo");
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
