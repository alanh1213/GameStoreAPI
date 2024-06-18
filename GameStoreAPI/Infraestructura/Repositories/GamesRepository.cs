using GameStoreAPI.Aplicacion.Dtos;
using GameStoreAPI.Dominio.Entidades;
using GameStoreAPI.Infraestructura.Data;
using GameStoreAPI.Infraestructura.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Infraestructura.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private readonly GameStoreContext dbContext;
        public GamesRepository(GameStoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GameSummaryDto>> GetAllAsync()
        {
            return await dbContext.Juegos.Include(juego => juego.Genero) //--> Agrega la informacion de la tabla vinculada
                                .Select(juego => juego.ToGameSummaryDto())
                                .AsNoTracking() //--> Esta ultima tarea optimiza la query ya que no vamos a operar con las entidades
                                .ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            Game? juego = await dbContext.Juegos.FindAsync(id);
            return juego;
        }

        public async Task Post(Game juego)
        {
            dbContext.Juegos.Add(juego);
            await dbContext.SaveChangesAsync(); //-> El await esta aqui por que Add no es un metodo que toque la BD
        }

        public async Task Update(int id, Game juegoAModificar, UpdateGameDto juegoModificado)
        {
            dbContext.Entry(juegoAModificar).CurrentValues.SetValues(juegoModificado.ToEntity(id));
            await dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var resultado = await dbContext.Juegos.Where(juegoABorrar => juegoABorrar.Id == id)
                                .ExecuteDeleteAsync();
            return resultado;
        }
    }
}
