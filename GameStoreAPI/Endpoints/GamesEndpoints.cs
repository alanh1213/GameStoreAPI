using GameStoreAPI.Data;
using GameStoreAPI.Dtos;
using GameStoreAPI.Entidades;
using GameStoreAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Endpoints
{
    public static class GamesEndpoints
    {
        //*******************************************************************************************
        // Se extiende la clase WebApplication con los metodos CRUD, de forma separada del Program.cs
        //*******************************************************************************************

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("juegos") //el group reemplaza al app en los endpoints y ya no hace falta
                                               //hardcodear "juegos" en cada uno de ellos.
                           .WithParameterValidation();  //---> Este filtro fuerza que se validen los anticuados Data Annotations
                                                        //Se puede poner en cada endpoint pero el group los agrega automaticamente


            // GET /games
            group.MapGet("/", async (GameStoreContext dbContext) =>
            {
                return await dbContext.Juegos.Include(juego => juego.Genero) //--> Agrega la informacion de la tabla vinculada
                                .Select(juego => juego.ToGameSummaryDto())
                                .AsNoTracking() //--> Esta ultima tarea optimiza la query ya que no vamos a operar con las entidades
                                .ToListAsync();
            });

            // GET /games/1
            group.MapGet("/{id}", async(int id, GameStoreContext dbContext) =>
            {
                Game? juego = await dbContext.Juegos.FindAsync(id);

                return juego is null ? Results.NotFound() : Results.Ok(juego.ToGameDetailsDto()); // Validacion en caso de que no exista el juego
            })
                .WithName("GetGame"); //---> WithName le agrega un nombre para poder usar el endpoint desde dentro del servidor



            // POST /games
            // El cliente accede a este endpoint con un objeto de tipo CreateGameDto.
            // El cuerpo de la funcion lambda lo convierte en un Dto interno con ID.
            // Y luego agrega el juego a la lista
            // Finalmente devuelve al cliente el codigo 201, con el id en forma de tipo anonimo (por convencion) y el objeto dentro de la lista
            group.MapPost("/", async(CreateGameDto nuevoJuego, GameStoreContext dbContext) =>
            {
                Game juego = nuevoJuego.ToEntity();

                dbContext.Juegos.Add(juego);
                await dbContext.SaveChangesAsync(); //-> El await esta aqui por que AddAsync no es un metodo que toque la BD

                GameDetailsDto juegoDto = juego.ToGameDetailsDto();
                return Results.CreatedAtRoute("GetGame", new { id = juego.Id}, juegoDto);
            });


            // PUT /games/1
            group.MapPut("/{id}", async(int id, UpdateGameDto updateJuego, GameStoreContext dbContext) =>
            {
                var juegoExistente = await dbContext.Juegos.FindAsync(id);
                if (juegoExistente is null) return Results.NotFound();  // --> Validacion (no esta el juego ID)

                dbContext.Entry(juegoExistente).CurrentValues.SetValues(updateJuego.ToEntity(id));
                await dbContext.SaveChangesAsync();

                return Results.NoContent(); //Por convencion se retorna NoContent
            });


            // DELETE /games/1
            group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Juegos.Where(juego => juego.Id == id)
                                .ExecuteDeleteAsync(); //--> Forma eficiente de borrar una registro

                return Results.NoContent();
            });


            return group;
        }
    }
}
