using WebApi.Configuracoes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerConfig();

builder.Services.AddApplicationServices();


builder.Services.AddCors();

var app = builder.Build();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSwaggerConfig();

app.MapControllers();

app.Run();

//app.MapGet("/repos/me", async (IRepositorioService service) =>
//{
//    var result = await service.ListarRepositoriosDoUsuario("octocat");
//    return Results.Ok(result);
//});

//app.MapPost("/favoritos", async (Favorito favorito, IRepositorioService service) =>
//{
//    await service.AdicionarFavorito(favorito);
//    return Results.Ok();
//});

//app.MapGet("/favoritos", async (IRepositorioService service) =>
//{
//    var result = await service.ListarFavoritos();
//    return Results.Ok(result);
//});