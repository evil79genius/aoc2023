using System.Text.RegularExpressions;

namespace _12A
{
    internal class Program
    {
        private class Line
        {
            public Line(string i)
            {
                parts = i.Split(' ');
                len = parts[0].Length;
                Regex brokenRegex = new Regex(@"#");
                Regex unknownRegex = new Regex(@"\?");
                broken = brokenRegex.Matches(parts[0]).Select(m => m.Index).ToArray();
                unknown = unknownRegex.Matches(parts[0]).Select(m => m.Index).ToArray();
                brokenGroups = parts[1].Split(',').Select(b => int.Parse(b)).ToArray();
                toBreak = brokenGroups.Sum() - broken.Length;
                toFix = unknown.Length - toBreak;
                tryBreaking = new int[toBreak];
            }
            string[] parts;
            int len;
            int[] broken;
            int[] unknown;
            int[] brokenGroups;
            int toBreak;
            int toFix;
            int[] tryBreaking;

            public int CountWays()
            {
                return CountWays(toBreak, toFix);
            }

            private int CountWays(int toBreak, int toFix)
            {
                if (toBreak + toFix == 0)
                {
                    return Evaluate() ? 1 : 0;
                }
                int count = 0;
                if (toBreak > 0)
                {
                    tryBreaking[toBreak - 1] = unknown[toBreak + toFix - 1];
                    count += CountWays(toBreak - 1, toFix);
                }
                if (toFix > 0)
                {
                    count += CountWays(toBreak, toFix - 1);
                }
                return count;
            }

            private bool Evaluate()
            {
                int[] pattern = broken.Concat(tryBreaking).Order().ToArray();
                int group = 0;
                int groupLength = 0;
                int prev = pattern[0] - 1;
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (pattern[i] == prev + 1)
                    {
                        groupLength++;
                    }
                    else
                    {
                        if (brokenGroups[group] != groupLength) return false;
                        group++;
                        groupLength = 1;
                    }
                    prev = pattern[i];
                }
                if (brokenGroups[group] != groupLength) return false;
                return true;
            }
        }
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass input filename as 1st parameter");
                return;
            }
            var lines = File.ReadAllLines(args[0]).Select(l => new Line(l)).ToList();
            var counts = lines.Select(l => l.CountWays()).ToList();
            Console.WriteLine("{0}", counts.Sum());
        }
    }
}
