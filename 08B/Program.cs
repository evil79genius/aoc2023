using System.Runtime;
using System.Text.RegularExpressions;

namespace _08B
{
    internal class Program
    {
        private static long Gcd(long a, long b)
        {
            long r;
            while ((r = a % b) != 0)
            {
                a = b;
                b = r;
            }
            return b;
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass input filename as 1st parameter");
                return;
            }
            string[] lines = File.ReadAllLines(args[0]);

            char[] path = lines[0].ToCharArray();
            Regex node = new Regex(@"[12A-Z]{3}");
            Dictionary<string, (string, string)> map = lines[2..].Select(l => node.Matches(l)).ToDictionary(m => m[0].Value, m => (m[1].Value, m[2].Value));

            string[] startNodes = map.Keys.Where(k => k.EndsWith('A')).ToArray();
            var loops = startNodes.Select(s => KeyValuePair.Create(s,
                new Dictionary<string, List<long>>()
                )).ToDictionary();
            Dictionary<string, long> start = new Dictionary<string, long>();
            Dictionary<string, long> periods = new Dictionary<string, long>();
            foreach (string startNode in startNodes)
            {
                string curNode = startNode;
                long step = 0;
                long? prev = null;
                while(true)
                {
                    curNode = path[step % path.Length] == 'L'
                        ? map[curNode].Item1
                        : map[curNode].Item2;
                    step++;
                    if (curNode.EndsWith('Z'))
                    {
                        if (!loops[startNode].ContainsKey(curNode))
                        {
                            loops[startNode][curNode] = new List<long>();
                        }
                        prev = loops[startNode][curNode].Select(s => (long?)s).FirstOrDefault(s => ((step - s) % path.Length) == 0);
                        if (prev.HasValue)
                        {
                            periods[startNode] = step - prev.Value;
                            loops[startNode][curNode].Add(step);
                            break;
                        }
                        loops[startNode][curNode].Add(step);
                    }
                }
                Console.WriteLine("Period: {0}\nzNodes: {1}", step - prev, string.Join(' ', loops[startNode].SelectMany(kvp => kvp.Value).ToList()));
            }
            Console.WriteLine("\nPeriods: {0}", string.Join(' ', periods.Values));
            long lcm = 1;
            while (periods.Count > 0)
            {
                string key = periods.Keys.First();
                long fact = periods[key];
                periods.Remove(key);
                long gcd = Gcd(lcm, fact);
                lcm = lcm / gcd * fact;
            }
            Console.WriteLine("{0}", lcm);
        }
    }
}
