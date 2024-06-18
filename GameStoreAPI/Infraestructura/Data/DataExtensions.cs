using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Infraestructura.Data
{
    public static class DataExtensions
    {
        //Metodo extendido de la clase WebApplication para migrar la base de datos al inicializar la aplicacion
        public static async Task MigrateDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
