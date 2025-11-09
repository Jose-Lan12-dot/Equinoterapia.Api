namespace Equinoterapia.Application.Interfaces
{
    public interface IEncryptionService
    {
        string EncryptPassword(string password);
    }
}