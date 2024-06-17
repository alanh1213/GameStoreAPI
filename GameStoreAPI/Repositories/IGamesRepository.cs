using GameStoreAPI.Dtos;
using GameStoreAPI.Entidades;

namespace GameStoreAPI.Repositories
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