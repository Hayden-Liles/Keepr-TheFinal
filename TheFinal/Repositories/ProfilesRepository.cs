namespace TheFinal.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetKeepsByProfile(string id)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.creatorId = @id";
            return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }, new { id }).ToList();
        }

        internal Profile GetProfile(string id)
        {
            string sql = @"
            SELECT
            *
            FROM accounts
            WHERE id = @id";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id });
        }

        internal List<Vault> GetVaultsByProfile(string id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @id";
            return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, new { id }).ToList();
        }
    }
}