namespace PassengerPortal.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } // Login u¿ytkownika
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Zahaszowane has³o
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}