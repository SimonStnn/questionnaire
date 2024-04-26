using QuestionnaireLibrary;

namespace Questionnaire
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a question and answers
            Question question = new("What is the capital of France?");
            question.Add(new Answer("Paris", true));
            question.Add(new Answer("London", false));
            question.Add(new Answer("Berlin", false));
            question.Add(new Answer("Madrid", false));

            // Display the question
            DisplayQuestion(question);

            // Display the answers and highlight the first one
            int currentAnswerIndex = 0;
            DisplayAnswers(question, currentAnswerIndex);

            // Save the current line
            int answerLine = Console.CursorTop - 1;

            // Main loop
            while (true)
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
                        if (answer.IsCorrect)
                        {
                            Console.WriteLine("Correct!");
                        }
                        else
                        {
                            Console.WriteLine("Incorrect. The correct answer is: " + question.Answers.First(a => a.IsCorrect).Text);
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                }   

                // Redisplay the answers
                Console.SetCursorPosition(0, answerLine);
                DisplayAnswers(question, currentAnswerIndex);
            }
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