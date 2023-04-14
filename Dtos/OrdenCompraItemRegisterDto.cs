namespace Backend.Dtos
{
    public class OrdenCompraItemRegisterDto
    {
        public int Id { get; set; }
        public int OrdenCompraId { get; set; }
        public int ProductoId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}