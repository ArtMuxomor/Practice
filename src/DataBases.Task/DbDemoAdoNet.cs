using Microsoft.Data.SqlClient;

namespace DataBases.PracticeTask
{
    public class DbDemoAdoNet : DbDemo
    {

        public DbDemoAdoNet(string connectionStr)
            : base(connectionStr)
        {
        }

        public override async Task Create(string objName)
        {
            objName += " (ADO)";

            using var connection = new SqlConnection(_connectionStr);

            await connection.OpenAsync();

            string insertSql = "INSERT INTO Stat (StatName) VALUES (@name)";

            using var cmd = new SqlCommand(insertSql, connection);

            cmd.Parameters.AddWithValue("@name", objName);

            await cmd.ExecuteNonQueryAsync();
        }

        public override async Task<List<Stat>> Read(int selectLimit = 1000)
        {
            using var connection = new SqlConnection(_connectionStr);

            await connection.OpenAsync();

            string selectSql = $"""
                SELECT TOP {selectLimit} StatId, StatName
                FROM Stat
                ORDER BY StatId
                """;

            using var cmd = new SqlCommand(selectSql, connection);

            using var reader = await cmd.ExecuteReaderAsync();

            var statList = new List<Stat>();

            while (reader.Read())
            {
                statList.Add(new Stat
                {
                    StatId = reader.GetInt32(0),
                    StatName = reader.GetString(1)
                });
            }

            return statList;
        }

        public override async Task Update(string newObjName)
        {
            newObjName += " (ADO)";

            using var connection = new SqlConnection(_connectionStr);

            await connection.OpenAsync();

            string updateSql = """
                UPDATE Stat
                SET StatName = @name
                WHERE StatId = (SELECT MAX(StatId) FROM Stat)
                """;

            using var cmd = new SqlCommand(updateSql, connection);

            cmd.Parameters.AddWithValue("@name", newObjName);

            await cmd.ExecuteNonQueryAsync();
        }

        public override async Task Delete()
        {
            using var connection = new SqlConnection(_connectionStr);

            await connection.OpenAsync();

            string deleteSql = """
                DELETE FROM Stat
                WHERE StatId = (SELECT MAX(StatId) FROM Stat)
                """;

            using var cmd = new SqlCommand(deleteSql, connection);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}