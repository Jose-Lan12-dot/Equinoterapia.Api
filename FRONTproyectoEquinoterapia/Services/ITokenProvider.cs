namespace FRONTproyectoEquinoterapia.Services
{
        public interface ITokenProvider
        {
            Task<string?> GetTokenAsync();
        }
}
