using QuestionnaireLibrary;
using ScoreboardLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuestionnaireTheGame
{
	/// <summary>
	/// Interaction logic for ResultPage.xaml
	/// </summary>
	public partial class ResultPage : Page
	{
		public static readonly Scoreboard scoreboard = new();
		public readonly int score;

		public ResultPage(List<Question> questions, List<Answer> guesses)
		{
			InitializeComponent();

			// Calculate the score and display it
			score = CalculateScore(questions, guesses);
			lblScore.Content = $"Score: {score}/{questions.Count}";

			// Display the questions and answers
			spGuesses.Children.Clear();
			foreach((Question question, Answer guess) in questions.Zip(guesses, (q, g) => (q, g)))
			{
				spGuesses.Children.Add(RenderResult(question, guess));
			}

			// Display top users in scoreboard
			RenderScoreBoard();
		}

		public static int CalculateScore(List<Question> questions, List<Answer> guesses) =>
			guesses.Count(guess => guess.IsCorrect);

		public void SavePlayer(string name, int score)
		{
			scoreboard.AddPlayer(name, score);
			scoreboard.SortScoreBoard();
			scoreboard.Save();
			RenderScoreBoard();
		}

		public static Grid RenderResult(Question question, Answer guess)
		{
			Grid grid = new()
			{
				RowDefinitions =
				{
					new RowDefinition(),
					new RowDefinition(),
				},
			};

			TextBlock tbQuestion = new()
			{
				Text = question.Text,
				FontSize = 16,
				Padding = new Thickness(5, 1, 5, 1),
				TextWrapping = TextWrapping.Wrap,
			};

			StackPanel spAnswers = new()
			{
				Orientation = Orientation.Vertical,
				Resources = new()
				{
					{ "CorrectAnswer", new SolidColorBrush(Colors.Green) },
					{ "IncorrectAnswer", new SolidColorBrush(Colors.Red) },
				},
			};

			foreach(Answer answer in question.Answers)
			{
				// If the answer was correct, make the text green; if the answer was incorrect, and the user guessed it, make the text red
				RadioButton rbAnswer = new()
				{
					Content = answer.Text,
					IsChecked = answer == guess,
					IsEnabled = false,
					Foreground = (answer.IsCorrect, answer == guess) switch
					{
						(true, _) => (Brush)spAnswers.Resources["CorrectAnswer"],
						(false, true) => (Brush)spAnswers.Resources["IncorrectAnswer"],
						_ => Brushes.Black,
					},
					Margin = new Thickness(10, 1, 10, 1),
				};
				spAnswers.Children.Add(rbAnswer);
			}
			grid.Children.Add(tbQuestion);
			grid.Children.Add(spAnswers);
			Grid.SetRow(spAnswers, 1);
			return grid;
		}

		public static Grid RenderScoreBoardPlayer(PlayerScore player)
		{
			Grid grid = new();

			Label name = new()
			{
				Content = player,
				HorizontalContentAlignment = HorizontalAlignment.Center,
			};

			grid.Children.Add(name);

			return grid;
		}

		public void RenderScoreBoard()
		{
			spScoreboard.Children.Clear();
			scoreboard.Load();
			foreach(PlayerScore playerScore in scoreboard.PlayerScores)
			{
				Trace.WriteLine($"Adding {playerScore.Name}");
				spScoreboard.Children.Add(RenderScoreBoardPlayer(playerScore));
			}
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			if(tbName.Text == "") return;

			SavePlayer(tbName.Text, score);
			tbName.IsEnabled = false;
			btnSave.IsEnabled = false;
		}
	}
}
