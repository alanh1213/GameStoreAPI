using GameStoreAPI.Dtos;
using GameStoreAPI.Entidades;

namespace GameStoreAPI.Mapping
{
    public static class GenreMapping
    {
        public static GeneroDto ToDto(this Genre genero)
        {
            return new GeneroDto(genero.Id, genero.Nombre);
        }
    }
}
