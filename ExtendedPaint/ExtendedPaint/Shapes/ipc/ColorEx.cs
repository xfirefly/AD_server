using System;
using System.Drawing;

public class ColorEx
{

    public static string ColorToHex(Color c)
    {
        string hex = "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        Console.WriteLine(c.ToString() + " => " + hex);
        return hex;
    }

    //  hex format : #AARRGGBB
    public static Color FromHtml(string hex)
    {
        //If the original colour was a custom colour (not a known colour) then
        //the stored colour will contain a HTML-string, like "ff12aadd".

        //Ignore the first two characters (the alpha) and hope
        //that all custom colours uses "ff" as alpha.
        Color c = ColorTranslator.FromHtml("#" + hex.Substring(3));
        //Color colour = ColorTranslator.FromHtml("#E7EFF2");
        Color colorAlpha = Color.FromArgb(Convert.ToInt32("0x" + hex.Substring(1, 2), 16), c);
        Console.WriteLine(hex + " => " + colorAlpha.ToString());

        return colorAlpha;
    }


}