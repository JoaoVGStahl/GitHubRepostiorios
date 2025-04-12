using WebApi.Configuracoes;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerConfig();

builder.Services.AddApplicationServices();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSwaggerConfig();

app.MapControllers();

app.UseMiddleware<ExceptionHandler>();

app.Run();