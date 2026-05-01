using FinalTaskFinanceAPI.Models;

namespace FinalTaskFinanceAPI.DTO
{
    /// <summary>
    /// Модификация вывода статей расходов.
    /// </summary>
    public class ItemDto
    {

        /// <summary>
        /// Название статьи расходов.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Название родительской категории расходов.
        /// </summary>
        public string ExpenseCategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Активность статьи расходов.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Идентификаторы зависимых транзакций.
        /// </summary>
        public List<Guid> TransactionIds { get; set; } = new();

        /// <summary>
        /// Конструктор DTO.
        /// </summary>
        public ItemDto()
        {
        }

        /// <summary>
        /// Конструктор DTO из статьи.
        /// </summary>
        /// <param name="item"></param>
        public ItemDto(ExpenseItem item)
        {
            Name = item.Name;
            ExpenseCategoryName = item.ExpenseCategoryName;
            IsActive = item.IsActive;
            TransactionIds = item.Transactions.Select(t => t.Id).ToList();
        }
    }
}