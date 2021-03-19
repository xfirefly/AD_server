using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
 
public static class Cmd
{
    // socket port
    public const int broadcast_port = 41000;
    public const int cmd_port = 41001;
    public const int file_port = 41002;

    /// <summary>
    /// create buffer send to network, 4 bytes head + message body, head is message length
    /// </summary>
    public static byte[] create(Command cmd, string msg)
    {
        byte[] b = Encoding.UTF8.GetBytes("/" + cmd.ToString() + "/:" + msg + "\n");
        // Send Length 
        //byte[] front = BitConverter.GetBytes(back.Length);  // 4 bytes
        //byte[] combined = front.Concat(back).ToArray();

        Console.WriteLine("###createBuffer => " + cmd.ToString() + " " + msg);
        return b;
    }

    public static byte[] createBuffer2(string cmd, string msg)
    {
        byte[] back = Encoding.ASCII.GetBytes("/" + cmd + "/:" + msg + "\n");
        // Send Length 
        byte[] front = BitConverter.GetBytes(back.Length);  // 4 bytes
        byte[] combined = front.Concat(back).ToArray();

        Console.WriteLine("###createBuffer => " + cmd + " " + msg);
        return combined;
    }
}

