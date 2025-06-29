using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Entities;

namespace UserManagementAPI.Services
{
    public class FavoritePlacesService
    {
        private readonly UserDbContext _context;

        public FavoritePlacesService(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddFavoriteAsync(Guid userId, int? placeId, string? name, double? lat, double? lon)
        {
            var favorite = new FavoritePlaceEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PlaceId = placeId,
                Name = name,
                Latitude = lat,
                Longitude = lon,
                CreatedAt = DateTime.UtcNow
            };

            _context.FavoritePlaces.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FavoritePlaceEntity>> GetFavoritesByUserAsync(Guid userId)
        {
            return await _context.FavoritePlaces
                .Include(f => f.Place)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
        
        public async Task<bool> DeleteFavoriteAsync(Guid userId, Guid favoriteId)
        {
            var favorite = await _context.FavoritePlaces
                .FirstOrDefaultAsync(f => f.Id == favoriteId && f.UserId == userId);

            if (favorite == null)
                return false;

            _context.FavoritePlaces.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}