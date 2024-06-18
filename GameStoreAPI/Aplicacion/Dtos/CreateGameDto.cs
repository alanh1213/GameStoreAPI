using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Aplicacion.Dtos
{
    public record class CreateGameDto(

        [Required][StringLength(50)] //-->Son los famosos data annotations
        string Nombre,
        [Required]
        int GeneroId,

        decimal Precio,

        DateOnly FechaLanzamiento);
}
