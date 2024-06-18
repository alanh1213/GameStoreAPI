namespace GameStoreAPI.Aplicacion.Excepciones
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mensaje) : base(mensaje)
        {

        }
    }
}
