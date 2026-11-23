/*--------------------------------------------------------------
*				HTBLA-Leonding / Class: 1xHIF
*--------------------------------------------------------------
*              Musterlösung-HA
*--------------------------------------------------------------
* Description: PingPong
*--------------------------------------------------------------
*/

using System;
namespace PingPong;

class Snake
{
    private bool _dead = false; 

    private int angle = 45;
    private char snakeChar = '*';
    private const int SNAKES_LENGTH = 10;
    private ConsoleGraphics _graphics;
    private Mirror _mirror;
    
    public record Cordinate
    {
        public int x { get; set; } 
        public int y { get; set; }
    };
    Cordinate[] snake = new Cordinate[SNAKES_LENGTH];

    
    public Snake(ConsoleGraphics graphics, Mirror mirror)
    {
        _graphics = graphics;
        _mirror = mirror;
        _nextTime = Environment.TickCount;
        int initialX = Random.Shared.Next(SNAKES_LENGTH, _graphics.Width - 1);
        int initialY = Random.Shared.Next(_graphics.Height / 2, _graphics.Height - 2);
        
        
        //the first index is the snakes head
        snake[0] = new Cordinate { x = initialX, y = initialY };

        for (int i = 1; i < SNAKES_LENGTH; i++)
        {
            snake[i] = new Cordinate { x = initialX - i, y = initialY };
        }
    }
    
    
    public bool IsDead
    {
        get { return _dead; } 
    }
    private void ChechIfHasToChangeAngle(int dy, int dx, int nX, int nY)
    {
        bool bottom = nY <= 0;
        bool top = nY > _graphics.Height - 1;
        bool side = nX <= 0 || nX >= _graphics.Width - 1;
        CheckTopAndBottom(bottom, top);

        CheckSideWalls(side, dy);
    }

    private void CheckTopAndBottom(bool bottom, bool top)
    {
        if (bottom || top)
        {
            int random = Random.Shared.Next(2);
            if(angle == 90 || angle == 270)
            {
                if (bottom)
                {
                    if (random == 1)
                    {
                        angle = 45;
                    }
                    else
                    {
                        angle = 135;
                    }
                }
                else
                {
                    if (random == 1)
                    {
                        angle = 315;
                    }
                    else
                    {
                        angle = 225;
                    }
                }
            }
            else if (Random.Shared.Next(100) < 25)
            {
                if (bottom)
                {
                    angle = 90;
                }
                else
                {
                    angle = 270;
                }
            }
            else
            {
                angle = (360 - angle) % 360;
            }
        }
    }
    private void GetStep(out int dx, out int dy)
    {
        double rad = angle * Math.PI / 180;
        dx = (int)Math.Round(Math.Cos(rad));
        dy = (int)Math.Round(Math.Sin(rad));
    }

    private void CheckSideWalls(bool side, int dy)
    {
        if (side)
        {
            // cant use the 360 - angle method, because it calculates the right direction, for the snake to bounce,
            //with the other one it could still push in to the wall, which would break the program.
            if (Random.Shared.Next(100) < 25)
            {
                if(dy > 0)
                {
                    angle = 90;
                }
                else
                {
                    angle = 270;
                }
            }
            else
            {
                angle = (540 - angle) % 360;
            }
        }
    }
    private bool MoveSnake()
    {
        GetStep(out int dx, out int dy);
        int nextX = snake[0].x + dx;
        int nextY = snake[0].y + dy;
        if (nextY <= 0)
        {
            
            if (!(nextX >= _mirror.firstX && nextX <= _mirror.lastX))            {
                _dead = true;
                return true;
            }
        }
        ChechIfHasToChangeAngle(dy, dx, nextX, nextY);
        GetStep(out dx, out dy);  
        for (int i = SNAKES_LENGTH - 1; i > 0; i--)
        {
            snake[i].x = snake[i - 1].x;
            snake[i].y = snake[i - 1].y;
        }
        snake[0].x += dx;
        snake[0].y += dy;
        return false;
    }
    

    private void DrawSnake()
    {
        
        ConsoleColor _color;
        for (int i = 0; i < SNAKES_LENGTH; i++)
        {
            if (i == 0)
            {
                _color = ConsoleColor.Red;
            }
            else
            {
                _color = ConsoleColor.Green;
            }
            
            _graphics.DrawChar(snake[i].x, snake[i].y, snakeChar, _color);
        }
        
    }
    private int _nextTime;
    private int _delay = Random.Shared.Next(80, 300);
    public bool Loop()
    {
        //TODO
        if (_dead)
        {
            for (int i = 0; i < SNAKES_LENGTH; i++)
            {
                _graphics.DrawChar(snake[i].x, snake[i].y, ' ', ConsoleColor.Black);
            }
            return false;
        }
        if (_nextTime > Environment.TickCount)
        {
            return false;
        }

        int angleAccelerator = _delay;
       
        _graphics.DrawChar(snake[SNAKES_LENGTH - 1].x, snake[SNAKES_LENGTH - 1].y, ' ', ConsoleColor.Black);
        MoveSnake(); 
        if (angle == 90 || angle == 270)
        {
            angleAccelerator = (int)(_delay / Math.Sqrt(2));
        }
        _nextTime = Environment.TickCount + angleAccelerator;
        DrawSnake();
        return true;
    }

}