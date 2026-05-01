using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalTaskFinanceAPI.Constants;

namespace FinalTaskFinanceAPI.Models
{
    /// <summary>
    /// Транзакция.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Идентификатор транзакции (ПК).
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Название родительской статьи расходов (ВК).
        /// </summary>
        [ForeignKey("ExpenseItemName")]
        [Required(ErrorMessage = "Необходимо указать статью расходов.")]
        public string ExpenseItemName { get; set; } = string.Empty;

        /// <summary>
        /// Дата совершения транзакции.
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }

        /// <summary>
        /// Сумма транзакции.
        /// </summary>
        [Required]
        [Range(0.01, FinanceLimits.TRANSACTION_MAX_SINGLE, ErrorMessage = "Сумма транзакции отрицательная или превышает лимит.")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Комментарий к транзакции.
        /// </summary>
        [StringLength(500)]
        public string Comment { get; set; } = string.Empty;
    }
}