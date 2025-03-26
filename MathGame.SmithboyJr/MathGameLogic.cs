using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MathGame
{
    public class MathGameLogic
    {

        // List to store game history
        static List<string> GameHistory { get; set; } = new List<string>();
        // Default difficulty is 10 (easy)
        static int difficultyLevel = 10;
        // Random number generator
        static Random number = new Random();

        static void Main()
        {
            MainMenu();
        }

        static void MainMenu()
        {
            string? readResult;
            string menuSelection = "";

            do
            {
                // display the top-level menu options

                Console.Clear();

                Console.WriteLine("Welcome to the Math Game app. Your main menu options are:");
                Console.WriteLine(" 1. Addition");
                Console.WriteLine(" 2. Subtraction");
                Console.WriteLine(" 3. Multiplication");
                Console.WriteLine(" 4. Division");
                Console.WriteLine(" 5. Random Game");
                Console.WriteLine(" 6. Set Difficulty");
                Console.WriteLine(" 7. Game History");
                Console.WriteLine(" 8. Exit");
                Console.WriteLine();
                Console.WriteLine("Enter your selection number");

                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    menuSelection = readResult.ToLower();
                }

                // use switch-case to process the selected menu option
                switch (menuSelection)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        SetDifficulty();
                        StartGame(menuSelection);
                        break;

                    case "5":
                        SetDifficulty(); 
                        StartRandomGame();
                        break;

                    case "6":
                        SetDifficulty();
                        break;

                    case "7":
                        ShowGameHistory();
                        break;

                    case "8":
                        Console.WriteLine("Thanks for playing!");
                        break;

                    default:
                        Console.WriteLine("Invalid selection, please try again.");
                        break;
                }

            } while (menuSelection != "8");
        }

        static void SetDifficulty()
        {
            string? readResult;
            Console.Clear();
            Console.WriteLine("Select Difficulty Level:");
            Console.WriteLine(" 1. Easy (1-10)");
            Console.WriteLine(" 2. Medium (1-50)");
            Console.WriteLine(" 3. Hard (1-100)");

            readResult = Console.ReadLine();

            switch (readResult)
            {
                case "1":
                    difficultyLevel = 10; // Easy
                    break;

                case "2":
                    difficultyLevel = 50; // Medium
                    break;

                case "3":
                    difficultyLevel = 100; // Hard
                    break;

                default:
                    Console.WriteLine("Invalid selection. Defaulting to Easy difficulty (1-10).");
                    difficultyLevel = 10;
                    break;
            }

            Console.WriteLine($"Difficulty set to {difficultyLevel}. Press Enter to Start Game.");
            Console.ReadLine();
        }

        // Generates a random number between 1 and difficultyLevel
        static int GenerateRandomNumber()
        {
            return number.Next(1, difficultyLevel + 1);
        }

        // This method handles the timer and the actual game logic for all operations
        static void StartGame(string operation)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int num1 = GenerateRandomNumber();
            int num2 = GenerateRandomNumber();
            int correctAnswer = 0;

            string operationSymbol = "";
            string problem = "";

            // Choose operation based on user selection
            switch (operation)
            {
                // Addition
                case "1":
                    correctAnswer = num1 + num2;
                    operationSymbol = "+";
                    problem = $"{num1} + {num2}";
                    break;

                // Subtraction
                case "2":
                    correctAnswer = num1 - num2;
                    operationSymbol = "-";
                    problem = $"{num1} - {num2}";
                    break;

                // Multiplication
                case "3":
                    correctAnswer = num1 * num2;
                    operationSymbol = "*";
                    problem = $"{num1} * {num2}";
                    break;

                // Division (integer only)
                case "4":
                    while (num2 == 0 || num1 % num2 != 0) // Ensure division results in an integer
                    {
                        num2 = GenerateRandomNumber(); // Generate a new divisor if necessary
                    }
                    correctAnswer = num1 / num2;
                    operationSymbol = "/";
                    problem = $"{num1} / {num2}";
                    break;
            }

            // Ask user for the answer
            Console.Clear();
            Console.WriteLine($"Solve: {problem}");
            Console.Write("Your answer: ");
            string? userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int userAnswer))
            {
                stopwatch.Stop();
                string result = userAnswer == correctAnswer ? "Correct!" : $"Incorrect. The correct answer was {correctAnswer}.";
                string historyEntry = $"{operationSymbol} Problem: {problem} | Your Answer: {userAnswer} | {result} | Time: {stopwatch.Elapsed.TotalSeconds:F2} seconds";
                GameHistory.Add(historyEntry);

                Console.WriteLine(result);
                Console.WriteLine($"Time taken: {stopwatch.Elapsed.TotalSeconds:F2} seconds.");
                Console.WriteLine("Press Enter to return to the menu.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.ReadLine();
        }

        // Start a random game with random operations
        static void StartRandomGame()
        {
            string[] operations = { "1", "2", "3", "4" };
            string randomOperation = operations[number.Next(operations.Length)];
            StartGame(randomOperation);
        }

        // Display the history of games played
        static void ShowGameHistory()
        {
            Console.Clear();
            Console.WriteLine("Game History:");
            if (GameHistory.Count == 0)
            {
                Console.WriteLine("No games played yet.");
            }
            else
            {
                foreach (var entry in GameHistory)
                {
                    Console.WriteLine(entry);
                }
            }
            Console.WriteLine("Press Enter to return to the menu.");
            Console.ReadLine();
        }

    }
}