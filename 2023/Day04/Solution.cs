namespace AdventOfCode2023.Day04
{
    internal class Solution
    {
        private class Card
        {
            public readonly int id;
            public readonly int[] winning;
            public readonly int[] numbers;
            public int count = 1;

            public Card(int id, int[] winning, int[] numbers)
            {
                this.id = id;
                this.winning = winning;
                this.numbers = numbers;
            }
        }

        private static readonly List<Card> cards = new();

        public static string Solve()
        {
            string[] lines = File.ReadLines(@"Day04/input.txt").ToArray();
            ParseCards(lines);
            int p1 = CountPoints();
            int p2 = CountCards();
            string result = $"Part 1: {p1} | Part 2: {p2}";
            return result;
        }

        private static void ParseCards(string[] lines)
        {
            cards.Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = string.Join(' ', lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries));
                string[] data = line.Split(": ");
                string[] numbersData = data[1].Split(" | ");

                int cardId = int.Parse(data[0].Remove(0, 5));
                int[] winning = IntParseArray(numbersData[0].Split(' '));
                int[] numbers = IntParseArray(numbersData[1].Split(' '));

                Card card = new Card(cardId, winning, numbers);
                cards.Add(card);
            }
        }

        private static int[] IntParseArray(string[] arr)
        {
            int[] result = new int[arr.Length];
            for (int j = 0; j < arr.Length; j++)
                result[j] = int.Parse(arr[j]);
            return result;
        }

        private static int CountPoints()
        {
            int total = 0;
            for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++)
            {
                Card card = cards[cardIndex];
                int winningNumbers = CountWinningNumbers(card);
                if (winningNumbers == 0)
                    continue;

                total += Convert.ToInt32(Math.Pow(2, winningNumbers - 1));

                for(int j = 1; j <= winningNumbers; j++)
                {
                    int copyIndex = cardIndex + j;
                    if (copyIndex >= cards.Count)
                        break;

                    Card copyCard = cards[cardIndex + j];
                    copyCard.count += card.count;
                }
            }
            return total;
        }

        private static int CountWinningNumbers(Card card)
        {
            int winningNumbers = 0;
            foreach(int number in card.numbers)
            {
                if (card.winning.Contains(number))
                    winningNumbers++;
            }
            return winningNumbers;
        }

        private static int CountCards()
        {
            int total = 0;
            foreach (Card card in cards)
                total += card.count;
            return total;
        }
    }
}
