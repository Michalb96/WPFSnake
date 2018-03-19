using Snake.Models;
using Snake.Models.SnakeDetails;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace WPFSnake
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameState gameState;
        private Board _board = new Board();
        private List<Rectangle> _rectangles = new List<Rectangle>();
        private DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            CreateBoard();
            InGame();
        }

        private void InGame()
        {
            while(_board.gameState == GameState.GAME)
            {
                timer.Tick += new EventHandler(_board.UpdateBoard);
                timer.Tick += new EventHandler(UpdateBoard);
                timer.Tick += new EventHandler(ShowScore);
                timer.Interval = new TimeSpan(1000000);
                timer.Start();
                this.KeyDown += new KeyEventHandler(_board.OnButtonKeyDown);
                break;
            }   
            
            if(_board.gameState == GameState.GAMEOVER)
            {
                GameOver();
            }
            //else
            //{
            //    ExitGame();
            //}

        }

        public void CreateBoard()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Height = 15,
                        Width = 15,
                        StrokeThickness = 0.25,
                        Stroke = Brushes.Black,
                        Name = "box" + i.ToString() + j.ToString()
                    };
                    RenderCell(_board.board[i,j], rectangle);
                    Canvas.SetLeft(rectangle, i * 15);
                    Canvas.SetTop(rectangle, j * 15);
                    board.Children.Add(rectangle);
                    _rectangles.Add(rectangle);                  
                }
            } 
        }

        public void UpdateBoard(object sender, EventArgs e)
        {
            if (_board.gameState == GameState.GAMEOVER)
            {
                GameOver();
                return;
            }
            board.Children.RemoveRange(0, board.Children.Count);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Height = 15,
                        Width = 15,
                        StrokeThickness = 0.25,
                        Stroke = Brushes.Black,
                        Name = "box" + i.ToString() + j.ToString()
                    };
                    RenderCell(_board.board[i, j], rectangle);
                    Canvas.SetLeft(rectangle, i * 15);
                    Canvas.SetTop(rectangle, j * 15);
                    board.Children.Add(rectangle);
                }
            }
            
        }

        public void RenderCell(Cell board, Rectangle rectangle)
        {
            
            switch(board)
            {
                case Cell.EMPTY:
                    rectangle.Fill = Brushes.White;
                    break;
                case Cell.FOOD:
                    rectangle.Fill = Brushes.Red;
                    break;
                case Cell.SNAKE:
                    rectangle.Fill = Brushes.DarkOliveGreen;
                    break;
                default:
                    return;
            }
        }

        public void CheckGameState(object sender, EventArgs e)
        {
            if(_board.gameState == GameState.GAMEOVER)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            timer.Stop();
            board.Children.RemoveRange(0, board.Children.Count);
            scoreLabel.Visibility = Visibility.Hidden;
            string score = _board.GetScore().ToString();
            Label gameOverLabel = new Label()
            {
                Width = 200,
                Height = 100,
                FontSize = 20,
                Content = $"Your score is:\n{score}"
            };
            Canvas.SetTop(gameOverLabel, 120);
            Canvas.SetLeft(gameOverLabel, 120);
            board.Children.Add(gameOverLabel);
        }

        public void ExitGame()
        {
            this.Close();
        }

        public void ShowScore(object sender, EventArgs e)
        {
            string score = _board.GetScore().ToString();
            scoreLabel.Content = (string)$"Score:\n{score}";
        }
    }
}
