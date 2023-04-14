using Backend.Data;
using Backend.Models;
using Backend.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _context;

        public ClienteRepository(DataContext context)
        {
            _context = context;
        }

        public void create(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public void delete(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
        }
        public void update(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Cliente>> getListClientes()
        {
            var cliente = await _context.Clientes.ToListAsync();
            return cliente;
        }

        public async Task<Cliente> getClienteById(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(p => p.Id == id);

            return cliente;
        }
    }
}