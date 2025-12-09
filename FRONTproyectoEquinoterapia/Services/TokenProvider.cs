using Blazored.LocalStorage;

namespace FRONTproyectoEquinoterapia.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ILocalStorageService _storage; // <-- Aquí

        public TokenProvider(ILocalStorageService storage)
        {
            _storage = storage;
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _storage.GetItemAsync<string>("jwtToken");
        }
    }
}

