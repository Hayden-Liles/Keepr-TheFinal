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
              (@Name, @Picture, @Email, @Id, 'https://images.unsplash.com/photo-1484069560501-87d72b0c3669?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8cXVlc3Rpb258ZW58MHx8MHx8&auto=format&fit=crop&w=700&q=60')";
    _db.Execute(sql, newAccount);
    return newAccount;
  }

  internal Account Edit(Account update)
  {
    string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture,
              coverImg = @coverImg
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

