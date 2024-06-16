using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Data
{
    public static class DataExtensions
    {
        //Metodo extendido de la clase WebApplication para migrar la base de datos al inicializar la aplicacion
        public static void MigrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
            dbContext.Database.Migrate();
        }
    }
}
