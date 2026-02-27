using System.Text;

namespace ValueTypes.Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var line in BuildDiamond(5))
            {
                Console.WriteLine(line);
            }
        }

        public static IEnumerable<string> BuildDiamond(int n)
        {
            if (n % 2 == 0 || n <= 0)
            {
                yield break;
            }

            int center = n / 2;
            var diamondLine = new StringBuilder(n);

            for (int row = 0; row < n; row++)
            {
                int distToCenter = Math.Abs(center - row);

                diamondLine.Clear()
                    .Append(' ', distToCenter)
                    .Append('X');

                if (distToCenter < center)
                {
                    diamondLine
                        .Append(' ', n - (distToCenter + 1) * 2)
                        .Append('X');
                }

                yield return diamondLine.ToString();
            }
        }
    }
}
