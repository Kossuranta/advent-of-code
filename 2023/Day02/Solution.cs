namespace AdventOfCode2023.Day02
{
    internal class SolutionPart1
    {
        private static readonly Dictionary<string, int> limits = new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 },
        };

        public static string Solve()
        {
            string[] lines = File.ReadLines(@"Day02/input.txt").ToArray();
            int sum = 0;
            for(int index = 0; index < lines.Length; index++)
            {
                string line = lines[index];
                int gameIndex = index + 1;
                if (IsGamePossible(line))
                    sum += gameIndex;
            }
            return sum.ToString();
        }

        public static bool IsGamePossible(string line)
        {
            line = line.Split(':').Last();
            line = line.Replace(" ", string.Empty);
            string[] inputs = line.Split(';');
            foreach (string input in inputs)
            {
                string[] cubes = input.Split(",");
                foreach (string cube in cubes)
                {
                    (string color, int amount) = ParseColorAmountPair(cube);
                    if (amount > limits[color])
                        return false;
                }
            }

            return true;
        }

        public static (string, int) ParseColorAmountPair(string line)
        {
            foreach((string color, int _) in limits)
            {
                if (line.Contains(color))
                {
                    string amount = line.Replace(color, string.Empty);
                    return (color, int.Parse(amount));
                }
            }

            return (string.Empty, 0);
        }
    }
}
