namespace TheFinal.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (`creatorId`, name, description, img)
            VALUES
            (@creatorId, @name, @description, @img);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            string sql2 = @"
            UPDATE keeps
            SET
            kept = kept + 1
            WHERE id = @id;
            ";
            int rows = _db.Execute(sql2, new { id });
            keepData.Kept++;
            return keepData;
        }

        internal Keep getKeep(int id)
        {
            string sql = @"
            SELECT
            keeps.*,
            acts.*
            FROM keeps
            JOIN accounts acts ON keeps.creatorId = acts.id
            WHERE keeps.id = @id;
            ";
            Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, creator) => {
                keep.Creator = creator;
                return keep;
            }, new{id}).FirstOrDefault();
            if(keep == null) throw new Exception($"No keep at Id: {id}");
            string sql2 = @"
            UPDATE keeps
            SET
            views = views + 1
            WHERE id = @id;
            ";
            int rows = _db.Execute(sql2, new { id });
            keep.Views++;
            return keep;
        }

        internal List<Keep> getKeeps()
        {
            string sql = @"
            SELECT
            keeps.*,
            acts.*
            FROM keeps
            JOIN accounts acts ON keeps.creatorId = acts.id;
            ";
            List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, creator) => {
                keep.Creator = creator;
                return keep;
            }).ToList();
            return keeps;
        }

        internal Keep updateKeep(Keep keepData)
        {
            string sql = @"
            UPDATE keeps
            set
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id;
            ";
            int rows = _db.Execute(sql, keepData);
            return keepData;
        }

        internal string deleteKeep(int id)
        {
            string sql = @"
            DELETE FROM keeps
            WHERE id = @id;
            ";
            int rows = _db.Execute(sql, new { id });
            return $"keep at Id: {id} has been succesfully deleted";
        }
    }
}