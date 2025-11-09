using Equinoterapia.Application.Interfaces;
using Equinoterapia.Infrastructure.Helpers; // El using a tu clase movida

namespace Equinoterapia.Infrastructure.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string EncryptPassword(string password)
        {
            return Encrypt.EncryptStringSspcWeb(password);
        }
    }
}