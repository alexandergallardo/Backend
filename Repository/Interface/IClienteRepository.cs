using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repository.Interface
{
    public interface IClienteRepository
    {
        void create(Cliente cliente);
        void update(Cliente cliente);
        void delete(Cliente cliente);
        Task<bool> SaveAll();
        Task<IEnumerable<Cliente>> getListClientes();
        Task<Cliente> getClienteById(int id);
    }
}