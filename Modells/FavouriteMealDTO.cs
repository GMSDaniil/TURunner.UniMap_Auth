namespace UserManagementAPI.Modells;

public class FavouriteMealDTO
{
    public string MealName { get; set; } = string.Empty;
    public string MealPrice { get; set; } = string.Empty;
    public bool Vegan { get; set; }
    public bool Vegetarian { get; set; }
    
    public FavouriteMealDTO(string mealName, string mealPrice, bool vegan, bool vegetarian)
    {
        MealName = mealName;
        MealPrice = mealPrice;
        Vegan = vegan;
        Vegetarian = vegetarian;
    }
}