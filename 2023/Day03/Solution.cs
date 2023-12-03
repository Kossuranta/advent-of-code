using System.Diagnostics;

namespace AdventOfCode2023.Day03
{
    internal class Solution
    {
        private static readonly Dictionary<string, List<int>> gears = new();

        public static string Solve()
        {
            string[] lines = File.ReadLines(@"Day03/input.txt").ToArray();
            int p1 = 0;
            for (int i = 0; i < lines.Length; i++)
                p1 += CountLineSum(lines, i);

            int p2 = CountGearSum();

            string result = $"Part 1: {p1} | Part 2: {p2}";
            return result;
        }

        public static int CountLineSum(string[] lines, int lineIndex)
        {
            int result = 0;
            string valueStr = "";
            bool isValidValue = false;
            string line = lines[lineIndex];
            string gearID = "";
            for (int charIndex = 0; charIndex < line.Length; charIndex++)
            {
                char c = line[charIndex];
                if (!char.IsDigit(c))
                {
                    TryAddResult();

                    isValidValue = false;
                    valueStr = "";
                    gearID = "";
                    continue;
                }

                valueStr += c;

                if (!isValidValue)
                {
                    isValidValue = IsValidValue(lines, lineIndex, charIndex, out string symbolID);
                    if (!string.IsNullOrEmpty(symbolID))
                        gearID = symbolID;
                }
            }

            TryAddResult();

            return result;

            void TryAddResult()
            {
                if (!isValidValue || string.IsNullOrEmpty(valueStr))
                    return;

                int value = int.Parse(valueStr);
                result += value;
                if (!string.IsNullOrEmpty(gearID))
                {
                    if (!gears.ContainsKey(gearID))
                        gears.Add(gearID, new List<int>(2));
                    gears[gearID].Add(value);
                }
            }
        }

        public static bool IsValidValue(string[] lines, int lineIndex, int charIndex, out string symbolID)
        {
            symbolID = "";
            bool symbolFound = false;
            for (int li = lineIndex - 1; li < lineIndex + 2; li++)
            {
                if (li < 0 || li >= lines.Length)
                    continue;

                string line = lines[li];
                for (int ci = charIndex - 1; ci < charIndex + 2; ci++)
                {
                    if (ci < 0 || ci >= line.Length)
                        continue;

                    char c = line[ci];

                    if (!char.IsDigit(c) && c != '.')
                    {
                        if (c == '*')
                            symbolID = $"L{li}C{ci}";
                        
                        symbolFound = true;
                    }
                }
            }

            return symbolFound;
        }

        public static int CountGearSum()
        {
            int sum = 0;
            foreach((string _, List<int> values) in gears)
            {
                if (values.Count != 2)
                    continue;

                sum += values[0] * values[1];
            }

            return sum;
        }
    }
}
