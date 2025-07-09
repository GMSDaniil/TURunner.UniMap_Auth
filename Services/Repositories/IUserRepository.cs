﻿using UserManagementAPI.Entities;
using UserManagementAPI.Modells;

namespace UserManagementAPI.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUsername(string email);
        Task<User?> GetByUserId(string email);
        Task<List<FavouriteMealEntity>> GetFavouriteMeals(string userId);
        Task<int> AddFavouriteMeal(FavouriteMealEntity meal);
        Task RemoveFavouriteMeal(String userId, int Id);
    }
}