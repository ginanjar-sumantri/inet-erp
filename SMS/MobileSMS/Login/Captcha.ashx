<%@ WebHandler Language="C#" Class="Captcha" %>

using System;

using System.Web;

using System.Drawing;

using System.IO;

using System.Web.SessionState;



public class Captcha : IHttpHandler, IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context) {

        Bitmap bmpOut = new Bitmap(80, 30);

        Graphics g = Graphics.FromImage(bmpOut);

        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.FillRectangle(Brushes.Blue, 0, 0, 80, 30);

        Random ran = new Random();
        Brush[] _randomColor = new Brush[20] ;
        _randomColor[0] = Brushes.Aqua ;
        _randomColor[1] = Brushes.Brown ;
        _randomColor[2] = Brushes.CadetBlue ;
        _randomColor[3] = Brushes.Chartreuse ;
        _randomColor[4] = Brushes.DarkGoldenrod ;
        _randomColor[5] = Brushes.DarkMagenta ;
        _randomColor[6] = Brushes.DarkTurquoise  ;
        _randomColor[7] = Brushes.DimGray ;
        _randomColor[8] = Brushes.Gold  ;
        _randomColor[9] = Brushes.CornflowerBlue ;
        _randomColor[10] = Brushes.DarkCyan ;
        _randomColor[11] = Brushes.DarkKhaki ;
        _randomColor[12] = Brushes.MediumPurple  ;
        _randomColor[13] = Brushes.Indigo ;
        _randomColor[14] = Brushes.GreenYellow ;
        _randomColor[15] = Brushes.MediumVioletRed  ;
        _randomColor[16] = Brushes.OliveDrab ;
        _randomColor[17] = Brushes.IndianRed ;
        _randomColor[18] = Brushes.SlateBlue  ;
        _randomColor[19] = Brushes.Tomato ;
        for (int i = 0; i < 88; i++)
            g.FillRectangle(_randomColor[ran.Next(0, 19)], ran.Next(0, 40), ran.Next(0, 15), ran.Next(0, 40), ran.Next(0, 15));
        for (int i = 0; i < 8; i++)
            g.FillRectangle(Brushes.White, ran.Next(0, 20), ran.Next(0, 25), ran.Next(0, 50), ran.Next(0, 3));

        g.DrawString(context.Session["Captcha"].ToString(), new Font("Verdana", 18), new SolidBrush(Color.White), 0, 0);

        MemoryStream ms = new MemoryStream();

        bmpOut.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

        byte[] bmpBytes = ms.GetBuffer();

        bmpOut.Dispose();

        ms.Close();

        context.Response.BinaryWrite(bmpBytes);

        context.Response.End();

    }



    public bool IsReusable
    {

        get
        {

            return false;

        }

    }

 

}