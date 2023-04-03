namespace TheFinal.Repositories;

public class AccountsRepository
{
  private readonly IDbConnection _db;

  public AccountsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Account GetByEmail(string userEmail)
  {
    string sql = "SELECT * FROM accounts WHERE email = @userEmail";
    return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
  }

  internal Account GetById(string id)
  {
    string sql = "SELECT * FROM accounts WHERE id = @id";
    return _db.QueryFirstOrDefault<Account>(sql, new { id });
  }

  internal Account Create(Account newAccount)
  {
    string sql = @"
            INSERT INTO accounts
              (name, picture, email, id, coverImg)
            VALUES
              (@Name, @Picture, @Email, @Id, @coverImg)";
    _db.Execute(sql, newAccount);
    return newAccount;
  }

  internal Account Edit(Account update)
  {
    string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
    _db.Execute(sql, update);
    return update;
  }

    internal List<Keep> GetKeepsByProfile(Account userInfo)
    {
        string sql = @"
        SELECT 
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON k.creatorId = a.id
        WHERE k.creatorId = @Id;";
        return _db.Query<Keep, Account, Keep>(sql, (keep, account) => {
          keep.Creator = account; return keep; },
          new { userInfo.Id }, splitOn: "id").ToList();
    }
}

