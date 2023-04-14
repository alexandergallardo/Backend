using Backend.Data;
using Backend.Models;
using Backend.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class OrdenCompraItemRepository : IOrdenCompraItemRepository
    {
        private readonly DataContext _context;

        public OrdenCompraItemRepository(DataContext context)
        {
            _context = context;
        }

        public void create(OrdenCompraItem item)
        {
            _context.OrdenCompraItems.Add(item);
        }

        public void delete(OrdenCompraItem item)
        {
            _context.OrdenCompraItems.Remove(item);
        }
        public void update(OrdenCompraItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<OrdenCompraItem>> getListOrdenComprasByOrden(int ordenId)
        {
           var items = await _context.OrdenCompraItems.ToListAsync();

            return items;
        }
        public async Task<OrdenCompraItem> getOrdenCompraItemById(int id)
        {
            var item = await _context.OrdenCompraItems.FirstOrDefaultAsync(i => i.Id == id);

            return item;
        }
    }

}