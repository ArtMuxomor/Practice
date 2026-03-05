namespace OOP.Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Создание прямоугольника");
                var rect = new CustomRectangle(10, 10, 60, 80);

                PrintRectangleInfo(rect);

                Console.WriteLine("\nСмещение на (9, -25) и изменение размера");
                rect.Offset(9, -25);
                rect.SetSize(65.41, 37.15);

                PrintRectangleInfo(rect);

                Console.WriteLine("\nДемонстрация исключения (отрицательная ширина)");
                rect.SetSize(-10, 60);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА: {ex.Message}");
            }

            try
            {
                var rect = new CustomRectangle(10, 10, 60, 80);

                Console.WriteLine("\nДемонстрация исключения (отрицательная длина)");
                rect.SetSize(10, -60);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА: {ex.Message}");
            }
        }

        static void PrintRectangleInfo(CustomRectangle rect)
        {
            Console.WriteLine($"{"Начальная точка",15} | ({rect.X}, {rect.Y})");
            Console.WriteLine($"{"Размер",15} | Ш: {rect.Width}, В: {rect.Height}");
            Console.WriteLine($"{"Площадь",15} | {rect.Area:F3}");
            Console.WriteLine($"{"Периметр",15} | {rect.Perimeter:F3}");
            Console.WriteLine($"{"Диагональ",15} | {rect.Diagonal:F3}");
        }
    }
}