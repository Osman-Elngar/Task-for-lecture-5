/*
1.Video of Task

2. Bonus:
-- don't allow duplicate entries
-- multiple input
-- sorting asc / des
-- swapping indexes / values
-- come up with your own ideas!

3. Important Note:
- Don't Use:
-- .Min()
-- .Max()
-- .Avg() / .Average
-- .Find()
-- Methods
-- ForEach
-- .Any()
-- .First() / .Last() / .Single()

- Use:
-- .Count
-- .Add()
-- .Length
-- .Clear()
-- .ToLower()
-- .ToUpper()       */

/*Main Menu
P-Print numbers
A-Add a number
M-Display mean of the numbers
S-Display the smallest numbaer
L-Display the largest number
F-Find a number
C-Clear the whole list
Q-Quit                      */

using System;
using System.Collections.Generic;

namespace FristProject
{
    internal class NumberManager
    {
        private List<int> numbers;

        public NumberManager()
        {
            numbers = new List<int>();
        }

        public void PrintNumbers()
        {
            if (numbers.Count == 0) { Console.WriteLine("[] - the list is empty"); return; }
            Console.WriteLine("Numbers:");
            for (int i = 0; i < numbers.Count; i++) Console.WriteLine($"[{i}] -> {numbers[i]}");
        }

        public void AddNumbersFromInput(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) { Console.WriteLine("No input given."); return; }

            var parts = line.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            var added = 0;
            var duplicates = 0;

            for (int p = 0; p < parts.Length; p++)
            {
                var token = parts[p].Trim();
                int val;
                if (!int.TryParse(token, out val))
                {
                    Console.WriteLine($"Skipped invalid token: '{token}'");
                    continue;
                }

                bool exists = false;
                for (int j = 0; j < numbers.Count; j++)
                {
                    if (numbers[j] == val) { exists = true; break; }
                }

                if (exists) duplicates++; else { numbers.Add(val); added++; }
            }

            Console.WriteLine($"Added: {added}. Duplicates skipped: {duplicates}.");
        }

        public void DisplayMean()
        {
            if (numbers.Count == 0) { Console.WriteLine("Unable to calculate mean - no data."); return; }
            long sum = 0;
            for (int i = 0; i < numbers.Count; i++) sum += numbers[i];
            double mean = (double)sum / numbers.Count;
            Console.WriteLine($"Mean: {mean:F2}");
        }

        public void DisplaySmallest()
        {
            if (numbers.Count == 0) { Console.WriteLine("List is empty."); return; }
            int min = numbers[0];
            for (int i = 1; i < numbers.Count; i++) if (numbers[i] < min) min = numbers[i];
            Console.WriteLine($"Smallest number: {min}");
        }

        public void DisplayLargest()
        {
            if (numbers.Count == 0) { Console.WriteLine("List is empty."); return; }
            int max = numbers[0];
            for (int i = 1; i < numbers.Count; i++) if (numbers[i] > max) max = numbers[i];
            Console.WriteLine($"Largest number: {max}");
        }

