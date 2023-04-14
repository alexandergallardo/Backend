using Backend.Data;
using Backend.Models;
using Backend.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteRepository _repo;

        public ClientesController(ILogger<ClientesController> logger, IClienteRepository repo)
        {
            _logger = logger;
            _repo   = repo;
        }

        [HttpGet(Name = "getClientes")]  
        public async Task<IActionResult> getListClientes()
        {
            var clientes = await _repo.getListClientes();

            return Ok(clientes);
        }

        [HttpGet("{id}", Name = "getCliente")]  
        public async Task<IActionResult> getClienteById(int id)
        {
            var cliente = await _repo.getClienteById(id);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> create(Cliente cliente)
        {
            _repo.create(cliente);
            if(await _repo.SaveAll())
                return Ok(cliente);
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> update(Cliente cliente)
        {
            var clienteToUpdate = await _repo.getClienteById(cliente.Id);

            if(clienteToUpdate == null)
            {
                return BadRequest("Cliente no Existente");
            }

            clienteToUpdate.Celular   = cliente.Celular;
            clienteToUpdate.Direccion = cliente.Direccion;
            clienteToUpdate.Nombre    = cliente.Nombre;
            
            if(!await _repo.SaveAll())
                return NoContent();

            return Ok(clienteToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var cliente = await _repo.getClienteById(id);

            if (cliente == null)
            {
                return NotFound("Cliente no existente");
            }

            _repo.delete(cliente);
            
            if(!await _repo.SaveAll())
                return NoContent();
            return Ok("Cliente Borrado Satisfactoriamente");
        }   
    }

}