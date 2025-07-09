using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Entities;

namespace UserManagementAPI.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DbSet<FavouriteMealEntity> FavouriteMeals { get; set; }
        public DbSet<StudyProgramEntity> ProgramCatalog { get; set; }
    }
}
