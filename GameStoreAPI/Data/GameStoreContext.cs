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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Este metodo se llama siempre cuando se ejecuta una migracion

            modelBuilder.Entity<Genre>().HasData(
                new {Id = 1, Nombre = "Fighting" },
                new { Id = 2, Nombre = "Roleplaying" },
                new { Id = 3, Nombre = "Sports" },
                new { Id = 4, Nombre = "Racing" },
                new { Id = 5, Nombre = "Kids and Family" },
                new { Id = 6, Nombre = "Real Strategy Game" }
            );
        }
    }
}
