namespace GameStoreAPI.Dominio.Entidades
{
    public class Game
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public decimal Precio { get; set; }
        public DateOnly FechaLanzamiento { get; set; }

        //**********************************************
        //Las relaciones con las otras tablas van debajo
        //**********************************************
        public int GeneroId { get; set; }
        public Genre? Genero { get; set; }
    }
}
