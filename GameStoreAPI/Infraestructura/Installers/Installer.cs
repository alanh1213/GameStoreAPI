using GameStoreAPI.Infraestructura.Middlewares;

namespace GameStoreAPI.Infraestructura.Installers
{
    public static class Installer
    {
        public static void AddMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
