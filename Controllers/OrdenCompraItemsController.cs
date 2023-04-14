using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Backend.Repository.Interface;
using Backend.Dtos;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenCompraItemsController : ControllerBase
    {
        private readonly ILogger<OrdenCompraItemsController> _logger;
        private readonly IOrdenCompraItemRepository _repo;
        public OrdenCompraItemsController(ILogger<OrdenCompraItemsController> logger, IOrdenCompraItemRepository repo)
        {
            _logger  = logger;
            _repo    = repo;
        }

        [HttpGet(Name = "getListOrdenCompras")]  
        public async Task<IActionResult> getListOrdenCompras(int ordenId)
        {
            var items = await _repo.getListOrdenComprasByOrden(ordenId);

            return Ok(items);
        }

        [HttpGet("{id}", Name = "getOrdenCompraItemById")]  
        public async Task<IActionResult> getOrdenCompraItemById(int id)
        {
            var item = await _repo.getOrdenCompraItemById(id);

            if (item == null)
            {
                return NotFound("Item no existente");
            }
            
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> create(OrdenCompraItemRegisterDto item)
        {
            var itemNew = new OrdenCompraItem();

            itemNew.Descripcion     = item.Descripcion;
            itemNew.ProductoId      = item.ProductoId;
            itemNew.OrdenCompraId   = item.OrdenCompraId;

            _repo.create(itemNew);

            if(await _repo.SaveAll())
            {
                item.Id = itemNew.Id;
                return Ok(item);
            }
                
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> update(OrdenCompraItemRegisterDto item)
        {
            var itemToUpdate = await _repo.getOrdenCompraItemById(item.Id);

            if(itemToUpdate == null)
            {
                return BadRequest();
            }

            itemToUpdate.Descripcion     = item.Descripcion;
            itemToUpdate.ProductoId      = item.ProductoId;
            itemToUpdate.OrdenCompraId   = item.OrdenCompraId;
            
            if(!await _repo.SaveAll())
                return NoContent();

            return Ok(item);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var item = await _repo.getOrdenCompraItemById(id);

            if (item == null)
            {
                return NotFound("Item no Existente");
            }

            _repo.delete(item);
            
            if(!await _repo.SaveAll())
                return NoContent();

            return Ok("Item Borrado Satisfactoriamente");
        }
    }

}