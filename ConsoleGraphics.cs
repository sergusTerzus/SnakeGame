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

class ConsoleGraphics
{
    const int _width = 80;
    const int _height = 26;

    public int Width
    {
        get { return _width; }
    }
    public int Height
    {
        get { return _height; }
    }

    public void Initialize()
    {
        Console.SetWindowSize(_width, _height);
        Console.CursorVisible = false;
    }

    public void Terminate()
    {
        Console.ResetColor();
        Console.SetCursorPosition(0, _height - 1);
        Console.WriteLine();
        Console.CursorVisible = true;
    }

    public void DrawChar(int x, int y, char ch, ConsoleColor color)
    {
        Console.SetCursorPosition(x, _height - y - 1);
        Console.ForegroundColor = color;
        Console.Write(ch);
    }

    public void DrawDot(int x, int y, ConsoleColor color)
    {
        DrawChar(x, y, '*', color);
    }

    public void DrawLine(int x0, int y0, int x1, int y1, ConsoleColor color)
    {
        int distanceX = x1 - x0;
        int distanceY = y1 - y0;

        int distance = Math.Max(Math.Abs(distanceX), Math.Abs(distanceY));

        int roundOffsetX = distanceX >= 0 ? distance / 2 : -distance / 2;
        int roundOffsetY = distanceY >= 0 ? distance / 2 : -distance / 2;

        for (int i = 0; i <= distance; i++)
        {
            int x = x0 + (i * distanceX + roundOffsetX) / distance;
            int y = y0 + (i * distanceY + roundOffsetY) / distance;
            DrawDot(x, y, color);
        }
    }

    public void DrawBox(int x0, int y0, int x1, int y1, ConsoleColor color)
    {
        DrawLine(x0, y0, x1, y0, color);
        DrawLine(x1, y0, x1, y1, color);
        DrawLine(x1, y1, x0, y1, color);
        DrawLine(x0, y1, x0, y0, color);
    }

    public void FillBox(int x0, int y0, int x1, int y1, char ch, ConsoleColor color)
    {
        int xStart = Math.Min(x0, x1);
        int xEnd = Math.Max(x0, x1);

        int yStart = Math.Min(y0, y1);
        int yEnd = Math.Max(y0, y1);

        for (int x = xStart; x <= xEnd; x++)
        {
            for (int y = yStart; y <= yEnd; y++)
            {
                DrawChar(x, y, ch, color);
            }
        }
    }

    public ConsoleColor RgbToConsoleColor(int R, int G, int B)
    {
        int index = (R > 128 | G > 128 | B > 128) ? 8 : 0;
        index |= (R > 64) ? 4 : 0;
        index |= (G > 64) ? 2 : 0;
        index |= (B > 64) ? 1 : 0;
        return (System.ConsoleColor)index;
    }
}
