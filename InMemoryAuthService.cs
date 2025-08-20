namespace Teknorix.JobsApi.Infrastructure.Auth;

public interface IAuthService
{
    bool ValidateUser(string username, string password, out string role);
}

public class InMemoryAuthService : IAuthService
{
    private readonly Dictionary<string, (string Password, string Role)> _users = new()
    {
        { "admin", ("admin123", "Admin") },
        { "viewer", ("viewer123", "Viewer") }
    };

    public bool ValidateUser(string username, string password, out string role)
    {
        role = "Viewer";
        if (_users.TryGetValue(username, out var u) && u.Password == password)
        {
            role = u.Role;
            return true;
        }
        return false;
    }
}
