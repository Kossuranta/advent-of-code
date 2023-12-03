using System.Diagnostics;

namespace AdventOfCode2023.Day03
{
    internal class SolutionPart1
    {
        public static string Solve()
        {
            string[] lines = File.ReadLines(@"Day03/input.txt").ToArray();
            int sum = 0;
            for (int i = 0; i < lines.Length; i++)
                sum += CountLineSum(lines, i);

            return sum.ToString();
        }

        public static int CountLineSum(string[] lines, int lineIndex)
        {
            int result = 0;
            string valueStr = "";
            bool isValidValue = false;
            string line = lines[lineIndex];
            for (int charIndex = 0; charIndex < line.Length; charIndex++)
            {
                char c = line[charIndex];
                if (!char.IsDigit(c))
                {
                    if (isValidValue && !string.IsNullOrEmpty(valueStr))
                        result += int.Parse(valueStr);

                    isValidValue = false;
                    valueStr = "";
                    continue;
                }

                valueStr += c;

                if (!isValidValue)
                    isValidValue = IsValidValue(lines, lineIndex, charIndex);
            }

            if (isValidValue && !string.IsNullOrEmpty(valueStr))
                result += int.Parse(valueStr);

            return result;
        }

        public static bool IsValidValue(string[] lines, int lineIndex, int charIndex)
        {
            for(int li = lineIndex - 1; li < lineIndex + 2; li++)
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
                        return true;
                }
            }

            return false;
        }
    }
}
