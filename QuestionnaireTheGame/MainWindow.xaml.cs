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
        private static readonly QuestionsPage questionsPage = new(new QuestionPageHandler());

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
                UpdateProgress();
            }

            public void Done()
            {
                ResultPage pgResult = new(questions, guesses);
                questionsPage.NavigationService.Navigate(pgResult);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            mainFrame.Navigate(questionsPage);

            lblInfo.Content = "Welcome to the Trivia Challenge! Loading your questions...";
            questionsPage.tbQuestion.Text = "Loading questions...";
            UpdateProgress();

            IQuestionHandler handler = new QuestionHandler();
            _ = LoadQuestions(handler);
        }

        // Fisher-Yates shuffle algorithm
        public static List<T> Shuffle<T>(List<T> list)
        {
            Random rnd = new();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
            return list;
        }

        private static void UpdateProgress()
        {
            questionsPage.lblProgress.Content = $"Question {currentQuestionIndex + 1} of {numberOfQuestions}";
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
            questions = Shuffle(questions);
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