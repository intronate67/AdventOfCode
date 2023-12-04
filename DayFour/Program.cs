namespace DayFour
{
    internal class Program
    {
        static void Main()
        {
            PartOne();
        }

        private static void PartOne()
        {
            var scratchCards = GetPuzzleInput();

            var pointsSum = scratchCards.Select(GetPointsFromCard).Sum();

            Console.WriteLine("Point Sum: " + pointsSum.ToString());
        }

        private static int GetPointsFromCard(Scratchcard card)
        {
            int points = 0;
            foreach(var num in card.GivenNumbers)
            {
                if (card.WinningNumbers.Contains(num))
                {
                    if(points == 0)
                    {
                        points = 1;
                    }
                    else
                    {
                        points *= 2;
                    }
                }
            }

            return points;
        }

        private static Scratchcard[] GetPuzzleInput()
        {
            return File.ReadAllLines(".\\cards.txt").Select(c => new Scratchcard(c)).ToArray();
        }

        public class Scratchcard
        {
            public int Id { get; private set; }
            // 10
            public HashSet<int> WinningNumbers { get; private set; }
            // 25
            public HashSet<int> GivenNumbers { get; private set; }

            public Scratchcard(string card)
            {
                var cardInfo = card.Split(':');
                var cardId = string.Join("", cardInfo[0].Where(char.IsDigit));

                Id = int.Parse(cardId);

                var numbers = cardInfo[1].Trim().Split('|');

                WinningNumbers = ExtractNumbers(numbers[0].Trim());
                GivenNumbers = ExtractNumbers(numbers[1].Trim());
            }

            private static HashSet<int> ExtractNumbers(string numberString)
            {
                var numbers = new HashSet<int>();
                string current = "";

                for (int i = 0; i < numberString.Length; i++)
                {
                    var character = numberString[i];
                    if (char.IsDigit(character))
                    {
                        current += character;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(current))
                        {
                            numbers.Add(int.Parse(current));

                            current = "";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(current))
                {
                    numbers.Add(int.Parse(current));
                }

                return numbers;
            }
        }
    }
}
