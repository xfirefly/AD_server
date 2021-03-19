using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ItemBase
{
    //保存按 1920*1080 计算的屏幕坐标
    public int x;
    public int y;
    public int w;
    public int h;

    // form 上面的坐标位置. 左上角
    public float fx;    
    public float fy;
    // form 上面的坐标位置. 右下角
    public float fx1;
    public float fy1;

    public string type;

    public override string ToString()
    {
        return "type: " + type + " " + x + " " + y + " " + w + " " + h;
    }
}