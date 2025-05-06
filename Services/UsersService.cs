using UserManagementAPI.Modells;
using UserManagementAPI.Repositories;

namespace UserManagementAPI.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string email, string password, string username)
        {
            var existingUser = await _userRepository.GetByUsername(username);
            if (existingUser != null)
            {
                throw new Exception("Username is already used");
            }
            existingUser = await _userRepository.GetByEmail(email);
            if (existingUser != null)
            {
                throw new Exception("Email is already used");
            }
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), hashedPassword, email, username);

            await _userRepository.Add(user);
        }

        public async Task<String> Login(string username, string password)
        {
            var user = await _userRepository.GetByUsername(username);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            if (!_passwordHasher.Verify(password, user.PasswordHash))
            {
                throw new Exception("Invalid password");
            }
            var token = _jwtProvider.GenerateToken(user);
            return token;
        }

        public async Task<User> Get(string userId)
        {
            var user = await _userRepository.GetByUserId(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
    }
}
