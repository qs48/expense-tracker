namespace UserService.Models
{
    public class User
    {
        public int Id { get; set; }                  // Primary key
        public string Username { get; set; } = "";   // User's name
        public string Email { get; set; } = "";      // User's email
        public string PasswordHash { get; set; } = "";// Hashed password
    }
}
