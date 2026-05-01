
using FinalTaskFinanceAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalTaskFinanceAPI
{
    /// <summary>
    /// Исполняемая программа.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Главный исполняемый метод.
        /// </summary>
        /// <param name="args">Список аргументов.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services
                .AddDbContext<FinanceDbContext>(
                    options => options.UseSqlite("Data Source=finance.db")
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
