namespace Collections.Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var stackInt = new SmartStack<int>([1, 2, 3, 4]);
                Console.WriteLine("Создали стек с 4 элементами");
                // Вместимость 4
                Console.WriteLine($"Вместимость при {stackInt.Count} элементах - {stackInt.Capacity}");

                // Добавление 5 увеличивает вместимость до 8
                stackInt.Push(5);
                Console.WriteLine($"Вместимость при {stackInt.Count} элементах - {stackInt.Capacity}");

                // При добавлении 9 увеличивает вместимость до 16
                stackInt.PushRange([6, 7, 8, 9, 10, 11, 12]);
                Console.WriteLine($"Вместимость при {stackInt.Count} элементах - {stackInt.Capacity}\n");

                Console.WriteLine("Все элементы первого стека от вершины до хвоста:");
                Console.WriteLine($"{String.Join(", ", stackInt)}\n");

                stackInt.Pop();
                // Вывод 11
                Console.WriteLine($"Метод Pop() вызван 1 раз. Первый элемент - {stackInt.Peek()}");
                stackInt.Pop();
                // Вывод 10
                Console.WriteLine($"Метод Pop() вызван 2 раза. Первый элемент - {stackInt.Peek()}\n");

                stackInt.Peek();
                // Вывод 10
                Console.WriteLine($"Метод Peek() вызван 1 раз. Первый элемент - {stackInt.Peek()}");
                stackInt.Peek();
                // Вывод 10
                Console.WriteLine($"Метод Peek() вызван 2 раза. Первый элемент - {stackInt.Peek()}\n");

                Console.WriteLine("Все элементы стека, полученные через индексатор []:");
                for (int i = 0; i < stackInt.Count; i++)
                {
                    Console.Write($"{stackInt[i]} ");
                }

                Console.WriteLine("\n\n------------------------------\n");

                var stackString = new SmartStack<string>(["string1", "string2", "string3"]);

                Console.WriteLine("Все элементы второго стека от вершины до хвоста:");
                Console.WriteLine($"{String.Join(", ", stackString)}\n");

                // Дальше 3 раза True
                Console.WriteLine($"Наличие \"string1\" в стеке - {stackString.Contains("string1")}");
                Console.WriteLine($"Наличие \"string2\" в стеке - {stackString.Contains("string2")}");
                Console.WriteLine($"Наличие \"string3\" в стеке - {stackString.Contains("string3")}");

                // 1 раз False ("string4" не в стеке)
                Console.WriteLine($"Наличие \"string4\" в стеке - {stackString.Contains("string4")}\n");

                // 1 раз False (нет пустых в трёх элементах)
                Console.WriteLine($"Наличие null в стеке - {stackString.Contains(null)}\n");

                stackString.Push(null);
                Console.WriteLine("Добавили null в стек");
                // 1 раз True, так как добавили null
                Console.WriteLine($"Наличие null в стеке - {stackString.Contains(null)}\n");

                Console.WriteLine("Все элементы второго стека с null:");
                Console.WriteLine($"{String.Join(", ", stackString)}\n");

                stackString.Pop();
                Console.WriteLine("Убрали null из стека");

                // 1 раз False ("выкинули" последний null)
                Console.WriteLine($"Наличие null в стеке - {stackString.Contains(null)}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}