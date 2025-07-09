namespace UserManagementAPI.Contracts;

public class AddFavouriteMealRequest
{
    public string Name { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public bool Vegan { get; set; }
    public bool Vegetarian { get; set; }
}