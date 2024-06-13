namespace GameStoreAPI.Dtos
{
    public record class UpdateGameDto(string Nombre,
        string Genero,
        decimal Precio,
        DateOnly FechaLanzamiento);

    //Es un record identico a CreateGameDto
    //Se crea otro record por convencion para cada operacion CRUD 
}
