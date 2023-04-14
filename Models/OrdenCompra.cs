using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class OrdenCompra
    {
        public int Id { get; set; }
        public DateOnly FechaCompra { get; set; }
	    public decimal ValorCompra { get; set; }
	    public string MedioPago { get; set; } = string.Empty;

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

    }
}