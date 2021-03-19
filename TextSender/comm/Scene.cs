using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

 

public class Scene
{
    public string timing; // 03:44
    public string name;
    public List<AdItemType> layers = new List<AdItemType>();    //表示 Video, Subtitle, Picture 的层次

    public List<Video> video = new List<Video> ();
    public List<Subtitle> subtitle = new List<Subtitle>() ;
    public List<Picture> picture = new List<Picture> ();

}
 