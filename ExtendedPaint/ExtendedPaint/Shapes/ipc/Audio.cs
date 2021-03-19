using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class Audio : ItemBase
{
    public int intval = -1;
    public List<string> filelist = new List<string>();

    public Audio()
    {
        type = "audio";
    }
}

