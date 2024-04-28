﻿using QuestionnaireLibrary;
using ScoreboardLibrary;

namespace Questionnaire
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Answer> guesses = new();
            List<Question> questions = new();

            // Create a question and answers
            Question question1 = new("What is the capital of France?");
            question1.Add(new Answer("Paris", true));
            question1.Add(new Answer("London", false));
            question1.Add(new Answer("Berlin", false));
            question1.Add(new Answer("Madrid", false));
            questions.Add(question1);

            Question question2 = new("What is the capital of Spain?");
            question2.Add(new Answer("Paris", false));
            question2.Add(new Answer("London", false));
            question2.Add(new Answer("Berlin", false));
            question2.Add(new Answer("Madrid", true));
            question2.Add(new Answer("Barcelona", false));
            questions.Add(question2);

            // Prompt the user with the questions
            foreach (Question question in questions)
            {
                PromptQuestion(question, guesses);
            }

            // Display the results
            Console.WriteLine();
            Console.WriteLine("You have completed the questionnaire.");
            int score = questions.Count(question => question.Answers.All(answer => answer.IsCorrect == guesses.Contains(answer)));
            Console.WriteLine($"You scored {score} out of {questions.Count}.");
            Console.WriteLine();
            Console.Write($"Enter your name: ");
            string? name = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("You must enter a name to save your score.");
                Console.WriteLine("Goodbye.");
                return;
            }

            Scoreboard scoreboard = new();
            scoreboard.Load();
            scoreboard.AddPlayer(name, score);
            scoreboard.SortScoreBoard();
            scoreboard.Save();

            Console.WriteLine($"Thank you, {name}. Your score has been added to the scoreboard.");

            Console.WriteLine();
            Console.WriteLine("Scoreboard:");
            foreach (PlayerScore player in scoreboard.PlayerScores)
            {
                Console.WriteLine($"{player.Name.PadLeft(10)}: \t\t{player.Score}/{questions.Count}");
            }
        }

        static void PromptQuestion(Question question, List<Answer> guesses)
        {
            // Hide the cursor
            Console.CursorVisible = false;

            // Display the question
            DisplayQuestion(question);

            // Display the answers and highlight the first one
            int currentAnswerIndex = 0;
            DisplayAnswers(question, currentAnswerIndex);

            // Save the current line
            int answerLine = Console.CursorTop - 1;

            bool running = true;
            while (running)
            {
                // Read a key
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.Q:
                    case ConsoleKey.Z:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.LeftArrow:
                        currentAnswerIndex = Math.Max(0, currentAnswerIndex - 1);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.RightArrow:
                        currentAnswerIndex = Math.Min(question.Answers.Count - 1, currentAnswerIndex + 1);
                        break;
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        Answer answer = question.GetAnswer(currentAnswerIndex);
                        guesses.Add(answer);
                        running = false;
                        continue;
                }

                // Redisplay the answers
                Console.SetCursorPosition(0, answerLine);
                DisplayAnswers(question, currentAnswerIndex);
            }

            // Show the cursor
            Console.CursorVisible = true;
        }

        static void DisplayQuestion(Question question)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(question.Text);
            Console.ResetColor();
        }

        static void DisplayAnswers(Question question, int highlightedIndex)
        {
            for (int i = 0; i < question.Answers.Count; i++)
            {
                if (i == highlightedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                Console.Write($"{(char)('a' + i)}. ");
                Console.Write(question.GetAnswer(i).Text);
                Console.ResetColor();
                Console.Write("  ");
            }
            Console.WriteLine();
        }
    }
}