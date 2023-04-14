using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}