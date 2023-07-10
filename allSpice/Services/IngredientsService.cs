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
  internal Ingredient GetByIngredientId(int ingredientId)
  {
    Ingredient ingredient = _repo.GetByIngredientId(ingredientId);
    if (ingredient == null) new Exception("Wrong Id");
    return ingredient;
  }
  internal List<Ingredient> GetIngredientByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }
  internal void DeleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetByIngredientId(ingredientId);
    Recipe recipe = _recipesService.GetRecipeById(ingredient.recipeId);
    if (userId != recipe.CreatorId) throw new Exception("You're missing the King's Jewel and the right to delete this. ");
    int rows = _repo.DeleteIngredient(ingredientId);
    if (rows > 1) throw new Exception("Whoops. It shouldn't blow up.");
  }
}