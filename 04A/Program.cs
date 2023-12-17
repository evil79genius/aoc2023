using System.Text.RegularExpressions;

namespace _04A
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
            Regex card = new Regex(@"(.*):(.*)\|(.*)");
            int sum = 0;
            foreach (string line in lines)
            {
                var match = card.Match(line);
                var winning = match.Groups[2].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToHashSet();
                var owned = match.Groups[3].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToHashSet();
                int score = winning.Count(i => owned.Contains(i));
                if (score > 0)
                {
                    sum += (int)Math.Pow(2, (score - 1));
                }
            }
            Console.WriteLine("{0}", sum);
        }
    }
}
