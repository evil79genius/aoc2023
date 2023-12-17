using System.Text.RegularExpressions;

namespace _01A
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass input filename as 1st parameter");
                return;
            }
            string[] lines = File.ReadAllLines(args[0]);
            int sum = 0;
            Regex digit = new Regex("[0-9]");
            foreach (string line in lines)
            {
                var mc = digit.Matches(line);
                sum += (Convert.ToInt32(mc.First().Value) * 10) + Convert.ToInt32(mc.Last().Value);
            }
            Console.WriteLine("{0}", sum);
        }
    }
}
