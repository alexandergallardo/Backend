using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class OrdenCompraItem
    {
        public int Id { get; set; }
	    public string Descripcion { get; set; } = string.Empty;

        [ForeignKey("OrdenCompra")]
        public int OrdenCompraId { get; set; }

        [ForeignKey("OrdenCompraId")]
        public virtual OrdenCompra OrdenCompra { get; set; }

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto Producto { get; set; }
    }
}