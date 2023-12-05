namespace AdventOfCode2023.Day05
{
    internal class Solution
    {
        private class Map()
        {
            public readonly List<long> destinations = new();
            public readonly List<long> sources = new();
            public readonly List<long> ranges = new();

            public long GetDestination(long value)
            {
                for (int i = 0; i < sources.Count; i++)
                {
                    long source = sources[i];
                    if (value < source || value >= source + ranges[i])
                        continue;

                    return value - source + destinations[i];
                }

                return value;
            }
        }

        private static long[] seeds;
        private readonly static List<Map> maps = new();

        public static string Solve()
        {
            string[] lines = File.ReadLines(@"Day05/input.txt").ToArray();
            ParseSeedsAndMaps(lines);
            long p1 = long.MaxValue;

            foreach (long seed in seeds)
            {
                long value = seed;
                foreach (Map map in maps)
                    value = map.GetDestination(value);

                if (value < p1)
                    p1 = value;
            }

            int p2 = 0;
            string result = $"Part 1: {p1} | Part 2: {p2}";
            return result;
        }

        private static void ParseSeedsAndMaps(string[] lines)
        {
            maps.Clear();
            string[] seedsStr = lines[0].Remove(0, 7).Split(' ');
            seeds = new long[seedsStr.Length];
            for (int i = 0; i < seedsStr.Length; i++)
                seeds[i] = long.Parse(seedsStr[i]);

            Map map = null;
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Length == 0)
                    continue;

                if (char.IsLetter(line[0]))
                {
                    if (map != null)
                        maps.Add(map);
                    map = new Map();
                }
                else
                {
                    string[] values = line.Split(' ');
                    map.destinations.Add(long.Parse(values[0]));
                    map.sources.Add(long.Parse(values[1]));
                    map.ranges.Add(long.Parse(values[2]));
                }
            }

            maps.Add(map);
        }
    }
}
