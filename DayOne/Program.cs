namespace DayOne
{
    internal class Program
    {
        private static readonly Dictionary<string, int> numMap = new()
        {
            { "zero", 0},
            { "one", 1},
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        static void Main()
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            int sum = 0;
            var lines = GetPuzzleInput();

            foreach (var line in lines)
            {
                sum += GetNumFromDigitOnlyString(line);
            }

            Console.WriteLine(sum);
        }

        private static void PartTwo()
        {
            var lines = GetPuzzleInput();
            int sum = 0;

            int invalidCount = 0;
            int validCount = 0;

            for(int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                List<int> potentialInts = [];
                int idx = 0;
                string current = "";
                while (idx < line.Length)
                {
                    if (int.TryParse(line[idx].ToString(), out int digit))
                    {
                        potentialInts.Add(digit);
                        current = "";
                    }
                    else
                    {
                        current += line[idx];

                        if (current.Length > numMap.Keys.Max(k => k.Length))
                        {
                            current = current.Remove(0, current.Length - numMap.Keys.Max(k => k.Length));
                        }

                        foreach (var kvp in numMap)
                        {
                            if(current.Contains(kvp.Key))
                            {
                                potentialInts.Add(kvp.Value);
                                break;
                            }
                        }
                    }

                    ++idx;
                }

                string numStr = "";
                if(potentialInts.Count == 1)
                {
                    numStr = $"{potentialInts[0]}{potentialInts[0]}";
                }
                else if(potentialInts.Count > 1)
                {
                    numStr = $"{potentialInts[0]}{potentialInts[^1]}";
                }
                
                if (string.IsNullOrEmpty(numStr))
                {
                    Console.WriteLine("Invalid count detected for line: " + line);
                    invalidCount++;
                }
                else
                {
                    Console.WriteLine("| # {0, 4} | {1, 6}: {2, -49} | ({3, 2}) |", i + 1, "String", line, numStr);
                    sum += int.Parse(numStr);
                    validCount++;
                }
            }
            Console.WriteLine($"Valid: {validCount}/{lines.Length} | Invalid: {invalidCount}/{lines.Length}");
            Console.WriteLine(sum);
        }

        private static int GetNumFromDigitOnlyString(string line)
        {
            var potentialIntsToAdd = new List<char>();
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    potentialIntsToAdd.Add(line[i]);
                }
            }
            string numToAdd;
            if (potentialIntsToAdd.Count >= 2)
            {
                numToAdd = $"{potentialIntsToAdd[0]}{potentialIntsToAdd[^1]}";
            }
            else
            {
                numToAdd = $"{potentialIntsToAdd[0]}{potentialIntsToAdd[0]}";
            }
            Console.WriteLine("Adding new num: " + numToAdd + " from string: " + line);
            return int.Parse(numToAdd);
        }

        private static string[] GetPuzzleInput()
        {
            return File.ReadAllLines(".\\puzzle_input.txt");
        }
    }
}
