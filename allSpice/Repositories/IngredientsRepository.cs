namespace allSpice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;
  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }
  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients
    (name, quantity, recipeId)
    VALUES
    (@name, @quantity, @recipeId);
    
    SELECT
    *
    FROM ingredients
    WHERE id = LAST_INSERT_ID();";
    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
    return ingredient;
  }
  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT 
    ing.*
    FROM ingredients ing
    WHERE ing.recipeId = @recipeId;
    ";
    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }
  // {
  //   string sql = @"
  //   SELECT
  //   *
  //   FROM ingredients
  //   WHERE recipeId = @recipeId
  //   ;";
  //   List<Ingredient> recipeIngredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
  //   return new List<Ingredient>(recipeIngredients);
  // }
  internal Ingredient GetByIngredientId(int ingredientId)
  {
    string sql = @"
  SELECT 
  *
  FROM ingredients
  WHERE id = @ingredientId;
    ";
    Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
    return ingredient;
  }
  internal int DeleteIngredient(int ingredientId)
  {
    string sql = @"
    DELETE
    FROM ingredients
    WHERE id = @ingredientId
    LIMIT 1 ;";
    int rows = _db.Execute(sql, new { ingredientId });
    return rows;
  }
  // internal Ingredient GetIngById(int ingredientId)
  // {
  //   string sql = @"
  //   SELECT
  //   *
  //   FROM ingredients
  //   WHERE id = @ingredientId;
  //   ";
  //   Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
  //   return ingredient;
  // }
}
