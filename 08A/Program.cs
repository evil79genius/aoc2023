using System.Text.RegularExpressions;

namespace _08A
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
            Regex node = new Regex(@"[A-Z]{3}");
            Dictionary<string, (string, string)> map = lines[2..].Select(l => node.Matches(l)).ToDictionary(m => m[0].Value, m => (m[1].Value, m[2].Value));

            string curNode = "AAA";
            int step = 0;
            while (curNode != "ZZZ")
            {
                curNode = path[step % path.Length] == 'L'
                    ? map[curNode].Item1
                    : map[curNode].Item2;
                step++;
            }
            Console.WriteLine("{0}", step);
        }
    }
}
