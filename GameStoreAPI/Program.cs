using GameStoreAPI.Data;
using GameStoreAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore"); //El Configuration lee el appsettings

builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
