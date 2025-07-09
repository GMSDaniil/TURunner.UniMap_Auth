namespace UserManagementAPI.Modells;

public class FavouriteMeal
{
    public int Id { get; set; }
    public string MealName { get; set; } = string.Empty;
    public string MealPrice { get; set; } = string.Empty;
    public bool Vegan { get; set; }
    public bool Vegetarian { get; set; }
    
    public FavouriteMeal(int id, string mealName, string mealPrice, bool vegan, bool vegetarian)
    {
        Id = id;
        MealName = mealName;
        MealPrice = mealPrice;
        Vegan = vegan;
        Vegetarian = vegetarian;
    }
}