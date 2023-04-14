namespace Backend.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public DateOnly FechaIngreso { get; set; }
        public int Cantidad { get; set; }
	    public decimal Valor { get; set; }
	    public string Descripcion { get; set; } = string.Empty;        
    }
}


