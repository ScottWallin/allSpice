namespace allSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites
    (accountId, recipeId)
    VALUES
    (@accountId, @recipeId)
    SELECT LAST_INSERT_Id();
    ";
    int lastInsertId = _db.ExecuteScalar<int>(sql, favoriteData);
    favoriteData.id = lastInsertId;
    // Favorite favorite = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();
    return favoriteData;
  }
}