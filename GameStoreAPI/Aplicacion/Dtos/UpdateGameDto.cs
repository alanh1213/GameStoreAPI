using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Aplicacion.Dtos
{
    public record class UpdateGameDto(

        [Required][StringLength(50)] //-->Son los famosos data annotations
        string Nombre,
        int GeneroId,
        decimal Precio,
        DateOnly FechaLanzamiento);

    //Es un record identico a UpdateGameDto
    //Se crea otro record por convencion para cada operacion CRUD 
}
