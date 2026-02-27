using System.Globalization;
using System.Text;

namespace ValueTypes.Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal deposit = 1000;
            int years = 3;
            decimal rate = 10;

            var taskResult = CalculatePercents(deposit, years, rate);
            Console.WriteLine(taskResult);
        }

        public static string CalculatePercents(decimal initialDeposit, int years, decimal interestRate)
        {
            var resultData = new StringBuilder();

            for (int currentYear = 1; currentYear <= years; currentYear++)
            {
                initialDeposit += initialDeposit * (interestRate / 100);

                var amountFormatted = initialDeposit.ToString("F2", CultureInfo.InvariantCulture);

                resultData.Append($"Год {currentYear}: {amountFormatted} руб.");
                if (currentYear < years)
                {
                    resultData.Append('\n');
                }
            }

            return resultData.ToString();
        }
    }
}