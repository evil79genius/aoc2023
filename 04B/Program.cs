using System.Text.RegularExpressions;

namespace _04B
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
            int[] copies = lines.Select(l => 1).ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                var match = card.Match(lines[i]);
                var winning = match.Groups[2].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToHashSet();
                var owned = match.Groups[3].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToHashSet();
                int score = winning.Count(i => owned.Contains(i));
                for (int j = i + 1; j <= i + score; j++)
                {
                    if (j < lines.Length)
                    {
                        copies[j] += copies[i];
                    }
                }
                sum += copies[i];
            }
            Console.WriteLine("{0}", sum);
        }
    }
}
