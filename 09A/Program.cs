using System.Text.RegularExpressions;

namespace _09A
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
            long sum = 0;

            Regex numbers = new Regex(@"-?[\d]+");
            foreach (string line in lines)
            {
                List<long> deltas = new List<long>();
                long[] work = numbers.Matches(line).Select(m => long.Parse(m.Value)).ToArray();
                int len = work.Length;
                while (!work[0..(len-1)].All(w => w == 0))
                {
                    for (int i = 1; i < len; i++)
                    {
                        work[i - 1] = work[i] - work[i - 1];
                    }
                    len--;
                }
                while (len < work.Length-1)
                {
                    work[len + 1] += work[len];
                    len++;
                }
                sum += work[len];
                Console.WriteLine("{0}", work[len]);
            }
            Console.WriteLine("\n{0}", sum);
        }
    }
}
