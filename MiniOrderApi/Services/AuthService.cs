using AutoMapper;
using MiniOrderApi.Data;
using MiniOrderApi.DTOs.Auth;
using MiniOrderApi.Entities;
using MiniOrderApi.Services.Interfaces;
using System.Security.Cryptography; 
using System.Text;

namespace MiniOrderApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService; 

        public AuthService(AppDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public string Register(RegisterRequest request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
                throw new Exception("This username is already taken.");

            var passwordHash = ComputeHash(request.Password);

            var newUser = _mapper.Map<User>(request);
            newUser.PasswordHash = passwordHash;
            newUser.Role = "Client"; 

            _context.Users.Add(newUser);
            _context.SaveChanges(); 

            var newCustomer = new Customer
            {
                UserId = newUser.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            return "User and Customer successfully created.";
        }

        public string Login(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null || user.PasswordHash != ComputeHash(request.Password))
                throw new Exception("Incorrect username or password!");

            return _tokenService.CreateToken(user);
        }

        private string ComputeHash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}