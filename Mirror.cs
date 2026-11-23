/*--------------------------------------------------------------
*				HTBLA-Leonding / Class: 1xHIF
*--------------------------------------------------------------
*              Musterlösung-HA
*--------------------------------------------------------------
* Description: PingPong
*--------------------------------------------------------------
*/

using System;
using System.Runtime.ExceptionServices;

namespace PingPong;

class Mirror
{
    private ConsoleColor _color = ConsoleColor.Cyan;
    const int LENGTH = 10;
    private ConsoleGraphics _graphics;
    private static int x0 = 0;
    private static int x1 = LENGTH-1;
    private int y = 0;
    public Mirror(ConsoleGraphics graphics)
    {
        _graphics = graphics;
        MakeMirror();
    }

    public int firstX = x0;
    public int lastX = x1;
    
    public void MakeMirror()
    {
        for (int i = 0; i < LENGTH; i++)
        {
            _graphics.DrawChar(x0 +i, y, 'o', _color);
        }
    }
    public bool Loop()
    {
        
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.RightArrow && x1 +1 < _graphics.Width)
            {
                _graphics.DrawChar(x0,y,' ', _color);
                ++x0;
                ++x1;
                MakeMirror();
            }

            if (key.Key == ConsoleKey.LeftArrow && x0- 1 >= 0)
            {
                _graphics.DrawChar(x1,y,' ', _color);
                --x0;
                --x1;
                MakeMirror();
            }

            firstX = x0;
            lastX = x1;
            
            //TODO

            return key.Key != ConsoleKey.Enter;
        }

        return true;
    }
}