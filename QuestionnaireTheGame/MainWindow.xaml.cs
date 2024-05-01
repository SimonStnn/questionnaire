using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuestionnaireLibrary;
using ScoreboardLibrary;
using TriviaApiLibrary;

namespace QuestionnaireTheGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private QuestionsPage questionsPage = new(new QuestionPageHandler());

        private static readonly int numberOfQuestions = 10;
        private static List<Question> questions = new();
        private static List<Answer> guesses = new();
        private static int currentQuestionIndex = 0;
        private static Question CurrentQuestion => questions[currentQuestionIndex];

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

        private class QuestionPageHandler : IQuestionPageHandler
        {
            public int CurrentQuestionIndex
            {
                get => currentQuestionIndex;
                set => currentQuestionIndex = value;
            }
            public List<Question> Questions => questions;
            private static List<Answer> Guesses => guesses;

            public void QuestionAnswered(Answer answer)
            {
                Guesses.Add(answer);
                Question? next = Questions.ElementAtOrDefault(currentQuestionIndex);
                if (next == null)
                {
                    StringBuilder sb = new();
                    sb.AppendLine("You have answered all the questions!");
                    sb.AppendLine("Here are your results:");
                    int correct = 0;
                    for (int i = 0; i < Questions.Count; i++)
                    {
                        Question question = Questions[i];
                        Answer guess = Guesses[i];
                        sb.AppendLine($"{question.Text}");
                        sb.AppendLine($"Your answer: {guess.Text}");
                        sb.AppendLine($"Correct answer: {question.CorrectAnswer.Text}");
                        sb.AppendLine();
                        if (guess.IsCorrect)
                            correct++;
                    }
                    sb.AppendLine($"You got {correct} out of {questions.Count} correct!");
                    MessageBox.Show(sb.ToString(), "Results");
                }
            }

            public void Done()
            {
                MessageBox.Show("You have answered all the questions!", "Results");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            mainFrame.Navigate(questionsPage);

            lblInfo.Content = "Welcome to the Trivia Challenge! Loading your questions...";
            questionsPage.lblQuestion.Content = "Loading questions...";

            IQuestionHandler handler = new QuestionHandler();
            _ = LoadQuestions(handler);
        }

        private async Task LoadQuestions(IQuestionHandler handler)
        {
            // Array to store the tasks
            Task[] tasks = new Task[numberOfQuestions];
            for (int i = 0; i < numberOfQuestions; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    await TriviaApiRequester.RequestRandomQuestion(handler);
                });
            }
            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            lblInfo.Content = "Trivia Challenge!";
            currentQuestionIndex = 0;
            questionsPage.RenderQuestion();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AboutWindow aboutWindow = new();
            aboutWindow.ShowDialog();
            this.Show();
        }
    }
}