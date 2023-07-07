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
    (id, name, quantity, recipeId)
    VALUES
    (@id, @name, @quantity, @recipeId);
    
    SELECT
    *
    FROM ingredients
    WHERE id = LAST_INSERT_ID();";
    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
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
}
