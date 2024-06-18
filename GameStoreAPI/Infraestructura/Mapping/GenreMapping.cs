using GameStoreAPI.Aplicacion.Dtos;
using GameStoreAPI.Dominio.Entidades;

namespace GameStoreAPI.Infraestructura.Mapping
{
    public static class GenreMapping
    {
        public static GeneroDto ToDto(this Genre genero)
        {
            return new GeneroDto(genero.Id, genero.Nombre);
        }
    }
}
