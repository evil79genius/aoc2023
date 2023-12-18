using System.Text.RegularExpressions;

namespace _08B
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

            char[] path = lines[0].ToCharArray();
            Regex node = new Regex(@"[12A-Z]{3}");
            Dictionary<string, (string, string)> map = lines[2..].Select(l => node.Matches(l)).ToDictionary(m => m[0].Value, m => (m[1].Value, m[2].Value));

            string[] curNodes = map.Keys.Where(k => k.EndsWith('A')).ToArray();
            int step = 0;
            int sub = 0;
            while (!curNodes.All(n => n.EndsWith('Z')))
            {
                curNodes = curNodes.Select(n => path[sub] == 'L'
                    ? map[n].Item1
                    : map[n].Item2).ToArray();
                int cnt = curNodes.Count(n => n.EndsWith('Z'));
                if (cnt > 2)
                {
                    Console.WriteLine("Step {0}[{1}] - {2}: {3}", step, sub, cnt, string.Join(' ', curNodes));
                }
                step++;
                sub++;
                if (sub == path.Length) { sub = 0; }
            }
            Console.WriteLine("{0}", step);
        }
    }
}
