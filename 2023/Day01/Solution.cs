using System.Diagnostics;

namespace AdventOfCode2023.Day01
{
    internal class SolutionPart1
    {
        public static string Solve()
        {
            int sum = 0;
            foreach(string line in File.ReadLines(@"Day01/input.txt"))
                sum += GetDigits(line);

            return sum.ToString();
        }

        private static int GetDigits(string line)
        {
            int first = -1;
            int second = -1;
            for(int i = 0; i < line.Length; i++)
            {
                string c = line[i].ToString();
                if (int.TryParse(c, out int value))
                {
                    if (first < 0)
                        first = value;
                    second = value;
                }
            }
            
            return first * 10 + second;
        }
    }

    internal class SolutionPart2
    {
        private static readonly (string, int)[] strToValuePairs =
        [
            ("one", 1),
            ("two", 2),
            ("three", 3),
            ("four", 4),
            ("five", 5),
            ("six", 6),
            ("seven", 7),
            ("eight", 8),
            ("nine", 9),
        ];

        public static string Solve()
        {
            int sum = 0;
            foreach (string line in File.ReadLines(@"Day01/input.txt"))
                sum += GetDigits(line);

            return sum.ToString();
        }

        private static int GetDigits(string line)
        {
            int first = -1;
            int firstIndex = int.MaxValue;
            int second = -1;
            int secondIndex = -1;

            foreach((string str, int value) in strToValuePairs)
            {
                SetFirst(str, value);
                SetFirst(value.ToString(), value);
                SetSecond(str, value);
                SetSecond(value.ToString(), value);
            }

            return first * 10 + second;

            void SetFirst(string str, int value)
            {
                int index = line.IndexOf(str);
                if (index >= 0 && index < firstIndex)
                {
                    first = value;
                    firstIndex = index;
                }
            }

            void SetSecond(string str, int value)
            {
                int index = line.LastIndexOf(str);
                if (index >= 0 && index > secondIndex)
                {
                    second = value;
                    secondIndex = index;
                }
            }
        }
    }
}
