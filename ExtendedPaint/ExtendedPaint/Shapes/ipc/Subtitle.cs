using Gdu.ExtendedPaint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;



public class Subtitle : ItemBase
{
    public string fontname;
    public int fontsize;
    public bool bold;
    public bool italic;
    public bool underline;
    public bool transparent;    //背景完全透明 优先级比 backcolor 高
    public int opacity; // 背景不透明度, 0 - 100,  0 完全透明
    public Direction direction;    // 012 三个值, 取值2时,文字居中显示
    public int speed;
    public string text;
    public string fontcolor;    //  #AARRGGBB
    public string backcolor;

    public Subtitle()
    {
        type = "subtitle";
        fontname = "simsun.ttf";
        fontsize = 30;
        bold = false;
        italic = false;
        underline = false;
        transparent = false;    //优先级比 backcolor 高
        opacity = 100;
        direction = Direction.ToLeft;    // r2l  l2r  static; 012 三个值; 取值2时;文字居中显示
        speed = 7;
        text = "请在 字幕内容 区输入字幕( 双击放大)";
        fontcolor = "#FFFFFFFF";
        backcolor = "#FF3CA7C7";
    }


}

