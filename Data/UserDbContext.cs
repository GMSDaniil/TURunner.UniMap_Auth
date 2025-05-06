using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Entities;

namespace UserManagementAPI.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
    }
}
