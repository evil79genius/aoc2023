using System.Text.RegularExpressions;

namespace _03A
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
            Regex symbols = new Regex(@"[^.^\d]");
            HashSet<int>[] symbolsMap = lines.Select(line => symbols.Matches(line).Select(m => m.Index).ToHashSet()).ToArray();
            HashSet<int>[] placesMap = new HashSet<int>[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                HashSet<int> places = symbolsMap[i].ToHashSet(); // Clone the HashSet
                if (i > 0)
                {
                    foreach(int place in symbolsMap[i-1])
                    {
                        places.Add(place);
                    }
                }
                if ((i+1) < lines.Length)
                {
                    foreach(int place in symbolsMap[i+1])
                    {
                        places.Add(place);
                    }
                }
                placesMap[i] = places;
            }
            Regex pieces = new Regex(@"[\d]+");
            int sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (Match piece in pieces.Matches(lines[i]))
                {
                    if (Enumerable.Range(piece.Index-1, piece.Value.Length+2).Any(p => placesMap[i].Contains(p)))
                    {
                        int validPiece = int.Parse(piece.Value);
                        sum += validPiece;
                    }
                }
            }
            Console.WriteLine("{0}", sum);
        }
    }
}
