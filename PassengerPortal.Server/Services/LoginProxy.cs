using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Server.Repositories;
using BCrypt.Net;

namespace PassengerPortal.Server.Services
{
    public class LoginProxy : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginProxy(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Authenticate(string username, string password)
        {
            // Pobierz użytkownika z bazy danych
            var user = _userRepository.GetUserByUsername(username);

            if (user == null)
                return false;

            // Porównanie zahaszowanego hasła (np. BCrypt)
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}