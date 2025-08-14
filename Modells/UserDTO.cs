using UserManagementAPI.Entities;

namespace UserManagementAPI.Modells;

public class UserDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public List<FavouriteMeal> FavouriteMeals { get; set; }
    public List<FavoritePlaceEntity> FavouritePlaces { get; set; }
    
    public UserDTO(string username, string email, List<FavouriteMeal> favouriteMeals, List<FavoritePlaceEntity> favouritePlaces)
    {
        Username = username;
        Email = email;
        FavouriteMeals = favouriteMeals;
        FavouritePlaces = favouritePlaces;
    }
}