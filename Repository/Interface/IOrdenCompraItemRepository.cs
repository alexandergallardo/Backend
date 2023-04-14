using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repository.Interface
{
    public interface IOrdenCompraItemRepository
    {
        void create(OrdenCompraItem item);
        void update(OrdenCompraItem item);
        void delete(OrdenCompraItem item);
        Task<bool> SaveAll();
        Task<IEnumerable<OrdenCompraItem>> getListOrdenComprasByOrden(int ordenId);
        Task<OrdenCompraItem> getOrdenCompraItemById(int id);
    }
}