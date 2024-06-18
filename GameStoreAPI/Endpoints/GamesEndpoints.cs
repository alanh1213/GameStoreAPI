using GameStoreAPI.Aplicacion.Dtos;
using GameStoreAPI.Dominio.Entidades;
using GameStoreAPI.Infraestructura.Data;
using GameStoreAPI.Infraestructura.Mapping;
using GameStoreAPI.Infraestructura.Repositories;
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
            

            // GET /juegos
            group.MapGet("/", async (IGamesRepository gamesRepository, GameStoreContext dbContext) =>
            {
                return Results.Ok(await gamesRepository.GetAllAsync());
            });

            // GET /juegos/1
            group.MapGet("/{id}", async (IGamesRepository gamesRepository, int id, GameStoreContext dbContext) =>
            {
                Game? juego = await gamesRepository.GetByIdAsync(id);

                return juego is null ? Results.NotFound() : Results.Ok(juego.ToGameDetailsDto()); // Validacion en caso de que no exista el juego
            })
                .WithName("GetGame"); //---> WithName le agrega un nombre para poder usar el endpoint desde dentro del servidor



            // POST /juegos
            // El cliente accede a este endpoint con un objeto de tipo CreateGameDto.
            // El cuerpo de la funcion lambda lo convierte en un Dto interno con ID.
            // Y luego agrega el juego a la lista
            // Finalmente devuelve al cliente el codigo 201, con el id en forma de tipo anonimo (por convencion) y el objeto dentro de la lista
            group.MapPost("/", async (IGamesRepository gamesRepository, CreateGameDto nuevoJuego, GameStoreContext dbContext) =>
            {
                Game juego = nuevoJuego.ToEntity();
                await gamesRepository.Post(juego);

                return Results.CreatedAtRoute("GetGame", new {id = juego.Id}, juego.ToGameDetailsDto());
            });


            // PUT /juegos/1
            group.MapPut("/{id}", async(IGamesRepository gamesRepository, int id, UpdateGameDto juegoModificado, GameStoreContext dbContext) =>
            {
                Game? juegoAModificar = await gamesRepository.GetByIdAsync(id);
                if (juegoAModificar is null) return Results.NotFound();  // --> Validacion (no esta el juego en la BD)

                await gamesRepository.Update(id, juegoAModificar, juegoModificado);
                return Results.NoContent(); //Por convencion se retorna NoContent
            });


            // DELETE /juegos/1
            group.MapDelete("/{id}", async (IGamesRepository gamesRepository, int id, GameStoreContext dbContext) =>
            {
                Game? juegoABorrar = await gamesRepository.GetByIdAsync(id);
                if (juegoABorrar is null) return Results.NotFound();  // --> Validacion (no esta el juego en la BD)

                await gamesRepository.Delete(id);
                return Results.NoContent();
            });


            return group;
        }
    }
}
