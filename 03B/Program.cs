using System.Text.RegularExpressions;

namespace _03B
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
            Regex gears = new Regex(@"\*");
            Dictionary<(int, int), List<int>> gearsMap = new Dictionary<(int, int), List<int>>();
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (int j in gears.Matches(lines[i]).Select(m => m.Index))
                {
                    gearsMap[(i, j)] = new List<int>();
                }
            }
            Regex pieces = new Regex(@"[\d]+");
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (Match piece in pieces.Matches(lines[i]))
                {
                    int gearTeeth = int.Parse(piece.Value);
                    for (int j = piece.Index-1; j < piece.Index+piece.Value.Length+1; j++)
                    {
                        if (gearsMap.ContainsKey((i-1,j)))
                        {
                            gearsMap[(i-1,j)].Add(gearTeeth);
                        }
                        if (gearsMap.ContainsKey((i,j)))
                        {
                            gearsMap[(i,j)].Add(gearTeeth);
                        }
                        if (gearsMap.ContainsKey((i+1,j)))
                        {
                            gearsMap[(i+1,j)].Add(gearTeeth);
                        }
                    }
                }
            }
            int sum = 0;
            foreach (List<int> gear in gearsMap.Values)
            {
                if (gear.Count == 2)
                {
                    sum += gear.First() * gear.Last();
                }
            }
            Console.WriteLine("{0}", sum);
        }
    }
}
