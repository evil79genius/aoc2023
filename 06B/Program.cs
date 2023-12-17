namespace _06B
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

            long time = long.Parse(lines[0].Split(':')[1].Replace(" ", null));
            long record = long.Parse(lines[1].Split(':')[1].Replace(" ", null));

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
            double disc = Math.Sqrt(Math.Pow(time, 2) - 4 * record);
            long minPulseDuration = (long)Math.Ceiling((-time + disc) / -2);
            long maxPulseDuration = (long)Math.Floor((-time - disc) / -2);
            Console.WriteLine("{0}", maxPulseDuration - minPulseDuration + 1);
        }
    }
}
