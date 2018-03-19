using Snake.Models.SnakeDetails;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public class Snake : IEnumerable
    {
        public List<Point> body { get; set; } = new List<Point>();
        public Direction direction { get; set; }
        public Snake()
        {
            var head = new Point();
            head.x = 5;
            head.y = 5;
            body.Add(head);
            //head.y = 4;
            //body.Add(head);
            direction = Direction.LEFT;
        }

        public int[] GetHead()
        {
            int x = body.First<Point>().x;
            int y = body.First<Point>().y;
            int[] point = { x, y };
            return point;
        }

        public int GetHeadXValue() => body.First<Point>().x;
        public Point GetTail() => body.Last<Point>();
        public int GetTailXValue() => body.Last<Point>().x;
        int GetLength() => body.Count;
        public void RemoveTail() => body.Remove(body.Last<Point>());
        public bool Move()
        {
            Point newHead = new Point()
            {
                x = GetHead()[0],
                y = GetHead()[1]
            };

            switch (direction)
            {
                case Direction.UP:
                    {
                        newHead.y -= 1;
                        break;
                    }
                case Direction.DOWN:
                    {
                        newHead.y += 1;
                        break;
                    }
                case Direction.RIGHT:
                    {
                        newHead.x += 1;
                        break;
                    }
                case Direction.LEFT:
                    {
                        newHead.x -= 1;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            // przejscie poza mape
            if (newHead.y < 0 || newHead.y > 19
             || newHead.x < 0 || newHead.x > 19)
                return false;

            body.Insert(0, newHead);
            return true;
        }
        public void ChangeDirection(Direction dir)
        {
            if ((direction == Direction.UP && dir == Direction.DOWN) ||
              (direction == Direction.LEFT && dir == Direction.RIGHT) ||
              (direction == Direction.RIGHT && dir == Direction.LEFT) ||
              (direction == Direction.DOWN && dir == Direction.UP))
            {
                return;
            }
            direction = dir;
        }
        public void PlaceSnake(Cell[,] board)
        {
            for (int i = 0; i < GetLength(); i++)
            {
                int x = body[i].x;
                int y = body[i].y;
                board[x, y] = Cell.SNAKE;
            }
        }
        public IEnumerator GetEnumerator() => body.GetEnumerator();
    }
}
