using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repository.Interface
{
    public interface IOrdenCompraRepository
    {
        void create(OrdenCompra orden);
        void update(OrdenCompra orden);
        void delete(OrdenCompra orden);
        Task<bool> SaveAll();
        Task<IEnumerable<OrdenCompra>> getListOrdenCompras();
        Task<IEnumerable<OrdenCompra>> getListOrdenComprasByCliente(int clienteId);
        Task<OrdenCompra> getOrdenCompraById(int id);
    }
}