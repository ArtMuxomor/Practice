using System.Text;

namespace ValueTypes.Task2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var diamondSize = 5;

            var symbolIndexes = GetDiamondIndexes(diamondSize);

            PrintDiamondToConsole(symbolIndexes, 'X');
        }

        /// <summary>
        /// Генерация индексов матрицы для отрисовки ромба.
        /// </summary>
        /// <param name="diagonalLength">Длина диагоналей ромба.</param>
        /// <returns>
        /// Массив пар индексов (левый и правый), определяющих положение границ ромба в каждой строке.
        /// </returns>
        /// <exception cref="ArgumentException">Если длина диагонали чётная.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Если длина диагонали не больше 0.</exception>
        public static (int X1, int X2)[] GetDiamondIndexes(int diagonalLength)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(diagonalLength);

            if (diagonalLength % 2 == 0)
            {
                throw new ArgumentException("Длина диагонали должна быть нечётной.", nameof(diagonalLength));
            }

            var result = new (int X1, int X2)[diagonalLength];
            var center = diagonalLength / 2;

            for (int row = 0; row < diagonalLength; row++)
            {
                int distToCenter = Math.Abs(center - row);

                result[row].X1 = distToCenter;
                result[row].X2 = diagonalLength - distToCenter - 1;
            }

            return result;
        }

        /// <summary>
        /// Отрисовка ромба в консоли, используя индексы.
        /// </summary>
        /// <param name="symbolIndexes">Массив индексов ромба.</param>
        /// <param name="symbol">Символ, которым отрисовывается грань ромба.</param>
        public static void PrintDiamondToConsole((int, int)[] symbolIndexes, char symbol = 'X')
        {
            var line = new StringBuilder(symbolIndexes.Length);
            foreach (var (x1, x2) in symbolIndexes)
            {
                line.Append(' ', symbolIndexes.Length);

                line[x1] = symbol;
                line[x2] = symbol;

                Console.WriteLine(line);

                line.Clear();
            }
        }
    }
}