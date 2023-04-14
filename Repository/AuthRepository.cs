using Backend.Data;
using Backend.Models;
using Backend.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteUsuario(string usuario)
        {
            if(await _context.Usuarios.AnyAsync(u => u.NombreUsuario == usuario))
                return true;
            return false;
        }

        public async Task<Usuario> Login(string nombre, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombre);

            if(usuario == null) return null;

            if(!VerificarPassword(password, usuario.PasswordHash, usuario.PasswordSalt))
            {
                return null;
            }

            return usuario;
        }

        public async Task<Usuario> Registrar(Usuario usuario, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte [] passwordSalt)
        {
            using( var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificarPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using( var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computeHash.Length; i++)
                {
                    if(computeHash[i] != passwordHash[i]) return false;
                }
            }  

            return true;
        }
    }
}
