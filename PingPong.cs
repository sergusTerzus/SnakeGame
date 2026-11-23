/*--------------------------------------------------------------
 *				HTBLA-Leonding / Class: 1xHIF
 *--------------------------------------------------------------
 *              Musterlösung-HA
 *--------------------------------------------------------------
 * Description: PingPong
 *--------------------------------------------------------------
 */

using System;
using PingPong;




Console.WriteLine("Snake");
Console.WriteLine("****************************");

Console.Write("Number of snakes: ");
int snakeCount = int.Parse(Console.ReadLine()!);

AnimatePingPong(snakeCount);


void AnimatePingPong(int snakeCount)
{
    Console.SetWindowSize(80, 26);
    Console.SetBufferSize(80, 26);
    ConsoleGraphics graphics = new ConsoleGraphics();
    Mirror mirror = new Mirror(graphics);
    //TODO

    Snake[] snakes = new Snake[snakeCount];

    
    graphics.Initialize();
    Console.Clear();
    graphics.FillBox(0, 1, graphics.Width - 1, graphics.Height - 1, '.', ConsoleColor.DarkGray);
    mirror.MakeMirror();
    int counter = 0;
    for (int i = 0; i < snakes.Length; i++)
    {
        snakes[i] = new Snake(graphics, mirror);
    }

    bool running = true;
    do
    {
        counter = 0;
        for (int i = 0; i < snakes.Length; i++)
        {
            snakes[i].Loop();
            if (snakes[i].IsDead)
            {
                counter++;
            }
        }
        running = mirror.Loop();
        
    } while (counter != snakeCount && running);
    graphics.Terminate();
    Console.ResetColor();
    Console.CursorVisible = true;
    Console.Clear();
//TODO
}
