using UserManagementAPI.Contracts;
using UserManagementAPI.Entities;
using UserManagementAPI.Modells;
using UserManagementAPI.Repositories;

namespace UserManagementAPI.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly FavoritePlacesService _favoritePlacesService;

        public UsersService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider, IRefreshTokenRepository refreshTokenRepository, FavoritePlacesService favoritePlacesService)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _refreshTokenRepository = refreshTokenRepository;
            _favoritePlacesService = favoritePlacesService;
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

            var user = User.Create(Guid.NewGuid(), hashedPassword, email, username, false);

            await _userRepository.Add(user);
        }

        public async Task<LoginUserResponse> Login(string username, string password)
        {
            var user = await _userRepository.GetByUsername(username);
            if(user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }
            if (!_passwordHasher.Verify(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid password");
            }
            var accessToken = _jwtProvider.GenerateToken(user);

            var refreshToken = _jwtProvider.GenerateRefreshToken(user);
            await _refreshTokenRepository.Add(refreshToken);
            
            var meals = await _userRepository.GetFavouriteMeals(user.Id.ToString());
            
            var mealsList = meals.Select(m => new FavouriteMeal(m.Id, m.MealName, m.MealPrice, m.Vegan, m.Vegetarian)).ToList();
            
            var favoritePlaces = await _favoritePlacesService.GetFavoritesByUserAsync(user.Id);
            
            return new LoginUserResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token.ToString(),
                User = new UserDTO(user.Username, user.Email, user.IsConfirmed, mealsList, favoritePlaces.ToList())
            };
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
        
        public async Task<UserDTO> GetDTO(string userId)
        {
            var user = await _userRepository.GetByUserId(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var meals = await _userRepository.GetFavouriteMeals(user.Id.ToString());
            var mealsList = meals.Select(m => new FavouriteMeal(m.Id, m.MealName, m.MealPrice, m.Vegan, m.Vegetarian)).ToList();
            var favoritePlaces = await _favoritePlacesService.GetFavoritesByUserAsync(user.Id);
            var userDTO = new UserDTO(user.Username, user.Email,user.IsConfirmed, mealsList, favoritePlaces.ToList());
            return userDTO;
        }

        public async Task<int> AddFavouriteMeal(string userId, FavouriteMealDTO meal)
        {
            var user = await _userRepository.GetByUserId(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var favouriteMeal = new FavouriteMealEntity
            {
                UserId = user.Id,
                MealName = meal.MealName,
                MealPrice = meal.MealPrice,
                Vegan = meal.Vegan,
                Vegetarian = meal.Vegetarian
            };

            var id = await _userRepository.AddFavouriteMeal(favouriteMeal);

            return id;
        }
        
        public async Task RemoveFavouriteMeal(string userId, int Id)
        {
            var user = await _userRepository.GetByUserId(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userRepository.RemoveFavouriteMeal(user.Id.ToString(), Id);
        }
    }
}
