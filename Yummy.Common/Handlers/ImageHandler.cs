using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;

namespace Yummy.Common.Handlers
{
    public class ImageHandler : IHttpHandler
    {
        string getSourceFile(HttpContext context)
        {
            return context.Request.QueryString["s"];
        }

        string getOutputDirectory(HttpContext context)
        {
            return context.Request.QueryString["o"];
        }
        
        string getFileName(HttpContext context)
        {
            string name = context.Request.QueryString["f"];
            return Regex.Replace(name, @"\.{2,}", @".");
        }

        string GetPreset(HttpContext context)
        {
            return context.Request.QueryString["p"];
        }

        private Bitmap Error()
        {
            Bitmap img = new Bitmap(1, 1);
            img.SetPixel(0, 0, Color.FromArgb(100, 100, 100));
            return img;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpg";

            //if (File.Exists(systemFile))
            //{
            //    var imageSettings = new ResizeSettings(settings.Presets[preset].Value + "&format=jpg");
            //    ImageBuilder.Current.Build(sourceDir + fileName, file, imageSettings);
            //    context.Response.WriteFile(file);
            //}
            //else
            //{
            //    context.Response.StatusCode = 500;
            //    Error().Save(context.Response.OutputStream, ImageFormat.Jpeg);
            //}
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
