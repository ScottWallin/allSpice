namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0;
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0)
  {
    _ingredientsService = ingredientsService;
    _auth0 = auth0;

  }
  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{ingredientId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      _ingredientsService.DeleteIngredient(ingredientId, userInfo.Id)
      return Ok("Ingredient Removed");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}