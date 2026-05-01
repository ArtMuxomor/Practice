namespace FinalTaskFinanceAPI.Constants
{
    /// <summary>
    /// Числовые константы для проверок.
    /// </summary>
    public static class FinanceLimits
    {
        /// <summary>
        /// Лимит одной транзакций.
        /// </summary>
        public const double TRANSACTION_MAX_SINGLE = 1_000_000.00;

        /// <summary>
        /// Лимит транзакций в день.
        /// </summary>
        public const double TRANSACTION_MAX_DAY = 1_000_000.00;

        /// <summary>
        /// Лимит бюджета категории в формате (18, 2).
        /// </summary>
        public const double CATEGORY_MAX_BUDGET = 9_999_999_999_999_999.99;
    }
}
