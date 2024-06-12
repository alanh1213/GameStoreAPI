namespace GameStoreAPI.Dtos
{
    public record class GameDto(int Id, 
        string Nombre, 
        string Genero, 
        decimal Precio, 
        DateOnly FechaLanzamiento);
}
