using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalTaskFinanceAPI.Constants;

namespace FinalTaskFinanceAPI.Models
{
    /// <summary>
    /// Статья расходов.
    /// </summary>
    public class ExpenseItem
    {
        /// <summary>
        /// Название статьи (ПК).
        /// </summary>
        [Key]
        [Required(ErrorMessage = "Название статьи обязательно.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Название родительской категории расходов (ВК).
        /// </summary>
        [ForeignKey("ExpenseCategoryName")]
        [Required(ErrorMessage = "Необходимо указать категорию расходов.")]
        public string ExpenseCategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Активность статьи расходов.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Список транзакций, относящихся к этой статье.
        /// </summary>
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}