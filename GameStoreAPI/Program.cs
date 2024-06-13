using GameStoreAPI.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> juegos = [
        new (
            1, 
            "Street Fighter II",
            "Peleas",
            19.99M,
            new DateOnly(1992, 7, 15)),
        new (
            2,
            "Final Fantasy XIV",
            "Juego de rol",
            59.99M,
            new DateOnly(2010, 9, 30)),
        new (
            3,
            "FIFA 23",
            "Deportes",
            69.99M,
            new DateOnly(2022, 9, 27)),

    ];

// GET /games
app.MapGet("juegos", () => juegos);

// GET /games/1
app.MapGet("juegos/{id}", (int id) => juegos.Find(juego => juego.Id == id))
   .WithName("GetGame"); //---> WithName le agrega un nombre para poder usar el endpoint desde dentro del servidor


// POST /games
// El cliente accede a este endpoint con un objeto de tipo CreateGameDto.
// El cuerpo de la funcion lambda lo convierte en un Dto interno con ID.
// Y luego agrega el juego a la lista
// Finalmente devuelve al cliente el codigo 201, con el id en forma de tipo anonimo (por convencion) y el objeto dentro de la lista
app.MapPost("juegos", (CreateGameDto nuevoJuego) =>
{
    GameDto juego = new(
            juegos.Count + 1,
            nuevoJuego.Nombre,
            nuevoJuego.Genero,
            nuevoJuego.Precio,
            nuevoJuego.FechaLanzamiento
        );

    juegos.Add(juego);
    return Results.CreatedAtRoute("GetGame", new { id = juego.Id }, juego);
});

// PUT /games/1
app.MapPut("juegos/{id}", (int id, UpdateGameDto updateJuego) =>
{
    var index = juegos.FindIndex(game => game.Id == id);

    juegos[index] = new GameDto(
            id,
            updateJuego.Nombre,
            updateJuego.Genero,
            updateJuego.Precio,
            updateJuego.FechaLanzamiento
        );

    return Results.NoContent(); //Por convencion se retorna NoContent
});

//DELETE
app.MapDelete("juegos/{id}", (int id) =>
{
    var index = juegos.FindIndex(juego => juego.Id == id);

    juegos.RemoveAt(index);

    return Results.NoContent();
});

app.Run();
