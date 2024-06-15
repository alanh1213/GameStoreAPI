using GameStoreAPI.Dtos;

namespace GameStoreAPI.Endpoints
{
    public static class GamesEndpoints
    {
        //*******************************************************************************************
        // Se extiende la clase WebApplication con los metodos CRUD, de forma separada del Program.cs
        //*******************************************************************************************

        private static readonly List<GameDto> juegos = [
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


        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("juegos") //el group reemplaza al app en los endpoints y ya no hace falta
                                               //hardcodear "juegos" en cada uno de ellos.
                           .WithParameterValidation();  //---> Este filtro fuerza que se validen los anticuados Data Annotations
                                                        //Se puede poner en cada endpoint pero el group los agrega automaticamente
                                                         

            // GET /games
            group.MapGet("/", () => juegos);

            // GET /games/1
            group.MapGet("/{id}", (int id) =>
            {
                var juego = juegos.Find(juego => juego.Id == id);

                return juego is null ? Results.NotFound() : Results.Ok(juego); // Validacion en caso de que no exista el juego
            })
                .WithName("GetGame"); //---> WithName le agrega un nombre para poder usar el endpoint desde dentro del servidor



            // POST /games
            // El cliente accede a este endpoint con un objeto de tipo CreateGameDto.
            // El cuerpo de la funcion lambda lo convierte en un Dto interno con ID.
            // Y luego agrega el juego a la lista
            // Finalmente devuelve al cliente el codigo 201, con el id en forma de tipo anonimo (por convencion) y el objeto dentro de la lista
            group.MapPost("/", (CreateGameDto nuevoJuego) =>
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
            group.MapPut("/{id}", (int id, UpdateGameDto updateJuego) =>
            {
                var index = juegos.FindIndex(game => game.Id == id);

                if (index == -1) return Results.NotFound();  // --> Validacion (no esta el juego ID)


                juegos[index] = new GameDto(
                        id,
                        updateJuego.Nombre,
                        updateJuego.Genero,
                        updateJuego.Precio,
                        updateJuego.FechaLanzamiento
                    );

                return Results.NoContent(); //Por convencion se retorna NoContent
            });

            // DELETE /games/1
            group.MapDelete("/{id}", (int id) =>
            {
                var index = juegos.FindIndex(juego => juego.Id == id);

                juegos.RemoveAt(index);

                return Results.NoContent();
            });


            return group;
        }
    }
}
