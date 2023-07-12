namespace allSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;
  private readonly RecipesService _recipesService;
  public FavoritesService(FavoritesRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }
  internal Favorite CreateFavorite(Favorite favoritesData)
  {
    Favorite newFavorite = _repo.CreateFavorite(favoritesData);
    return newFavorite;
  }
}