using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using TextSender;

public class FFMPEG
{
    private static Process ffmpeg;

    public static void exec(string input, string output, string parametri)
    {
        ffmpeg = new Process();

        ffmpeg.StartInfo.Arguments = " -i \"" + input + "\" -y -ss 00:00:02 -vframes 1 -an " + parametri + output;
        ffmpeg.StartInfo.FileName = "ffmpeg.exe";
        ffmpeg.StartInfo.UseShellExecute = false;
        ffmpeg.StartInfo.RedirectStandardOutput = true;
        ffmpeg.StartInfo.RedirectStandardError = true;
        ffmpeg.StartInfo.CreateNoWindow = true;

        Console.WriteLine(ffmpeg.StartInfo.Arguments);
        ffmpeg.Start();
        ffmpeg.WaitForExit();
        ffmpeg.Close();
    }

    public static void GetThumbnail(string video, string jpg, string velicina)
    {
        if (velicina == null)
            velicina = "640*480 ";

        exec(video, jpg, "-s " + velicina);
    }

    public static string GetThumbnail(string video)
    {
        Directory.CreateDirectory(App.ScrshotDir);

        string imgPath;

        //重新打开场景, 视频缩略图已经生成了
        if (video.Contains(frmMain.VideoPrefix))    
        {
            Regex reg = new Regex(@"advid_(.*)\.");
            var mat = reg.Match(video);
            Console.WriteLine(mat.Groups[1]);

            imgPath = Path.Combine(App.ScrshotDir, mat.Groups[1] + ".png");

        }
        else
        {
            imgPath = Path.Combine(App.ScrshotDir, Util.md5(video) + ".png");
        }

        if (!File.Exists(imgPath))
        {
            exec(video, imgPath, "-s 960*540 ");
        }

        if( !File.Exists(imgPath))
        {
            imgPath = Path.Combine(App.Dir, "screen_def.png");
        }

        return imgPath;
    }
}