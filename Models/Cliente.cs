namespace Backend.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
	    public string Celular { get; set; } = string.Empty;
	    public string Direccion { get; set; } = string.Empty;
    }
}