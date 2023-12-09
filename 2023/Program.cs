while (true)
{
    Console.Write("Enter day [1-25] to solve: ");
    string? answer = Console.ReadLine();
    if (int.TryParse(answer, out int day))
    {
        if (day < 1 || day > 25)
        {
            Console.WriteLine($"Input needs to be between 1 and 25!");
            continue;
        }

        Type? type = Type.GetType($"AdventOfCode2023.Day{day:D2}.Solution");
        System.Reflection.MethodInfo? method = type?.GetMethod("Solve");
        if (method == null)
        {
            Console.WriteLine($"Day {day} hasn't been solved yet!");
        }
        else if (!File.Exists(@$"Day{day:D2}/input.txt"))
        {
            Console.WriteLine($"Couldn't find /Day{day:D2}/input.txt!");
        }
        else
        {
            string result = (string)method.Invoke(null, null);
            Console.WriteLine(result);
        }
    }
}