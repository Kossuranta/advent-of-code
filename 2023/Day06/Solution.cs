namespace AdventOfCode2023.Day06
{
    internal class Solution
    {
        private static (int time, int distance)[] races;

        public static string Solve()
        {
            string[] lines = File.ReadLines(@"Day06/input.txt").ToArray();
            ParseInput(lines);
            int p1 = 1;
            foreach ((int time, int distance) in races)
            {
                int waysToWin = 0;
                for(int holdTime = 1;  holdTime < time; holdTime++)
                {
                    int dist = holdTime * (time - holdTime);
                    if (dist > distance)
                        waysToWin++;
                }

                p1 *= waysToWin;
            }

            int p2 = 0;
            string result = $"Part 1: {p1} | Part 2: {p2}";
            return result;
        }

        private static void ParseInput(string[] lines)
        {
            string[] timesStr = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] distancesStr = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            races = new (int, int)[timesStr.Length - 1];
            for(int i = 1; i < timesStr.Length; i++)
                races[i - 1] = (int.Parse(timesStr[i]), int.Parse(distancesStr[i]));
        }
    }
}
