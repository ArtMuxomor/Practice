using System.Globalization;
using System.Text;

namespace ValueTypes.Task1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var deposit = 1000;
            var years = 3;
            var rate = 10;

            Console.WriteLine($"""
                Начальный вклад: {deposit}
                Сколько лет: {years}
                Годовая процентная ставка: {rate}

                """);

            var yearRecords = CalculateDepositHistory(deposit, years, rate);

            foreach (var (year, amount) in yearRecords)
            {
                Console.WriteLine($"Год {year}: {amount.ToString("F2", CultureInfo.InvariantCulture)} руб.");
            }
        }

        /// <summary>
        /// Считает проценты на заданное количество лет.
        /// </summary>
        /// <param name="initialDeposit">Начальный депозит.</param>
        /// <param name="years">Количество лет.</param>
        /// <param name="interestRate">Процентная ставка в процентах.</param>
        /// <returns>Массив пар (год, сумма).</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Любое из значений оказалось не положительным.
        /// </exception>
        public static (int Year, decimal Amount)[] CalculateDepositHistory(decimal initialDeposit, int years, decimal interestRate)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(initialDeposit);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(years);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(interestRate);

            var result = new (int Year, decimal Amount)[years];
            var currentAmount = initialDeposit;
            var multiplier = 1 + (interestRate / 100);

            for (int year = 0; year < years; year++)
            {
                result[year].Year = year + 1;

                currentAmount *= multiplier;
                result[year].Amount = currentAmount;

                currentAmount = Math.Round(currentAmount, 2, MidpointRounding.AwayFromZero);
            }

            return result;
        }
    }
}