using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Dtos
{
    public record class CreateGameDto(

        [Required][StringLength(50)] //-->Son los famosos data annotations
        string Nombre, 
        [Required][StringLength(25)]
        string Genero,
        
        decimal Precio,

        DateOnly FechaLanzamiento);
}
