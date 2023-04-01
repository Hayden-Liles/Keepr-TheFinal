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

        internal void deleteVaultKeep(VaultKeep vaultKeep)
        {
            int vkId = vaultKeep.Id;
            int keepId = vaultKeep.keepId;
            string sql = @"
            DELETE
            FROM
            vaultKeeps WHERE id = @vkId";
            _db.Execute(sql, new {vkId});
            string sql2 = @"
            UPDATE keeps
            SET
            kept = kept - 1
            WHERE id = @keepId;
            ";
            int rows = _db.Execute(sql2, new { keepId });
        }
    }
}