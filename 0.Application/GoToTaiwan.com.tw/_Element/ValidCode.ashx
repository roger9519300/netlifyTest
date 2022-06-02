<%@ WebHandler Language="C#" Class="RandomNumberImage" %>

using System;
using System.Web;
using System.Drawing;
using System.Web.SessionState;
using LeftHand.MemberShip2;

public class RandomNumberImage : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        Bitmap ValidCode = LoginManager.GetCurrentValidCodeBitmap();

        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        ValidCode.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        context.Response.ClearContent();
        context.Response.ContentType = "image/Gif";
        context.Response.BinaryWrite(ms.ToArray());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}