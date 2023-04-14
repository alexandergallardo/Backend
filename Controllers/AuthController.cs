using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Backend.Repository.Interface;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;  
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioRegisterDto usuarioDto)
        {
            usuarioDto.NombreUsuario = usuarioDto.NombreUsuario.ToLower();

            if(await _repo.ExisteUsuario(usuarioDto.NombreUsuario))
                return BadRequest("El nombre de usuario no esta disponible.");

            var usuarioNuevo = new Usuario();

            usuarioNuevo.NombreUsuario = usuarioDto.NombreUsuario;
            usuarioNuevo.Estado        = true;

            var usuarioCreado = await _repo.Registrar(usuarioNuevo, usuarioDto.Password);

            var usuario = new UsuarioResponseDto();

            usuario.Id = usuarioCreado.Id;
            usuario.NombreUsuario = usuarioCreado.NombreUsuario;

            return Ok(usuario);    
        } 

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var usuarioLogin = await _repo.Login(usuarioLoginDto.NombreUsuario, usuarioLoginDto.Password);

            if(usuarioLogin == null)
                return Unauthorized(new {
                    Message = "Unauthorized",
                    code    = 401
                });
            
            var usuarioDto = new UsuarioResponseDto();

            usuarioDto.Id = usuarioLogin.Id;
            usuarioDto.NombreUsuario = usuarioLogin.NombreUsuario;

            var tokenNew = _tokenService.CreateToken(usuarioLogin);
            
            return Ok(new {
                estado = true,
                token = tokenNew,
                usuario = usuarioDto
            });
        }           
    }

}