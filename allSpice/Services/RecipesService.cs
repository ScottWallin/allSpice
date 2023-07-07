namespace allSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;
  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal void DeleteRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId) throw new Exception("You have no power here, Gandalf Greycode");
    int rows = _repo.DeleteRecipe(recipeId);
    if (rows > 1) new Exception("Mmm try again there bud");
    // recipe.Archived = !recipe.Archived;
    // _repo.UpdateRecipe(recipe);
    // return recipe;
    // int rows = _repo.ArchiveRecipe(recipeId);
    // if (rows > 1) new Exception("mmmm try again");
  }

  internal Recipe CreateRecipe(Recipe recipesData)
  {
    Recipe recipes = _repo.CreateRecipe(recipesData);
    return recipes;
  }
  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }
  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repo.GetRecipeById(recipeId);
    if (recipe == null) throw new Exception($"No recipe at id:{recipeId}. Make the recipe first you dingus.");
    return recipe;
  }

  internal Recipe UpdateRecipe(Recipe updateData)
  {
    Recipe original = GetRecipeById(updateData.Id);
    original.Title = updateData.Title != null ? updateData.Title : original.Title;
    original.Instructions = updateData.Instructions != null ? updateData.Instructions : original.Instructions;
    original.Img = updateData.Img != null ? updateData.Img : original.Img;
    original.Category = updateData.Category != null ? updateData.Category : original.Category;

    _repo.UpdateRecipe(original);
    return original;
  }
}