using GameStoreAPI.Data;
using GameStoreAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore"); //--> El Configuration lee el appsettings

builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();  //--> Games Endpoints
app.MapGenresEndpoints(); //--> Genre Endpoints

await app.MigrateDbAsync();  //--> Migra la BD cada vez que se ejecuta el server

app.Run();
