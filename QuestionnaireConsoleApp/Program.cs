using QuestionnaireLibrary;
using ScoreboardLibrary;
using TriviaApiLibrary;

namespace Questionnaire
{
    internal class Program
    {
        static List<Question> questions = new();
        static List<Answer> guesses = new();

        private class QuestionHandler : IQuestionHandler
        {
            void IQuestionHandler.ProcessQuestion(TriviaMultipleChoiceQuestion question)
            {
                // Convert the trivia question to your Question class
                Question newQuestion = new(question.Question.Text);
                newQuestion.Add(new Answer(question.CorrectAnswer, true));
                foreach (string incorrectAnswer in question.IncorrectAnswers)
                {
                    newQuestion.Add(new Answer(incorrectAnswer, false));
                }
                questions.Add(newQuestion);
            }
        }

        static void WelcomeMessage()
        {
            Console.Title = "Questionnaire";
            Console.WriteLine("Welcome to the Trivia Challenge!");
            Console.WriteLine("You will be presented with 10 questions.");
            Console.WriteLine("Can you score 100%?");
            Console.WriteLine();
        }
        static async Task Main(string[] args)
        {
            WelcomeMessage();

            IQuestionHandler handler = new QuestionHandler();

            // Array to store the tasks
            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    await TriviaApiRequester.RequestRandomQuestion(handler);
                });
            }
            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

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

            if (string.IsNullOrWhiteSpace(name))
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
                Console.WriteLine($"{player.Name,10}: \t{player.Score}/{questions.Count}");
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