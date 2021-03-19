using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class FontEx
{
     
    private static Dictionary<string, FontFamily> fontDict;  //  ttf 文件名称 => FontFamily
    private static System.Drawing.Text.PrivateFontCollection[] privateFonts;
    private static Dictionary<string, string> ttfDict;

    static FontEx()
    {
        fontDict = new Dictionary<string, FontFamily>();


        //这些字体win 自带
        try
        {
            fontDict["simsun.ttf"] = new FontFamily("宋体");
            fontDict["simkai.ttf"] = new FontFamily("楷体");
            fontDict["simhei.ttf"] = new FontFamily("黑体");
        }
        catch (Exception)
        {
            fontDict["simkai.ttf"] = new FontFamily("宋体");
            fontDict["simhei.ttf"] = new FontFamily("宋体");
        }

        // 这些字体 win不自带
        string[] fn = { "msyh.ttf", "pingfang.ttf" }; // "simkai.ttf", "simsun.ttf", "simhei.ttf", 
        privateFonts = new System.Drawing.Text.PrivateFontCollection[fn.Length];
        for (int i = 0; i < fn.Length; i++)
        {
            string ttf = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fn[i]);
            privateFonts[i] = new System.Drawing.Text.PrivateFontCollection();
            privateFonts[i].AddFontFile(ttf);

            fontDict[fn[i]] = privateFonts[i].Families[0];
        }

        //字体名称 => ttf 文件名称
        ttfDict = new Dictionary<string, string>();
        ttfDict.Add("楷体", "simkai.ttf");
        ttfDict.Add("雅黑", "msyh.ttf");
        ttfDict.Add("宋体", "simsun.ttf");
        ttfDict.Add("黑体", "simhei.ttf");
        ttfDict.Add("苹方", "pingfang.ttf");
    }


    public static string getTtfName(string fontname)
    {
        return ttfDict[fontname];
    }

    public static FontFamily getFontFamily(string ttfname)
    {
        return fontDict[ttfname];
    }

}
