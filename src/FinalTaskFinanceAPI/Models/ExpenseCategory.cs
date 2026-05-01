using System.ComponentModel.DataAnnotations;
using FinalTaskFinanceAPI.Constants;

namespace FinalTaskFinanceAPI.Models
{
    /// <summary>
    /// Категория расходов.
    /// </summary>
    public class ExpenseCategory
    {
        /// <summary>
        /// Название категории (ПК).
        /// </summary>
        [Key]
        [Required(ErrorMessage = "Название категории обязательно.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Месяцное ограничение бюджета.
        /// </summary>
        [Required]
        [Range(0.01, FinanceLimits.CATEGORY_MAX_BUDGET, ErrorMessage = "Бюджет должен быть положительным числом менее 10 квдрл.")]
        public decimal MonthlyBudget { get; set; }

        /// <summary>
        /// Активность категории.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Список статей расходов, относящихся к этой категории.
        /// </summary>
        public ICollection<ExpenseItem> Items { get; set; } = new List<ExpenseItem>();
    }
}