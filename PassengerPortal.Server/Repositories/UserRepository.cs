using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}

/*
using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Server.Repositories;

public class UserRepository
{
 
    
    public User GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }
    
    public User GetUserByUsername(string username)
    {
        using (var _context = new ApplicationDbContext()) 
        {
            return _context.User.FirstOrDefault(u => u.Username == username);
        }
    }
}
*/