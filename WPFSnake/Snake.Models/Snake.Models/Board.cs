using Snake.Models.SnakeDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snake.Models
{
    public class Board
    {
        public GameState gameState { get; set; }
        public Cell[,] board { get; set; } = new Cell[20, 20];
        public Snake snake { get; set; } = new Snake();
        public Direction direction_selected { get; set; }
        public Board()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    board[i, j] = Cell.EMPTY;
            snake.PlaceSnake(board);
            PlaceFood();
            direction_selected = Direction.LEFT;
            gameState = GameState.GAME;
        }
        public Cell GetCell(int x, int y)
        {
            if (x >= 0 && x < 20 && y >= 0 && y < 20)
                return board[x, y];
            else
                throw new ArgumentOutOfRangeException("Poważny błąd, przekroczony zakres tablicy!");
        }
        public void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    direction_selected = Direction.UP;
                    break;
                case Key.Down:
                    direction_selected = Direction.DOWN;
                    break;
                case Key.Right:
                    direction_selected = Direction.RIGHT;
                    break;
                case Key.Left:
                    direction_selected = Direction.LEFT;
                    break;
                default:
                    break;
            }
        }
        public void UpdateBoard(object sender, EventArgs e)
        {
            snake.ChangeDirection(direction_selected);
            if (!snake.Move())
            {
                gameState = GameState.GAMEOVER;
                return;
            }

            Point i = new Point()
            {
                x = snake.GetHead()[0],
                y = snake.GetHead()[1]
            };

            Cell new_head = board[i.x, i.y];

            if (new_head == Cell.SNAKE)
            {
                gameState = GameState.GAMEOVER;
                return;
            }
            else if (new_head == Cell.EMPTY)
            {
                board[i.x, i.y] = Cell.SNAKE;
                Point j = snake.GetTail();
                board[j.x, j.y] = Cell.EMPTY;
                snake.RemoveTail();
            }
            else if (new_head == Cell.FOOD)
            {
                board[i.x, i.y] = Cell.SNAKE;
                PlaceFood();
            }
        }
        void PlaceFood()
        {
            while (true)
            {
                Random rnd = new Random();
                int x = rnd.Next(0, 19);
                int y = rnd.Next(0, 19);
                Cell cell = board[x, y];
                if (cell == Cell.EMPTY)
                {
                    board[x, y] = Cell.FOOD;
                    return;
                }
            }
        }
        public int GetLength() => snake.body.Count;

        public int GetScore() => GetLength() - 1;
    }
}
