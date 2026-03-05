namespace Collections.Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var stack = new SmartStack<int>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}