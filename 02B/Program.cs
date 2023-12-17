using System.Text.RegularExpressions;

namespace _02B
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
            Regex game = new Regex("^Game ([0-9]+): (.*)$");
            Regex draws = new Regex("([0-9]+) (red|green|blue)");
            foreach (string line in lines)
            {
                var match = game.Match(line);
                if (!match.Success)
                {
                    Console.WriteLine("Badly formatted line:\n{0}", line);
                    return;
                }
                int minRed = 0;
                int minGreen = 0;
                int minBlue = 0;
                foreach (string draw in match.Groups[2].Value.Split("; "))
                {
                    var matches = draws.Matches(draw);
                    if (matches.Count > 3)
                    {
                        Console.WriteLine("Badly formatted line:\n{0}", line);
                        return;
                    }
                    foreach (Match color in matches)
                    {
                        int cubes = int.Parse(color.Groups[1].Value);
                        switch (color.Groups[2].Value)
                        {
                            case "red":
                                if (cubes > minRed) minRed = cubes;
                                break;
                            case "green":
                                if (cubes > minGreen) minGreen = cubes;
                                break;
                            case "blue":
                                if (cubes > minBlue) minBlue = cubes;
                                break;
                        }
                    }
                }
                sum += minRed * minGreen * minBlue;
            }
            Console.WriteLine("{0}", sum);
        }
    }
}
