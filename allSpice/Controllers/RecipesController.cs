namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth;

  public RecipesController(RecipesService recipesService, Auth0Provider auth, IngredientsService ingredientsService)
  {
    _recipesService = recipesService;
    _auth = auth;
    _ingredientsService = ingredientsService;
  }
  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipesData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      recipesData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.CreateRecipe(recipesData);
      return new ActionResult<Recipe>(Ok(recipe));
    }
    catch (Exception e)
    {
      return new ActionResult<Recipe>(BadRequest(e.Message));
    }
  }

  [HttpPut("{recipeId}")]
  [Authorize]
  public ActionResult<Recipe> UpdateRecipe(int recipeId, [FromBody] Recipe updateData)
  {
    try
    {
      updateData.Id = recipeId;
      Recipe recipe = _recipesService.UpdateRecipe(updateData);
      return Ok(recipe);
    }
    catch (Exception e)
    {

      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes()
  {
    try
    {
      List<Recipe> recipes = _recipesService.GetAllRecipes();
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<Recipe>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      // Recipe recipe =
      _recipesService.DeleteRecipe(recipeId, userInfo.Id);
      // string message = _recipesService.ArchiveRecipe(recipeId);
      return Ok("Buh Bye");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // SECTION Ingredients
  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientByRecipeId(recipeId);
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}