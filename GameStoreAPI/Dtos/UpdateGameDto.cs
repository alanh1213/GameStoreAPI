using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Dtos
{
    public record class UpdateGameDto(
        
        [Required][StringLength(50)] //-->Son los famosos data annotations
        string Nombre,
        [Required][StringLength(25)]
        string Genero,
        decimal Precio,
        DateOnly FechaLanzamiento);

    //Es un record identico a UpdateGameDto
    //Se crea otro record por convencion para cada operacion CRUD 
}
