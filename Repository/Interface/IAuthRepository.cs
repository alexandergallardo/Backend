using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<Usuario> Registrar(Usuario usuario, string password);
        Task<Usuario> Login(string usuario, string password); 

        Task<bool> ExisteUsuario(string usuario);
    }
}