using ShopFlexByte.Domain.Enums;

namespace ShopFlexByte.Domain.Entities;

// B1: Aplica corretamente modificadores de acesso (private no campo, get-only/private set nas propriedades),
//     construtor que inicializa o estado e métodos públicos coesos (AddRole/RemoveRole).
// D1: Encapsula a coleção de papéis — o campo _roles é privado e só é exposto como IReadOnlyCollection.
public sealed class User
{
    private readonly List<UserRole> _roles = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; }
    public string Email { get; }
    public string FullName { get; }
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    public User(string username, string email, string fullName, IEnumerable<UserRole> roles)
    {
        Username = username;
        Email = email;
        FullName = fullName;
        _roles.AddRange(roles);
    }

    public void AddRole(UserRole role)
    {
        if (!_roles.Contains(role))
        {
            _roles.Add(role);
        }
    }

    public void RemoveRole(UserRole role)
    {
        _roles.Remove(role);
    }
}
