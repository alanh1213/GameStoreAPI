using GameStoreAPI.Dominio.Entidades;
using GameStoreAPI.Infraestructura.Data;
using GameStoreAPI.Infraestructura.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Endpoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("generos");


            // GET /generos
            group.MapGet("/", async (GameStoreContext dbContext) =>
            {
                return await dbContext.Generos.Select(genero => genero.ToDto())
                                .AsNoTracking() //--> Esta ultima tarea optimiza la query ya que no vamos a operar con las entidades
                                .ToListAsync();
            });

            // GET /generos/1
            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                Genre? genero = await dbContext.Generos.FindAsync(id);

                return genero is null ? Results.NotFound() : Results.Ok(genero.ToDto()); // Validacion en caso de que no exista el juego
            });

            return group;
        }
    }
}
