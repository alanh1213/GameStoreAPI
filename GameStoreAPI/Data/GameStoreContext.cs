using GameStoreAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Data
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
            //En este constructor le paso la informacion necesaria para armar la sesion con la base de datos
        }

        //Los DbSet son los objetos/tablas que se van a utilizar para las querys
        public DbSet<Game> Juegos => Set<Game>();
        public DbSet<Genre> Generos => Set<Genre>();
    }
}
