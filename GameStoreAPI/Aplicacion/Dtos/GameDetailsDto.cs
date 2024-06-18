namespace GameStoreAPI.Aplicacion.Dtos
{
    public record class GameDetailsDto(int Id,
        string Nombre,
        int GeneroId,
        decimal Precio,
        DateOnly FechaLanzamiento);

}
