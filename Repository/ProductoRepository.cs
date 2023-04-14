using Backend.Data;
using Backend.Models;
using Backend.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DataContext _context;

        public ProductoRepository(DataContext context)
        {
            _context = context;
        }

        public void create(Producto producto)
        {
            _context.Productos.Add(producto);
        }

        public void delete(Producto producto)
        {
            _context.Productos.Remove(producto);
        }
        public void update(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Producto>> getListProductos()
        {
            var producto = await _context.Productos.ToListAsync();
            return producto;
        }

        public async Task<Producto> getProductoById(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            return producto;
        }
    }

}