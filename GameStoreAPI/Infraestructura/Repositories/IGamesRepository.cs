using GameStoreAPI.Aplicacion.Dtos;
using GameStoreAPI.Dominio.Entidades;

namespace GameStoreAPI.Infraestructura.Repositories
{
    public interface IGamesRepository
    {
        Task<int> Delete(int id);
        Task<IEnumerable<GameSummaryDto>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task Post(Game juego);
        Task Update(int id, Game juegoAModificar, UpdateGameDto juegoModificado);
    }
}