namespace TheFinal.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault createVault(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, description, img, isPrivate)
            VALUES
            (@creatorId, @name, @description, @img, @isPrivate);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }

        internal Vault getVault(int id)
        {
            string sql = @"
            SELECT
            vaults.*,
            acts.*
            FROM vaults
            JOIN accounts acts ON vaults.creatorId = acts.id
            WHERE vaults.id = @id;
            ";
            Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, creator) => {
                vault.Creator = creator;
                return vault;
            }, new{id}).FirstOrDefault();
            if(vault == null) throw new Exception($"No vault at Id: {id}");
            return vault;
        }

        internal Vault updateVault(Vault vaultData)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @name,
            description = @description,
            img = @img,
            isPrivate = @isPrivate
            WHERE id = @id;
            ";
            int rows = _db.Execute(sql, vaultData);
            return vaultData;
        }

        internal void deleteVault(int id)
        {
            string sql = @"
            DELETE FROM vaults
            where id = @id;
            ";
            int rows = _db.Execute(sql, new { id });
        }
    }
}