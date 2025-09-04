using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Entities;
using UserManagementAPI.Modells;

namespace UserManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            var userEntity = new UserEntity(user.Id, user.PasswordHash, user.Email, user.Username);

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return User.Create(user.Id, user.PasswordHash, user.Email, user.Username, user.isConfirmed);
        }

        public async Task<User?> GetByUsername(string username)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return null;
            }
            return User.Create(user.Id, user.PasswordHash, user.Email, user.Username, user.isConfirmed);
        }

        public async Task<User?> GetByUserId(string userId)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return null;
            }
            return User.Create(user.Id, user.PasswordHash, user.Email, user.Username, user.isConfirmed);
        }

        public async Task<List<FavouriteMealEntity>> GetFavouriteMeals(string userId)
        {
            var meals = await _context.FavouriteMeals
                .Where(m => m.UserId.ToString() == userId)
                .AsNoTracking()
                .ToListAsync();

            return meals;
        }
        
        public async Task<int> AddFavouriteMeal(FavouriteMealEntity meal)
        {
            await _context.FavouriteMeals.AddAsync(meal);
            await _context.SaveChangesAsync();

            return meal.Id;
        }
        
        public async Task RemoveFavouriteMeal(String userId, int Id)
        {
            var meal = await _context.FavouriteMeals
                .FirstOrDefaultAsync(m => m.UserId.ToString() == userId && m.Id == Id);
            if (meal != null)
            {
                _context.FavouriteMeals.Remove(meal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ConfirmUser(string userId)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user != null)
            {
                user.isConfirmed = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePassword(string userId, string newPasswordHash)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user != null)
            {
                user.PasswordHash = newPasswordHash;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            
            var favoriteMeals = _context.FavouriteMeals.Where(m => m.UserId.ToString() == userId);
            _context.FavouriteMeals.RemoveRange(favoriteMeals);
            await _context.SaveChangesAsync();
            
            var favoritePlaces = _context.FavoritePlaces.Where(fp => fp.UserId.ToString() == userId);
            _context.FavoritePlaces.RemoveRange(favoritePlaces);
            await _context.SaveChangesAsync();
        }

    }
}
