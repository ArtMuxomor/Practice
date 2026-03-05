namespace Collections.Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var stack = new SmartStack<int>(0);
                stack.Push(1);
                stack.Push(2);
                stack.Push(3);
                stack.Push(4);
                stack.Push(5);
                stack.PushRange([6, 7, 8, 9, 10, 11, 12]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}