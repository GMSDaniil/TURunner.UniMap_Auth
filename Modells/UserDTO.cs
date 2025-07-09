namespace UserManagementAPI.Modells;

public class UserDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public List<FavouriteMeal> FavouriteMeals { get; set; }
    public UserDTO(string username, string email, List<FavouriteMeal> favouriteMeals)
    {
        Username = username;
        Email = email;
        FavouriteMeals = favouriteMeals;
    }
}