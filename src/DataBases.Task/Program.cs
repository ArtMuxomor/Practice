using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataBases.Task
{
    public class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Настройка конфигурации
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Строка для подключения
            string connectionStr = config.GetConnectionString("DefaultConnection")!;

            if (connectionStr.Contains("SERVER_NAME") ||
                connectionStr.Contains("DATABASE_NAME"))
            {
                Console.WriteLine("Сперва задайте название сервера и базы данных в файле appsettings.json");
                return;
            }

            Console.WriteLine("/// Тест CRUD в Ado.Net");
            DemoAdoNet(connectionStr);

            Console.WriteLine("\n/// Тест CRUD в Entity Framework");
            await DemoEntityFramework(connectionStr);
        }

        /// <summary>
        /// Демо Ado.Net.
        /// </summary>
        /// <param name="connectionStr">Строка с подключением.</param>
        static void DemoAdoNet(string connectionStr)
        {
            int selectLimit = 1000;
            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                var newName = "Отталкивание (ADO)";

                // Create (insert)
                string insertSql = "INSERT INTO Stats (StatName) VALUES (@name)";
                using (var cmd = new SqlCommand(insertSql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", newName);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"ADO: Добавлена стата: \"{newName}\"");
                }

                // Read
                int lastId = 0;

                AdoNetPrintStatsConsole(connection, ref lastId, selectLimit);

                // Update
                string updateSql = "UPDATE Stats SET StatName = @name WHERE StatId = @id";
                using (var cmd = new SqlCommand(updateSql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", "Двойное отталкивание (ADO)");
                    cmd.Parameters.AddWithValue("@id", lastId);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("ADO: Запись обновлена.");
                }

                AdoNetPrintStatsConsole(connection, ref lastId, selectLimit);

                // Delete
                string deleteSql = "DELETE FROM Stats WHERE StatId = @id";
                using (var cmd = new SqlCommand(deleteSql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", lastId);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("ADO: Запись удалена.");
                }

                AdoNetPrintStatsConsole(connection, ref lastId, selectLimit);
            }
        }

        /// <summary>
        /// Вывод всех стат в консоль через Ado.Net.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="lastId"></param>
        static void AdoNetPrintStatsConsole(SqlConnection connection, ref int lastId, int limit = 100)
        {
            string selectSql = $"SELECT TOP {limit} StatId, StatName FROM Stats ORDER BY StatId";
            using (var cmd = new SqlCommand(selectSql, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine($"\nВсе статы (первые {limit}):");
                    var currentStat = new Stat();
                    while (reader.Read())
                    {
                        currentStat = new Stat
                        {
                            StatId = reader.GetInt32(0),
                            StatName = reader.GetString(1)
                        };
                        Console.WriteLine(currentStat);
                    }
                    Console.WriteLine();
                    lastId = currentStat.StatId;
                }
            }
        }

        /// <summary>
        /// Демо Entity Framework.
        /// </summary>
        /// <param name="connStr">Строка с подключением.</param>
        /// <returns>Асинхронная задача.</returns>
        static async System.Threading.Tasks.Task DemoEntityFramework(string connStr)
        {
            int selectLimit = 1000;

            // Подключение к БД
            using (var db = new GameDbContext(connStr))
            {
                // Create (insert)
                var newStat = new Stat { StatName = "Сопротивление (EF)" };
                db.Stats.Add(newStat);
                await db.SaveChangesAsync();
                Console.WriteLine($"Entity Framework: Добавлена стата {newStat}");

                await EntityFrameworkPrintStatsConsoleAsync(db, selectLimit);

                // Read
                var stat = await db.Stats
                    .OrderByDescending(s => s.StatId)
                    .FirstOrDefaultAsync<Stat>();

                Console.WriteLine($"EF: Прочитана последняя стата {stat}\n");

                // Update
                stat.StatName = "Сопротивление к огню (EF)";
                await db.SaveChangesAsync();
                Console.WriteLine("Entity Framework: Запись обновлена.");

                await EntityFrameworkPrintStatsConsoleAsync(db, selectLimit);

                // Delete
                db.Stats.Remove(stat);
                await db.SaveChangesAsync();
                Console.WriteLine("Entity Framework: Запись удалена.");

                await EntityFrameworkPrintStatsConsoleAsync(db, selectLimit);
            }
        }

        /// <summary>
        /// Вывод всех стат в консоль через Entity Framework.
        /// </summary>
        /// <param name="gdb">Объект GameDbContext.</param>
        /// <returns>Асинхронная задача.</returns>
        static async System.Threading.Tasks.Task EntityFrameworkPrintStatsConsoleAsync(GameDbContext gdb, int limit = 100)
        {
            Console.WriteLine($"\nВсе статы (первые {limit}):");

            var stats = gdb.Stats
                .OrderBy(s => s.StatId)
                .Take(limit);

            foreach (var statOut in stats)
            {
                Console.WriteLine(statOut);
            }
            Console.WriteLine("");
        }
    }
}