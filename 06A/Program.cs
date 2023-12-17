namespace _06A
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

            int[] times = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            int[] records = lines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

            int chances = 1;
            for (int c = 0; c < times.Length; c++)
            {
                /* speed == pulseDuration
                 * travelTime := time - pulseDuration
                 * travelDistance := travelTime * pulseDuration == (time - pulseDuration) * pulseDuration
                 * travelDistance > record := (time - pulseDuration) * pulseDuration - record > 0
                 * 
                 * Parabolic
                 * -(pulseDuration^2) + time*(pulseDuration) - record > 0
                 * 
                 * Solutions
                 * minPulseDuration = ceil((-time+sqrt(time^2-4*record))/-2)
                 * maxPulseDuration = floow((-time-sqrt(time^2-4*record))/-2)
                 */
                double disc = Math.Sqrt(Math.Pow(times[c], 2) - 4 * records[c]);
                int minPulseDuration = (int)Math.Ceiling((-times[c] + disc) / -2);
                int maxPulseDuration = (int)Math.Floor((-times[c] - disc) / -2);
                chances *= maxPulseDuration - minPulseDuration + 1;
            }
            Console.WriteLine("{0}", chances);
        }
    }
}
