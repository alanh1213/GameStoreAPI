using GameStoreAPI.Data;
using GameStoreAPI.Endpoints;
using GameStoreAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore"); //--> El Configuration lee el appsettings

builder.Services.AddSqlite<GameStoreContext>(connString);
builder.Services.AddScoped<IGamesRepository, GamesRepository>();

var app = builder.Build();

app.MapGamesEndpoints();  //--> Games Endpoints
app.MapGenresEndpoints(); //--> Genre Endpoints

await app.MigrateDbAsync();  //--> Migra la BD cada vez que se ejecuta el server

app.Run();
