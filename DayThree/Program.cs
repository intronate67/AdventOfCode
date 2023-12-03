namespace DayThree
{
    internal class Program
    {
        private static readonly char[] _specialCharacters = ['*', '#', '/', '%', '=', '&', '-', '+', '$', '@'];

        static void Main()
        {
            PartOne();
            PartTwo();
        }

        private static void PartTwo()
        {
            var lines = GetPuzzleInput();

            var engineMatrix = GetEngineMatrix(lines);

            var gearSum = GetGearRatioSum(engineMatrix);

            Console.WriteLine(gearSum.ToString());
        }

        private static int GetGearRatioSum(char[][] engineMatrix)
        {
            // Couple of assumptions (like assuming max number length of 3 digits) could/should be corrected
            int sum = 0;

            for(int i = 0; i < engineMatrix.Length; i++)
            {
                var row = engineMatrix[i];

                for(int j = 0; j < row.Length; j++)
                {
                    var character = row[j];

                    if (IsGear(character))
                    {
                        var ratios = new List<int>();

                        // Begin 7x3 matrix search centered around our current i and j
                        // Look to the left, only if we're not at the start
                        if (j > 0)
                        {
                            string currentRatio = "";
                            int scanStart = j < 3 ? 0 : j - 3;
                            for(int k = scanStart; k < j; k++)
                            {
                                char scanResult = row[k];
                                if (char.IsDigit(scanResult))
                                {
                                    currentRatio += scanResult;
                                }
                                else
                                {
                                    currentRatio = "";
                                }
                            }

                            if (!string.IsNullOrEmpty(currentRatio) && int.TryParse(currentRatio, out int newRatio))
                            {
                                ratios.Add(newRatio);
                            }
                        }

                        // Look to the right, only if we're not at the end.
                        if(j < row.Length - 1)
                        {
                            string currentRatio = "";
                            int scanEnd = (j + 1 == row.Length - 1) ? row.Length - 1 : j + 3;
                            bool addedRatioFromRight = false;
                            for (int k = j + 1; k <= scanEnd; k++)
                            {
                                char scanResult = row[k];
                                if (char.IsDigit(scanResult))
                                {
                                    currentRatio += scanResult;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            // Full 3 digit number
                            if (!addedRatioFromRight && !string.IsNullOrEmpty(currentRatio) && int.TryParse(currentRatio, out int newRatio))
                            {
                                ratios.Add(newRatio);
                            }
                        }

                        // Look in the row above, if not in the first row already
                        if(i > 0)
                        {
                            string currentRatio = "";
                            char[] rowAbove = engineMatrix[i - 1];

                            int? currentStart = null;
                            int? currentEnd = null;

                            int scanStart = j < 3 ? 0 : j - 3;
                            int scanEnd = j == rowAbove.Length ? rowAbove.Length - 1 : j + 3;

                            for (int k = scanStart; k <= scanEnd; k++)
                            {
                                char scanResult = rowAbove[k];
                                if (char.IsDigit(scanResult))
                                {
                                    currentRatio += scanResult;

                                    if (currentStart is null)
                                    {
                                        currentStart = k;
                                        currentEnd = k;
                                    }
                                    else if (currentEnd is null || k > currentEnd)
                                    {
                                        currentEnd = k;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(currentRatio) && int.TryParse(currentRatio, out int ratio)
                                        && ((currentStart <= j + 1 && currentStart >= j - 1) || (currentEnd <= j + 1 && currentEnd >= j - 1)))
                                    {
                                        ratios.Add(ratio);
                                    }

                                    currentRatio = "";
                                    currentStart = null;
                                    currentEnd = null;
                                }
                            }

                            if (!string.IsNullOrEmpty(currentRatio) && int.TryParse(currentRatio, out int newRatio)
                                && ((currentStart <= j + 1 && currentStart >= j - 1) || (currentEnd <= j + 1 && currentEnd >= j - 1)))
                            {
                                ratios.Add(newRatio);
                            }
                        }

                        // Look in the row below, if not in the bottom row already
                        if(i < engineMatrix.Length - 1)
                        {
                            string currentRatio = "";
                            char[] rowBelow = engineMatrix[i + 1];

                            int? currentStart = null;
                            int? currentEnd = null;

                            int scanStart = j < 3 ? 0 : j - 3;
                            int scanEnd = j == rowBelow.Length ? rowBelow.Length - 1 : j + 3;

                            for (int k = scanStart; k <= scanEnd; k++)
                            {
                                char scanResult = rowBelow[k];
                                if (char.IsDigit(scanResult))
                                {
                                    currentRatio += scanResult;

                                    if (currentStart is null)
                                    {
                                        currentStart = k;
                                        currentEnd = k;
                                    }
                                    else if (currentEnd is null || k > currentEnd)
                                    {
                                        currentEnd = k;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(currentRatio) && int.TryParse(currentRatio, out int ratio)
                                        && ((currentStart <= j + 1 && currentStart >= j - 1) || (currentEnd <= j + 1 && currentEnd >= j - 1)))
                                    {
                                        ratios.Add(ratio);
                                    }

                                    currentRatio = "";
                                    currentStart = null;
                                    currentEnd = null;
                                }
                            }

                            if (!string.IsNullOrEmpty(currentRatio) && int.TryParse(currentRatio, out int newRatio)
                                && ((currentStart <= j + 1 && currentStart >= j - 1) || (currentEnd <= j + 1 && currentEnd >= j - 1)))
                            {
                                ratios.Add(newRatio);
                            }
                        }

                        if(ratios.Count == 2)
                        {
                            sum += ratios[0] * ratios[1];
                        }
                    }
                }
            }

            return sum;
        }

        private static bool IsGear(char c) => c == '*';

        private static void PartOne()
        {
            var lines = GetPuzzleInput();

            var engineMatrix = GetEngineMatrix(lines);

            var partSum = GetPartNumberSum(engineMatrix);

            Console.WriteLine(partSum.ToString());
        }

        private static int GetPartNumberSum(char[][] engineMatrix)
        {
            int sum = 0;

            for (int i = 0; i < engineMatrix.Length; i++)
            {
                var row = engineMatrix[i];

                var current = "";
                int? currentStart = null;
                int? currentEnd = null;

                for (int j = 0; j < row.Length; j++)
                {
                    var character = row[j];

                    if (char.IsDigit(character))
                    {
                        // Find end idx of current num to get range
                        // Use range (start/end idx) to scan surrounding indices for special characters (*/#)
                        current += character;

                        if (currentStart is null)
                        {
                            currentStart = j;
                            currentEnd = j;
                        }
                        else if (currentEnd is null || j > currentEnd)
                        {
                            currentEnd = j;
                        }
                    }

                    if ((j > currentEnd || j == row.Length - 1) && currentStart.HasValue && currentEnd.HasValue
                        && currentEnd >= currentStart && current.Length > 0)
                    {
                        int scanStart = currentStart.Value == 0 ? currentStart.Value : currentStart.Value - 1;
                        int scanEnd = currentEnd == (row.Length - 1) ? row.Length - 1 : currentEnd.Value + 1;

                        // First check to the left then right
                        // Right (current value assigned character)
                        if (j != row.Length - 1 && _specialCharacters.Any(c => c.Equals(character)))
                        {
                            // Character to the right of the number is a special character, add the current string to the sum
                            sum += int.Parse(current);
                        }
                        else if (_specialCharacters.Any(c => c.Equals(row[scanStart])))
                        {
                            // Character to the left of the number is a special character, add the current string to the sum
                            sum += int.Parse(current);
                        }
                        else
                        {
                            // Then the row above
                            bool foundSpecialCharInTopRow = false;
                            if (i > 0)
                            {
                                var lineAbove = engineMatrix[i - 1];
                                for (int k = scanStart; k <= scanEnd; k++)
                                {
                                    if (_specialCharacters.Any(c => c.Equals(lineAbove[k])))
                                    {
                                        // Character in the line above (adjacent to the number) is a special character, add the current string to the sum
                                        sum += int.Parse(current);
                                        foundSpecialCharInTopRow = true;
                                        break;
                                    }
                                }
                            }

                            // and finally, if necessary, the row below.
                            if (!foundSpecialCharInTopRow && (i + 1) <= engineMatrix.Length - 1)
                            {
                                var lineBelow = engineMatrix[i + 1];
                                for (int k = scanStart; k <= scanEnd; k++)
                                {
                                    if (_specialCharacters.Any(c => c.Equals(lineBelow[k])))
                                    {
                                        // Character in the line below (adjacent to the number) is a special character, add the current string to the sum
                                        sum += int.Parse(current);
                                        break;
                                    }
                                }
                            }
                        }

                        // Clear
                        current = "";
                        currentStart = null;
                        currentEnd = null;
                    }
                }
            }

            return sum;
        }

        private static char[][] GetEngineMatrix(string[] lines)
        {
            char[][] parts = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                parts[i] = new char[line.Length];
                for (int j = 0; j < line.Length; j++)
                {
                    parts[i][j] = line[j];
                }
            }

            return parts;
        }

        private static string[] GetPuzzleInput()
        {
            return File.ReadAllLines(".\\engine_schematic.txt");
        }
    }
}
