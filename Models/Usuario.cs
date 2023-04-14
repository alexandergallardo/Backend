namespace Backend.Models
{
    public class Usuario
    {
        public int Id { get; set; }
	    public string NombreUsuario { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
