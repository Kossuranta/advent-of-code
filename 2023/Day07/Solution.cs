using System;

namespace AdventOfCode2023.Day07
{
    internal class Solution
    {
        private class Hand(string hand, int[] cards, int bet)
        {
            public readonly string hand = hand;
            public readonly int[] cards = cards;
            public readonly int bet = bet;
            public int type = 0;

            public override string ToString()
            {
                return $"Hand: {hand} Cards: {cards[0]}-{cards[1]}-{cards[2]}-{cards[3]}-{cards[4]} Type: {type} Bet: {bet}";
            }
        }

        public static string Solve()
        {
            string[] lines = File.ReadLines(@"Day07/input.txt").ToArray();
            Hand[] hands = ParseHands(lines);
            SolveHandTypes(hands);
            Array.Sort(hands, CompareHands);

            long p1 = 0;
            for (int i = 0; i < hands.Length; i++)
            {
                long bet = hands[i].bet * (i + 1);
                p1 += hands[i].bet * (i + 1);
                Console.WriteLine($"{hands[i]} * {i + 1} - Bet: {bet}");
            }

            int p2 = 0;
            string result = $"Part 1: {p1} | Part 2: {p2}";
            return result;
        }

        private static Hand[] ParseHands(string[] lines)
        {
            Hand[] hands = new Hand[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(' ');
                int[] cards = new int[5];
                for (int j = 0; j < 5; j++)
                    cards[j] = GetNumericalValue(data[0][j]);
                int bet = int.Parse(data[1]);

                hands[i] = new Hand(data[0], cards, bet);
            }
            return hands;
        }

        private static void SolveHandTypes(Hand[] hands)
        {
            foreach (Hand hand in hands)
            {
                IEnumerable<IGrouping<int, int>> groups = hand.cards.GroupBy(c => c);
                int groupCount = groups.Count();
                switch (groupCount)
                {
                    case 1:
                        hand.type = 6; //Five of a kind
                        break;

                    case 2:
                        foreach(IGrouping<int, int> group in groups)
                        {
                            int count = group.Count() + 1;
                            if (count > hand.type)
                                hand.type = count; //5 = Four of a kind or 4 = Full house
                        }
                        break;

                    case 3:
                        foreach (IGrouping<int, int> group in groups)
                        {
                            int count = group.Count();
                            if (count > hand.type)
                                hand.type = count; //3 = Three of a kind or 2 = Two pairs
                        }
                        break;

                    case 4:
                        hand.type = 1; //One pair
                        break;

                    case 5:
                        hand.type = 0; //High card
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
                if (a.cards[i] < b.cards[i]) return -1;
                if (a.cards[i] > b.cards[i]) return 1;
            }

            return 0;
        }

        private static int GetNumericalValue(char c)
        {
            if (char.IsDigit(c))
                return c - '0';

            return c switch
            {
                'T' => 10,
                'J' => 11,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => -1
            };
        }
    }
}
