namespace AdventOfCode2023.Day07
{
    internal class Solution
    {
        private class Hand(string cards, int bet)
        {
            public readonly string cards = cards;
            public readonly int bet = bet;
            public int type = 0;

            public override string ToString()
            {
                return $"Hand: {cards} Type: {type} Bet: {bet}";
            }
        }

        private static bool useJoker = false;

        public static string Solve()
        {
            string[] lines = File.ReadLines(@"Day07/input.txt").ToArray();
            Hand[] hands = ParseHands(lines);
            
            useJoker = false;
            SolveHandTypes(hands);
            Array.Sort(hands, CompareHands);
            int p1 = 0;
            for (int i = 0; i < hands.Length; i++)
            {
                int bet = hands[i].bet * (i + 1);
                p1 += hands[i].bet * (i + 1);
                Console.WriteLine($"{hands[i]} * {i + 1} - Bet: {bet}");
            }

            useJoker = true;
            SolveHandTypes(hands);
            Array.Sort(hands, CompareHands);
            int p2 = 0;
            for (int i = 0; i < hands.Length; i++)
            {
                int bet = hands[i].bet * (i + 1);
                p2 += hands[i].bet * (i + 1);
                Console.WriteLine($"{hands[i]} * {i + 1} - Bet: {bet}");
            }

            string result = $"Part 1: {p1} | Part 2: {p2}";
            return result;
        }

        private static Hand[] ParseHands(string[] lines)
        {
            Hand[] hands = new Hand[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(' ');
                int bet = int.Parse(data[1]);
                hands[i] = new Hand(data[0], bet);
            }
            return hands;
        }

        private static void SolveHandTypes(Hand[] hands)
        {
            foreach (Hand hand in hands)
            {
                char[] cards = hand.cards.ToCharArray();
                IEnumerable<IGrouping<char, char>> groups = cards.GroupBy(c => c);
                int groupCount = groups.Count();
                int jokerCount = 0;
                if (useJoker)
                {
                    foreach (IGrouping<char, char> group in groups)
                    {
                        if (group.Key == 'J')
                        {
                            jokerCount = group.Count();
                            break;
                        }
                    }
                }

                switch (groupCount)
                {
                    case 1:
                    case 2 when jokerCount > 0:
                        hand.type = 6; //Five of a kind
                        break;

                    case 2:
                        foreach(IGrouping<char, char> group in groups)
                        {
                            int count = group.Count() + 1;
                            if (count > hand.type)
                                hand.type = count; //5 = Four of a kind or 4 = Full house
                        }
                        break;

                    case 3:
                        foreach (IGrouping<char, char> group in groups)
                        {
                            int count = group.Count();
                            if (count > hand.type)
                                hand.type = count; //3 = Three of a kind or 2 = Two pairs
                        }

                        if (jokerCount > 0)
                        {
                            if (hand.type == 3 || jokerCount == 2)
                                hand.type = 5;
                            else
                                hand.type = 4;
                        }
                        
                        break;

                    case 4:
                        hand.type = 1; //One pair
                        if (jokerCount > 0)
                            hand.type = 3;
                        break;

                    case 5:
                        hand.type = 0; //High card
                        if (jokerCount > 0)
                            hand.type = 1;
                        break;
                }
            }
        }

        private static int CompareHands(Hand a, Hand b)
        {
            if (a.type < b.type) return -1;
            if (a.type > b.type) return 1;

            for(int i = 0; i < 5; i++)
            {
                int aValue = GetNumericalValue(a.cards[i]);
                int bValue = GetNumericalValue(b.cards[i]);
                if (aValue < bValue) return -1;
                if (aValue > bValue) return 1;
            }

            return 0;
        }

        private static int GetNumericalValue(char c)
        {
            if (char.IsDigit(c))
                return c - '0';

            switch(c)
            {
                case 'T':
                    return 10;
                case 'J':
                    return useJoker ? 1 : 11;
                case 'Q':
                    return 12;
                case 'K':
                    return 13;
                case 'A':
                    return 14;
                default:
                    return -1;
            }
        }
    }
}
