namespace DayTwo
{
    internal class Program
    {
        static void Main()
        {
            PartTwo();
        }

        private static void PartTwo()
        {
            var gameDetails = GetGameDetails();
            int powerSum = 0;
            for (int i = 0; i < gameDetails.Length; i++)
            {
                var gameDetail = gameDetails[i];

                powerSum +=  gameDetail.MaxRed * gameDetail.MaxGreen * gameDetail.MaxBlue;
            }

            Console.WriteLine("Power Sum: " + powerSum.ToString());
        }

        private static void PartOne()
        {
            var gameDetails = GetGameDetails();

            int sum = 0;
            for (int i = 0; i < gameDetails.Length; i++)
            {
                var gameDetail = gameDetails[i];

                if (gameDetail.MaxRed > 12 || gameDetail.MaxGreen > 13 || gameDetail.MaxBlue > 14)
                {
                    // skipping
                    Console.WriteLine("Skipping gameID: " + gameDetail.GameId.ToString());
                    continue;
                }

                // or i instead of gameID since it really is 1-100
                sum += gameDetail.GameId;
            }

            //var constrainedSum = gameDetails.Where(g => g.MaxRed <= 12 && g.MaxGreen <= 13 && g.MaxBlue <= 14).Sum(g => g.GameId);
            Console.WriteLine("Game ID Sum: " + sum.ToString());
        }

        private static GameDetails[] GetGameDetails()
        {
            var games = GetPuzzleInput();

            var gameDetails = new GameDetails[games.Length];
            for (int i = 0; i < games.Length; i++)
            {
                var game = games[i];

                // Get rounds
                var gameDetail = game.Split(':');
                var gameId = int.Parse(gameDetail[0].Split(' ')[1]);
                var rounds = gameDetail[1].Split(';');

                int maxRed = 0;
                int maxGreen = 0;
                int maxBlue = 0;

                for (int j = 0; j < rounds.Length; j++)
                {
                    var draws = rounds[j].Split(",");
                    foreach (var draw in draws)
                    {
                        var drawDetails = draw.Trim().Split(' ');
                        var amount = int.Parse(drawDetails[0]);
                        var color = drawDetails[1];
                        if (color.Equals("red") && (maxRed == 0 || amount > maxRed))
                        {
                            maxRed = amount;
                        }
                        else if (color.Equals("green") && (maxGreen == 0 || amount > maxGreen))
                        {
                            maxGreen = amount;
                        }
                        else if (color.Equals("blue") && (maxBlue == 0 || amount > maxBlue))
                        {
                            maxBlue = amount;
                        }
                    }
                }

                Console.WriteLine("| GameID: {0, 3} | Rounds: {1, 1} | Red: {2, 2} | Green: {3, 2} | Blue: {4, 2} |",
                    gameId, rounds.Length, maxRed, maxGreen, maxBlue);
                gameDetails[i] = new GameDetails(gameId, rounds.Length, maxRed, maxGreen, maxBlue);
            }

            return gameDetails;
        }

        private static string[] GetPuzzleInput()
        {
            return File.ReadAllLines(".\\games.txt");
        }

        internal struct GameDetails(int gameId, int roundCount, int red, int green, int blue)
        {
            public int GameId { get; private set; } = gameId;
            public int Rounds { get; private set; } = roundCount;
            public int MaxRed { get; private set; } = red;
            public int MaxGreen { get; private set; } = green;
            public int MaxBlue { get; private set; } = blue;
        }
    }
}
