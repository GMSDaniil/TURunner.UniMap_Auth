namespace UserManagementAPI.Modells;

public class AddFavoriteRequest
{
    public int? PlaceId { get; set; }
    public string? Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
