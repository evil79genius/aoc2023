using System.Text.RegularExpressions;

namespace _11A
{
    internal class Program
    {
        private class Star
        {
            public Star(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;
        }
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass input filename as 1st parameter");
                return;
            }
            string[] lines = File.ReadAllLines(args[0]);
            Regex starRegex = new Regex("#");
            var stars = lines.SelectMany((s, r) => starRegex.Matches(s).Select(m => new Star(m.Index, r))).ToArray();

            int cntEmpty;

            var columns = stars.Select(s => s.x).Distinct();
            cntEmpty = 0;
            var columnExpansion = Enumerable.Range(0, columns.Max() + 1).Select(c => columns.Contains(c) ? cntEmpty : ++cntEmpty).ToArray();
            var rows = stars.Select(s => s.y).Distinct();
            cntEmpty = 0;
            var rowExpansion = Enumerable.Range(0, rows.Max() + 1).Select(r => rows.Contains(r) ? cntEmpty : ++cntEmpty).ToArray();
            foreach (Star star in stars)
            {
                star.x += columnExpansion[star.x];
                star.y += rowExpansion[star.y];
            }
            long distances = 0;
            for (int i = 0; i < stars.Length; i++)
            {
                for (int j = i + 1; j < stars.Length; j++)
                {
                    distances += Math.Abs(stars[i].x - stars[j].x) + Math.Abs(stars[i].y - stars[j].y);
                }
            }
            Console.WriteLine("{0}", distances);
        }
    }
}
