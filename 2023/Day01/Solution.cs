namespace AdventOfCode2023.Day01
{
    internal class Solution
    {
        public static void Solve()
        {
            int sum = 0;
            foreach(string line in File.ReadLines(@"C:/input.txt"))
                sum += GetDigits(line);
            
            Console.WriteLine(sum);
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
}
