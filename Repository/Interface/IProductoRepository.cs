using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repository.Interface
{
    public interface IProductoRepository
    {
        void create(Producto producto);
        void update(Producto producto);
        void delete(Producto producto);
        Task<bool> SaveAll();
        Task<IEnumerable<Producto>> getListProductos();
        Task<Producto> getProductoById(int id);
    }
}