namespace Backend.Dtos
{
    public class OrdenCompraRegisterDto
    {
	    public DateOnly FechaCompra { get; set; }
	    public decimal ValorCompra { get; set; }
	    public string MedioPago { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public int Id { get; set; }
    }
}