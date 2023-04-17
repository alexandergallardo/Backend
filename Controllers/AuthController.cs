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
            usuarioDto.userName = usuarioDto.userName.ToLower();

            if(await _repo.ExisteUsuario(usuarioDto.userName))
                return BadRequest("El nombre de usuario no esta disponible.");

            var usuarioNuevo = new Usuario();

            usuarioNuevo.NombreUsuario = usuarioDto.userName;
            usuarioNuevo.Estado        = true;

            var usuarioCreado = await _repo.Registrar(usuarioNuevo, usuarioDto.passWord);

            var usuario = new UsuarioResponseDto();

            usuario.userId = usuarioCreado.Id;
            usuario.userName = usuarioCreado.NombreUsuario;

            return Ok(usuario);    
        } 

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var usuarioLogin = await _repo.Login(usuarioLoginDto.userName, usuarioLoginDto.passWord);

            if(usuarioLogin == null)
                return Ok(new {
                    status = "Error",
                    code    = "401",
                    message = "User Name o Password incorrecto",
                    data  = new {}
                });
            
            var usuarioDto = new UsuarioResponseDto();

            usuarioDto.userId = usuarioLogin.Id;
            usuarioDto.userName = usuarioLogin.NombreUsuario;

            var tokenNew = _tokenService.CreateToken(usuarioLogin);
            
            return Ok(new {
                status = "Ok",
                code = "200",
                message = "Autenticación satisfactoria",
                data  = new {
                    token = tokenNew,
                    usuario = usuarioDto
                }
             
            });
        }           
    }

}