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
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly IProductoRepository _repo;

        public ProductosController(ILogger<ProductosController> logger, IProductoRepository repo)
        {
            _logger = logger;
            _repo   = repo;
        }

        [HttpGet(Name = "getProductos")]  
        public async Task<IActionResult> getListProductos()
        {
            var productos = await _repo.getListProductos();

            return Ok(productos);
        }

        [HttpGet("{id}", Name = "getProducto")]  
        public async Task<IActionResult> getProductoById(int id)
        {
            var producto = await _repo.getProductoById(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> create(Producto producto)
        {
            _repo.create(producto);
            if(await _repo.SaveAll())
                return Ok(producto);
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> update(Producto producto)
        {
            var productoToUpdate = await _repo.getProductoById(producto.Id);

            if(productoToUpdate == null)
            {
                return BadRequest();
            }

            productoToUpdate.Cantidad    = producto.Cantidad;
            productoToUpdate.Descripcion = producto.Descripcion;
            productoToUpdate.Valor       = producto.Valor;
            
            if(!await _repo.SaveAll())
                return NoContent();
            return Ok(productoToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var producto = await _repo.getProductoById(id);

            if (producto == null)
            {
                return NotFound("Producto No Existente");
            }

            _repo.delete(producto);
            
            if(!await _repo.SaveAll())
                return NoContent();
            return Ok("Producto Borrado Satisfactoriamente");
        }
    }

}