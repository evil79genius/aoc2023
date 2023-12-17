using System.Text.RegularExpressions;

namespace _01B
{
    enum Numbers {
        zero = 0, one = 1, two = 2, three = 3, four = 4, five = 5, six = 6, seven = 7, eight = 8, nine = 9
    };

    internal class Program
    {
        static int Parse(string number)
        {
            int result = 0;
            if (int.TryParse(number, out result)) return result;
            return (int)Enum.Parse<Numbers>(number);
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass input filename as 1st parameter");
                return;
            }
            string[] lines = File.ReadAllLines(args[0]);
            int sum = 0;
            Regex digit = new Regex("[0-9]|" + string.Join('|', Enum.GetValues<Numbers>()));
            foreach (string line in lines)
            {
                var mc = digit.Matches(line);
                int value = (Parse(mc.First().Value) * 10) + Parse(mc.Last().Value);
                sum += value;
            }
            Console.WriteLine("{0}", sum);
        }
    }
}
