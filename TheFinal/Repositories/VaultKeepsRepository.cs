namespace TheFinal.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep createVaultKeep(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@creatorId, @vaultId, @keepId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
            vaultKeepData.Id = id;
            return vaultKeepData;
        }

        internal VaultKeep getVaultKeepById(int vkId)
        {
            string sql = @"
            SELECT
            *
            FROM
            vaultKeeps WHERE id = @vkId";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new {vkId});
        }

        internal void deleteVaultKeep(int vkId)
        {
            string sql = @"
            DELETE
            FROM
            vaultKeeps WHERE id = @vkId";
            _db.Execute(sql, new {vkId});
        }
    }
}