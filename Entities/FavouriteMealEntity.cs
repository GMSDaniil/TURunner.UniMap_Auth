namespace UserManagementAPI.Entities;

public class FavouriteMealEntity
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string MealName { get; set; } = string.Empty;
    public string MealPrice { get; set; } = string.Empty;
    public bool Vegan { get; set; }
    public bool Vegetarian { get; set; }
}