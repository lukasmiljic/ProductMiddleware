namespace ProductMiddleware.Models
{
    public class User
    {
        int Id { get; set; }
        string FirstName { get; set; } = string.Empty;
        string LastName { get; set; } = string.Empty;
        string Email { get; set; } = string.Empty;
        string Password { get; set; } = string.Empty;
        string Role { get; set; } = "user";
    }
}