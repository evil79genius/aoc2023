using System.ComponentModel;
using System.Threading.Tasks.Dataflow;

namespace _10A
{
    internal class Program
    {
        private enum Direction
        {
            //     NESW
            N  = 0b01000,
            E  = 0b00100,
            S  = 0b00010,
            W  = 0b00001,
            G  = 0b00000,
            NS = 0b01010,
            EW = 0b00101,
            NE = 0b01100,
            NW = 0b01001,
            SW = 0b00011,
            SE = 0b00110,
            ST = 0b10000,
            A  = 0b01111
        }
        private enum Note
        {
            G  = '.',
            NS = '|',
            EW = '-',
            NE = 'L',
            NW = 'J',
            SW = '7',
            SE = 'F',
            ST = 'S'
        }
        private class Tile
        {
            public Tile(char c, int x, int y)
            {
                direction = Enum.Parse<Direction>(((Note)c).ToString());
                this.x = x; this.y = y;
                if (direction != Direction.G && direction != Direction.ST)
                {
                    length = 1;
                }
                otherEnd = this;
            }
            public Direction direction;
            public int x, y;
            public int? length;
            public bool endpoint;
            public Tile? otherEnd;

            public bool TouchesNorth(Tile north)
            {
                return ((north.direction & Direction.S) != 0) && ((direction & Direction.N) != 0);
            }
            public bool TouchesWest(Tile west)
            {
                return ((west.direction & Direction.E) != 0) && ((direction & Direction.W) != 0);
            }
            public void Join(Tile other)
            {
                Tile aoe = otherEnd;
                otherEnd.otherEnd = other.otherEnd;
                other.otherEnd.otherEnd = aoe;

                length += other.length;
                otherEnd.length = length;
                other.otherEnd.length = length;
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
            int width = lines[0].Length;
            int height = lines.Length;
            Tile[][] tiles = lines.Select((l, y) => l.ToCharArray().Select((c, x) => new Tile(c, x, y)).ToArray()).ToArray();
            int startR = -1, startC = -1;
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    if (tiles[r][c].direction == Direction.ST)
                    {
                        startR = r;
                        startC = c;
                    }
                    if (c > 0 && tiles[r][c].TouchesWest(tiles[r][c - 1]))
                    {
                        tiles[r][c].Join(tiles[r][c - 1]);
                    }
                    if (r > 0 && tiles[r][c].TouchesNorth(tiles[r - 1][c]))
                    {
                        tiles[r][c].Join(tiles[r - 1][c]);
                    }
                }
            }
            Tile start = tiles[startR][startC];
            start.direction = Direction.A;
            Tile[] neighbors = [
                tiles[startR - 1][startC],
                tiles[startR][startC + 1],
                tiles[startR + 1][startC],
                tiles[startR][startC - 1]
            ];
            tiles[startR - 1][startC].endpoint = start.TouchesNorth(tiles[startR - 1][startC]);
            tiles[startR][startC + 1].endpoint = tiles[startR][startC + 1].TouchesWest(start);
            tiles[startR + 1][startC].endpoint = tiles[startR + 1][startC].TouchesNorth(start);
            tiles[startR][startC - 1].endpoint = start.TouchesWest(tiles[startR][startC - 1]);
            foreach(Tile tile in neighbors.Where(t => t.endpoint))
            {
                if (neighbors.Contains(tile.otherEnd) && tile.length > 1)
                {
                    Console.WriteLine("{0}", (tile.length + 1) / 2);
                    break;
                }
            }
        }
    }
}
