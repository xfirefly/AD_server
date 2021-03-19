using System;
using System.Drawing;

public class LocationCalc
{
    public static int SCREEN_W = 1280;   //目标平台分辨率
    public static int SCREEN_H = 720;

    public static int CANVAS_W = 999;//920;    //画布宽和高
    public static int CANVAS_H = 999;//499;     

    public static void setScreen(int w, int h)
    {
        SCREEN_W = w;
        SCREEN_H = h;
    }

    public static int ToScreenX(float i, float canvasWidth)
    {
        if (i < 3)
        {
            i = 0;  //吸附边缘
        }

        int n = (int)(SCREEN_W * i / canvasWidth);
        Console.WriteLine("ToScreenX " + i + " " + n);

        if (n > SCREEN_W)
        {
            n = SCREEN_W;
        }
        return n;
    }

    public static float ToFormX(float i, float canvasWidth)
    {
        return (canvasWidth * i / SCREEN_W);
    }

    public static int ToScreenY(float i, float canvasHeight)
    {
        if (i < 3)
        {
            i = 0;  //吸附边缘
        }

        int n = (int)(SCREEN_H * i / canvasHeight);
        Console.WriteLine("ToScreenY " + i + " " + n);

        if (n > SCREEN_H)
        {
            n = SCREEN_H;
        }
        return n;
    }

    public static float ToFormY(float i, float canvasHeight)
    {
        return (canvasHeight * i / SCREEN_H);
    }

    public static void setCanvas(Size size)
    {
        CANVAS_W = size.Width;
        CANVAS_H = size.Height;
    }
}