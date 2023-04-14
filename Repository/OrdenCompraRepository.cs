using Backend.Data;
using Backend.Models;
using Backend.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class OrdenCompraRepository : IOrdenCompraRepository
    {
        private readonly DataContext _context;

        public OrdenCompraRepository(DataContext context)
        {
            _context = context;
        }

        public void create(OrdenCompra orden)
        {
            _context.OrdenCompras.Add(orden);
        }

        public void delete(OrdenCompra orden)
        {
            _context.OrdenCompras.Remove(orden);
        }
        public void update(OrdenCompra orden)
        {
            _context.Entry(orden).State = EntityState.Modified;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<OrdenCompra>> getListOrdenCompras()
        {
            var ordenes = await _context.OrdenCompras.ToListAsync();
            return ordenes;
        }
        public async Task<IEnumerable<OrdenCompra>> getListOrdenComprasByCliente(int clienteId)
        {
           var ordenes = await _context.OrdenCompras.ToListAsync();

            return ordenes;
        }
        public async Task<OrdenCompra> getOrdenCompraById(int id)
        {
            var orden = await _context.OrdenCompras.FirstOrDefaultAsync(p => p.Id == id);

            return orden;
        }
    }

}