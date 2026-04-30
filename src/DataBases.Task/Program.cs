using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataBases.PracticeTask
{
    public class Program
    {
        static async Task Main(string[] args)
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

            int selectLimit = 1000;

            Console.WriteLine("/// Тест CRUD в Ado.Net");
            await RunDemo(new DbDemoAdoNet(connectionStr), selectLimit);

            Console.WriteLine("\n/// Тест CRUD в Entity Framework");
            await RunDemo(new DbDemoEntityFramework(connectionStr), selectLimit);
        }

        /// <summary>
        /// Запускает демо.
        /// </summary>
        /// <param name="demo">Демо.</param>
        /// <returns>Асинхронная задача.</returns>
        static async Task RunDemo(DbDemo demo, int selectLimit = 1000)
        {
            Console.WriteLine("Добавляется новая запись.");
            await demo.Create("Новая стата");

            Console.WriteLine("Все статы:");
            PrintAllConsole(await demo.Read(selectLimit));

            Console.WriteLine("\nЗапись изменяется.");
            await demo.Update("Новая стата (обновлённая)");

            Console.WriteLine("Все статы:");
            PrintAllConsole(await demo.Read(selectLimit));

            Console.WriteLine("\nЗапись удаляется.");
            await demo.Delete();

            Console.WriteLine("Все статы:");
            PrintAllConsole(await demo.Read(selectLimit));
        }

        /// <summary>
        /// Выводит все элементы из списка в консоль.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        static void PrintAllConsole<T>(IEnumerable<T> list)
        {
            foreach (var obj in list)
            {
                Console.WriteLine(obj);
            }
        }
    }
}