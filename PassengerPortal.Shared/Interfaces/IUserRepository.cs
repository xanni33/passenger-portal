using PassengerPortal.Shared.Models;

namespace PassengerPortal.Shared.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
    }
}