using FinalTaskFinanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalTaskFinanceAPI.Data
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    public class FinanceDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста.
        /// </summary>
        /// <param name="options">Настройки для создания контекста.</param>
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
          : base(options)
        {
        }

        /// <summary>
        /// Таблица категорий.
        /// </summary>
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        /// <summary>
        /// Таблица статей.
        /// </summary>
        public DbSet<ExpenseItem> ExpenseItems { get; set; }

        /// <summary>
        /// Таблица транзакций.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Точность денежных данных

            modelBuilder
              .Entity<ExpenseCategory>()
              .Property(c => c.MonthlyBudget)
              .HasPrecision(18, 2);

            modelBuilder
              .Entity<Transaction>()
              .Property(t => t.Amount)
              .HasPrecision(18, 2);

            // Связь Категория-Статья (1:N)
            modelBuilder.Entity<ExpenseItem>()
              .HasOne<ExpenseCategory>()
              .WithMany(c => c.Items)
              .HasForeignKey(i => i.ExpenseCategoryName)
              .OnDelete(DeleteBehavior.Cascade);

            // Связь Статья-Транзакция (1:N)
            // Ограничение удаления статьи с транзакциями
            modelBuilder.Entity<Transaction>()
              .HasOne<ExpenseItem>()
              .WithMany(i => i.Transactions)
              .HasForeignKey(t => t.ExpenseItemName)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}