namespace UserManagementAPI.Modells;

public class UserDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public List<FavouriteMealDTO> FavouriteMeals { get; set; }
    public UserDTO(string username, string email, List<FavouriteMealDTO> favouriteMeals)
    {
        Username = username;
        Email = email;
        FavouriteMeals = favouriteMeals;
    }
}