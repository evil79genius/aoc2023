using System.Text.RegularExpressions;

namespace _05A
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

            long[] source = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();

            int curLine = 1;
            while (curLine < lines.Length)
            {
                long[] dest = (long[])source.Clone();
                while (++curLine < lines.Length)
                {
                    if (lines[curLine].EndsWith("map:")) continue;
                    if (lines[curLine].Length == 0) break;
                    long[] map = lines[curLine].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
                    for (int s = 0; s < source.Length; s++)
                    {
                        if (source[s] >= map[1] && source[s] < (map[1] + map[2]))
                        {
                            dest[s] = source[s] - map[1] + map[0];
                        }
                    }
                }
                source = dest;
            }
            Console.WriteLine("{0}", source.Min());
        }
    }
}
