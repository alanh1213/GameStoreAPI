using GameStoreAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext>options) 
        : DbContext(options) //---> Todo este choclo es para tener todas las opciones para configurar la sesion
                            //con la base de datos
    {


        //Los DbSet son los objetos que se van a utilizar en las querys
        public DbSet<Game> Juegos => Set<Game>();
        public DbSet<Genre> Generos => Set<Genre>();
    }
}
