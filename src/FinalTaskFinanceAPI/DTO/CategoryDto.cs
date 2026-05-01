using FinalTaskFinanceAPI.Models;

namespace FinalTaskFinanceAPI.DTO
{
    /// <summary>
    /// Модификация вывода категорий расходов.
    /// </summary>
    public class CategoryDto
    {

        /// <summary>
        /// Название категории расходов.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Установленный месячный бюджет.
        /// </summary>
        public decimal MonthlyBudget { get; set; }

        /// <summary>
        /// Активность категории.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Имена зависимых статей расходов.
        /// </summary>
        public List<string> ItemNames { get; set; } = new();

        /// <summary>
        /// Конструктор DTO.
        /// </summary>
        public CategoryDto()
        {
        }

        /// <summary>
        /// Конструктор DTO из категории.
        /// </summary>
        /// <param name="category"></param>
        public CategoryDto(ExpenseCategory category)
        {
            Name = category.Name;
            MonthlyBudget = category.MonthlyBudget;
            IsActive = category.IsActive;
            ItemNames = category.Items.Select(i => i.Name).ToList();
        }
    }
}
