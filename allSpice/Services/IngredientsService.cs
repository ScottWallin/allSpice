namespace allSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;
  private readonly RecipesService _recipesService;
  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }
  internal Ingredient CreateIngredient(Ingredient ingredientsData)
  {
    Ingredient newIngredient = _repo.CreateIngredient(ingredientsData);
    return newIngredient;
  }
}