using System.Runtime.ExceptionServices;

namespace _07A
{
    internal class Program
    {
        private class Hand : IComparable<Hand>
        {
            private enum Type
            {
                HighCard = 6,
                OnePair = 5,
                TwoPair = 4,
                ThreeOfAKind = 3,
                FullHouse = 2,
                FourOfAKind = 1,
                FiveOfAKind = 0
            };

            public int bet;
            private char[] hand;
            private Type type;
            private const string rank = "AKQJT98765432";

            public Hand(string hand, int bet)
            {
                if (hand.Length != 5) throw new ArgumentException("Bad hand");
                this.bet = bet;
                this.hand = hand.ToCharArray();
                var groups = hand.GroupBy(c => c).ToArray();
                switch (groups.Length)
                {
                    case 1:
                        type = Type.FiveOfAKind;
                        break;
                    case 2:
                        type = groups.Count(g => g.Count() == 4) == 1
                            ? Type.FourOfAKind
                            : Type.FullHouse;
                        break;
                    case 3:
                        type = groups.Count(g => g.Count() == 3) == 1
                            ? Type.ThreeOfAKind
                            : Type.TwoPair;
                        break;
                    case 4:
                        type = Type.OnePair;
                        break;
                    case 5:
                        type = Type.HighCard;
                        break;
                }
            }

            public int CompareTo(Hand? other)
            {
                if (other == null) return 1;
                int typeCompare = ((int)type).CompareTo((int)other.type);
                if (typeCompare != 0) return typeCompare;
                for (int c = 0; c < 5; c++)
                {
                    int pt = rank.IndexOf(hand[c]);
                    int po = rank.IndexOf(other.hand[c]);
                    if (pt < po) return -1;
                    if (pt > po) return 1;
                }
                return 0;
            }
        }
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass input filename as 1st parameter");
                return;
            }
            string[] lines = File.ReadAllLines(args[0]);
            int pos = 1;
            int winning = 0;
            foreach (Hand hand in lines.Select(l => { string[] h = l.Split(' ', StringSplitOptions.RemoveEmptyEntries); return new Hand(h[0], int.Parse(h[1])); }).OrderByDescending(h => h))
            {
                winning += pos++ * hand.bet;
            }
            Console.WriteLine("{0}", winning);
        }
    }
}
