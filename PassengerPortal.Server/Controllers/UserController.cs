using Microsoft.AspNetCore.Mvc;
using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ApplicationDbContext _context;

        public UsersController(ILoginService loginService, ApplicationDbContext context)
        {
            _loginService = loginService;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_loginService.Authenticate(request.Username, request.Password))
            {
                return Ok(new { Message = "Login successful!" });
            }

            return Unauthorized(new { Message = "Invalid username or password." });
        }
        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return Ok();
        }
        
        [HttpGet("isLoggedIn")]
        public IActionResult IsLoggedIn()
        {
            return Ok(new { IsLoggedIn = HttpContext.Session.GetString("Username") != null });
        }
        

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // Sprawdzenie, czy login lub email już istnieje
            if (_context.Users.Any(u => u.Username == request.Username || u.Email == request.Email))
            {
                return BadRequest(new { Message = "Username or Email is already taken." });
            }

            // Haszowanie hasła
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Tworzenie nowego użytkownika
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            // Zapisanie użytkownika w bazie danych
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { Message = "User registered successfully." });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