        public void FindNumberFromInput(string input)
        {
            int target;
            if (!int.TryParse(input, out target)) { Console.WriteLine("Invalid number."); return; }

            var countFound = 0;
            var indices = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == target) { countFound++; indices.Add(i); }
            }

            if (countFound == 0) Console.WriteLine($"{target} was not found.");
            else
            {
                Console.Write($"{target} found {countFound} time(s) at index(es): ");
                for (int i = 0; i < indices.Count; i++)
                {
                    Console.Write(indices[i].ToString());
                    if (i < indices.Count - 1) Console.Write(", ");
                }
                Console.WriteLine();
            }
        }

        public void ClearAll()
        {
            numbers.Clear();
            Console.WriteLine("List cleared.");
        }

        public void SortMode(string modeRaw)
        {
            if (numbers.Count < 2) { Console.WriteLine("Not enough items to sort."); return; }
            if (string.IsNullOrWhiteSpace(modeRaw)) { Console.WriteLine("No sort mode given."); return; }
            var m = modeRaw.Trim().ToLower();

            for (int pass = 0; pass < numbers.Count - 1; pass++)
            {
                for (int i = 0; i < numbers.Count - 1 - pass; i++)
                {
                    bool shouldSwap = false;
                    if (m == "a") { if (numbers[i] > numbers[i + 1]) shouldSwap = true; }
                    else { if (numbers[i] < numbers[i + 1]) shouldSwap = true; }

                    if (shouldSwap)
                    {
                        int tmp = numbers[i];
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = tmp;
                    }
                }
            }

            Console.WriteLine(m == "a" ? "Sorted ascending." : "Sorted descending.");
        }

        public void SwapIndexesFromInput(string aRaw, string bRaw)
        {
            int aIdx, bIdx;
            if (!int.TryParse(aRaw, out aIdx) || !int.TryParse(bRaw, out bIdx)) { Console.WriteLine("Invalid index input."); return; }
            if (aIdx < 0 || aIdx >= numbers.Count || bIdx < 0 || bIdx >= numbers.Count) { Console.WriteLine("Index out of range."); return; }

            int tmp = numbers[aIdx];
            numbers[aIdx] = numbers[bIdx];
            numbers[bIdx] = tmp;
            Console.WriteLine($"Swapped indexes {aIdx} and {bIdx}.");
        }

        public void SwapValuesFromInput(string v1Raw, string v2Raw)
        {
            int v1, v2;
            if (!int.TryParse(v1Raw, out v1) || !int.TryParse(v2Raw, out v2)) { Console.WriteLine("Invalid value input."); return; }

            int idx1 = -1, idx2 = -1;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (idx1 == -1 && numbers[i] == v1) idx1 = i;
                if (idx2 == -1 && numbers[i] == v2) idx2 = i;
                if (idx1 != -1 && idx2 != -1) break;
            }

            if (idx1 == -1 || idx2 == -1) { Console.WriteLine("One or both values not found."); return; }

            int tmp = numbers[idx1];
            numbers[idx1] = numbers[idx2];
            numbers[idx2] = tmp;
            Console.WriteLine($"Swapped values {v1} (index {idx1}) and {v2} (index {idx2}).");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Number Manager");
            var manager = new NumberManager();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("P - Print numbers");
                Console.WriteLine("A - Add a number (multiple input accepted)");
                Console.WriteLine("M - Display mean of the numbers");
                Console.WriteLine("S - Display the smallest number");
                Console.WriteLine("L - Display the largest number");
                Console.WriteLine("F - Find a number");
                Console.WriteLine("C - Clear the whole list");
                Console.WriteLine("O - Sort numbers (Asc/Desc)");
                Console.WriteLine("X - Swap two indexes");
                Console.WriteLine("V - Swap two values");
                Console.WriteLine("Q - Quit");
                Console.Write("Choose an option: ");

                var choiceRaw = Console.ReadLine();
                if (choiceRaw == null) continue;
                var choice = choiceRaw.Trim().ToLower();

                if (choice == "q") { Console.WriteLine("Goodbye."); break; }

                if (choice == "p") manager.PrintNumbers();
                else if (choice == "a")
                {
                    Console.Write("Enter number(s) (separate with space or comma): ");
                    var line = Console.ReadLine();
                    manager.AddNumbersFromInput(line ?? "");
                }
                else if (choice == "m") manager.DisplayMean();
                else if (choice == "s") manager.DisplaySmallest();
                else if (choice == "l") manager.DisplayLargest();
                else if (choice == "f")
                {
                    Console.Write("Enter number to find: ");
                    var input = Console.ReadLine();
                    manager.FindNumberFromInput(input ?? "");
                }
                else if (choice == "c") manager.ClearAll();
                else if (choice == "o")
                {
                    Console.Write("Sort ascending or descending? (A/D): ");
                    var mode = Console.ReadLine();
                    manager.SortMode(mode ?? "");
                }
                else if (choice == "x")
                {
                    Console.Write("Enter first index: ");
                    var aRaw = Console.ReadLine();
                    Console.Write("Enter second index: ");
                    var bRaw = Console.ReadLine();
                    manager.SwapIndexesFromInput(aRaw ?? "", bRaw ?? "");
                }
                else if (choice == "v")
                {
                    Console.Write("Enter first value to swap: ");
                    var v1Raw = Console.ReadLine();
                    Console.Write("Enter second value to swap: ");
                    var v2Raw = Console.ReadLine();
                    manager.SwapValuesFromInput(v1Raw ?? "", v2Raw ?? "");
                }
                else Console.WriteLine("Unknown option.");
            }
        }
    }
}