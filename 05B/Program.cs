using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _05B
{
    internal class Program
    {
        private class Map
        {
            public long To;
            public long From;
            public long Range;
        }
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass input filename as 1st parameter");
                return;
            }
            string[] lines = File.ReadAllLines(args[0]);

            string sources = lines[0].Split(':')[1];
            List<(long, long)> source = new Regex(@"([\d]+) ([\d]+)").Matches(sources).Select(m => (long.Parse(m.Groups[1].Value), long.Parse(m.Groups[2].Value))).ToList();

            int curLine = 1;
            while (curLine < lines.Length)
            {
                List<Map> map = new List<Map>();
                while (++curLine < lines.Length)
                {
                    if (lines[curLine].EndsWith("map:")) continue;
                    if (lines[curLine].Length == 0) break;
                    long[] mapLine = lines[curLine].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
                    map.Add(new Map {
                        To = mapLine[0],
                        From = mapLine[1],
                        Range = mapLine[2]
                    });
                }
                List<(long, long)> dest = new List<(long, long)>();
                while (source.Any())
                {
                    (long start, long range) = source.First();
                    source.RemoveAt(0);
                    while (range > 0)
                    {
                        Map? entry = map.Where(m => m.From < (start + range) && (m.From + m.Range) > start).OrderBy(m => m.From).FirstOrDefault();
                        if (entry != null)
                        {
                            long mapTo = entry.To;
                            long mapFrom = entry.From;
                            long mapRange = entry.Range;
                            if (start < mapFrom)
                            {
                                // Unmapped portion at the beginning
                                dest.Add((start, mapFrom - start));
                                range -= mapFrom - start;
                                start = mapFrom;
                            }
                            if (mapFrom < start)
                            {
                                // Move mapping ahead
                                mapRange -= start - mapFrom;
                                mapTo += start - mapFrom;
                            }
                            if (mapRange < range)
                            {
                                // Not enough to map whole range
                                dest.Add((mapTo, mapRange));
                                start += mapRange;
                                range -= mapRange;
                            }
                            else
                            {
                                // Map whole range
                                dest.Add((mapTo, range));
                                range = 0;
                            }
                        }
                        else
                        {
                            // No overlapping map entry
                            dest.Add((start, range));
                            range = 0;
                        }
                    }
                }
                source = dest;
            }
            Console.WriteLine("{0}", source.Select(s => s.Item1).Min());
        }
    }
}
