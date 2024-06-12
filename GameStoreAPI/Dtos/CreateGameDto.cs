namespace GameStoreAPI.Dtos
{
    public record class CreateGameDto(string Nombre,
        string Genero,
        decimal Precio,
        DateOnly FechaLanzamiento);
}
