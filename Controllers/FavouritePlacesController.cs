using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Modells;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritePlacesController : ControllerBase
{
    private readonly FavoritePlacesService _favoritePlacesService;

    public FavoritePlacesController(FavoritePlacesService favoritePlacesService)
    {
        _favoritePlacesService = favoritePlacesService;
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteRequest request)
    {
        var userIdStr = User.FindFirst("userId")?.Value;
        if (userIdStr == null) return Unauthorized();
        var userId = Guid.Parse(userIdStr);

        await _favoritePlacesService.AddFavoriteAsync(userId, request.PlaceId, request.Name, request.Latitude, request.Longitude);
        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        var userIdStr = User.FindFirst("userId")?.Value;
        if (userIdStr == null) return Unauthorized();
        var userId = Guid.Parse(userIdStr);

        var favorites = await _favoritePlacesService.GetFavoritesByUserAsync(userId);
        return Ok(favorites);
    }
    
    [Authorize]
    [HttpDelete("{favoriteId}")]
    public async Task<IActionResult> DeleteFavorite(Guid favoriteId)
    {
        var userIdStr = User.FindFirst("userId")?.Value;
        if (userIdStr == null) return Unauthorized();
        var userId = Guid.Parse(userIdStr);

        var success = await _favoritePlacesService.DeleteFavoriteAsync(userId, favoriteId);
        if (!success)
        {
            return NotFound("Favorite place not found or you don't have access.");
        }

        return NoContent(); // 204 — успішно видалено
    }
}