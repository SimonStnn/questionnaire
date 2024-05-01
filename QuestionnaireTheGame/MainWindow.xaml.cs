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

        public MainWindow()
        {
            InitializeComponent();

            lblInfo.Content = "Welcome to the Trivia Challenge! Loading your questions...";
            lblQuestion.Content = "Loading questions...";

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
            RenderQuestion();
        }

        private void RenderQuestion()
        {
            RenderQuestion(CurrentQuestion);
        }

        private void RenderQuestion(Question question)
        {
            lblQuestion.Content = question.Text;
            spAnswers.Children.Clear();
            foreach (Answer answer in question.Answers)
            {
                Button btn = RenderAnswer(answer);
                spAnswers.Children.Add(btn);
            }
        }

        private static Button RenderAnswer(Answer answer)
        {
            Button btn = new()
            {
                Content = answer.Text,
            };
            btn.Click += (sender, e) =>
            {

            };
            return btn;
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