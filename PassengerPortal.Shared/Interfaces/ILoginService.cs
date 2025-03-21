
namespace PassengerPortal.Shared.Interfaces
{
    public interface ILoginService
    {
        bool Authenticate(string username, string password);
    }
}
