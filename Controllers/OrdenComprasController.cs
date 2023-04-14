using Backend.Data;
using Backend.Dtos;
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
    public class OrdenComprasController : ControllerBase
    {
        private readonly ILogger<OrdenComprasController> _logger;
        private readonly IOrdenCompraRepository _repo;
        public OrdenComprasController(ILogger<OrdenComprasController> logger, IOrdenCompraRepository repo)
        {
            _logger  = logger;
            _repo    = repo;
        }

        [HttpGet(Name = "getOrdenCompras")]  
        public async Task<IActionResult> getListOrdenCompras()
        {
            var ordenes = await _repo.getListOrdenCompras();

            return Ok(ordenes);
        }

        [HttpGet("{id}", Name = "getOrdenCompra")]  
        public async Task<IActionResult> getOrdenCompraById(int id)
        {
            var orden = await _repo.getOrdenCompraById(id);

            if (orden == null)
            {
                return NotFound("Orden de compra no existente");
            }
            
            return Ok(orden);
        }

        [HttpPost]
        public async Task<IActionResult> create(OrdenCompraRegisterDto orden)
        {
            var ordenNueva = new OrdenCompra();

            ordenNueva.ClienteId   = orden.ClienteId;
            ordenNueva.FechaCompra = orden.FechaCompra;
            ordenNueva.MedioPago   = orden.MedioPago;
            ordenNueva.ValorCompra = orden.ValorCompra;

            _repo.create(ordenNueva);

            if(await _repo.SaveAll())
            {
                orden.Id = ordenNueva.Id;
                return Ok(orden);
            }
                
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> update(OrdenCompraRegisterDto orden)
        {
            var ordenToUpdate = await _repo.getOrdenCompraById(orden.Id);

            if(ordenToUpdate == null)
            {
                return BadRequest();
            }

            ordenToUpdate.ClienteId = orden.ClienteId;
            ordenToUpdate.FechaCompra = orden.FechaCompra;
            ordenToUpdate.MedioPago  = orden.MedioPago;
            ordenToUpdate.ValorCompra = orden.ValorCompra;
            
            if(!await _repo.SaveAll())
                return NoContent();

            return Ok(orden);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var orden = await _repo.getOrdenCompraById(id);

            if (orden == null)
            {
                return NotFound("Orden de Compra no Existente");
            }

            _repo.delete(orden);
            
            if(!await _repo.SaveAll())
                return NoContent();

            return Ok("Orden de Compra Borrada Satisfactoriamente");
        }
    }

}