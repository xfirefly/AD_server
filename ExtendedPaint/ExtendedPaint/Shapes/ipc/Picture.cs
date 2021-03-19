using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;



public class Picture : ItemBase, ICloneable
{
    public int intval = -1;
    public List<string> filelist = new List<string>(); 
    public Picture()
    {
        type = "picture";
    }

    public object Clone()
    {
        return this.MemberwiseClone(); //浅复制
    }
}