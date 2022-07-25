using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    #region Fields :
    private static readonly List<User> _users = new();
    #endregion

    #region CTORS :
    public UserRepository()
    {

    }
    #endregion

    #region Operations :
    public void AddUser(User user)
    {
        _users.Add(user);
    }
    public User GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(x => x.Email == email);
    }
    #endregion
}
