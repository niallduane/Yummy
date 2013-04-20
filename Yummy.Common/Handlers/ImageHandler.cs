using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using Yummy.Common.Configuration.ImageHandler;
using System.Drawing.Imaging;
using ImageResizer;

namespace Yummy.Common.Handlers
{
    /// <summary>
    /// Image Handler gives a website the ability for the user to upload an Image file. 
    /// Keep the Original file and create different image sizes
    /// based on the original file
    /// </summary>
    /// <remarks>
    /// <ImageHandlerSettings OutputDirectory="~/Uploads/" SourceDirectory="~/Uploads/" Default="Thumbnail">
    ///    <Preset Name="Thumbnail" MaxHeight="200" MaxWidth="200" Quality="90">
    /// </ImageHandlerSettings>
    /// </remarks>
    public class ImageHandler : IHttpHandler
    {
        private ImageHandlerSetting HandlerSettings = ImageHandlerSetting.Settings;

        private string GetOriginalFilePath(HttpContext context)
        {
            return HandlerSettings.SourceDirectory + GetFileName(context);
        }

        private string GetFileName(HttpContext context)
        {
            string file = context.Request["f"];
            if (string.IsNullOrEmpty(file)) throw new Exception("The Filename was not sent in the Request.");

            return Regex.Replace(file, @"\.{2,}", @".");
        }

        private ImageSetting GetImageSettings(HttpContext context)
        {
            string setting = (!String.IsNullOrEmpty(context.Request["s"])) ? context.Request["s"] :  HandlerSettings.Default;
            return HandlerSettings.ImageSettings.GetSetting(setting);
        }

        private string GetNewFilePath(HttpContext context)
        {
            return string.Format("{0}/{1}", HandlerSettings.SourceDirectory, GetImageSettings(context).Name);
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
            string orginalFile = context.Server.MapPath(GetOriginalFilePath(context));
            if (File.Exists(orginalFile))
            {
                string directory = context.Server.MapPath(GetNewFilePath(context));
                string newFile = string.Format("{0}/{1}", context.Server.MapPath(GetNewFilePath(context)), GetFileName(context));
                if (File.Exists(newFile))
                {
                    context.Response.WriteFile(newFile);
                    return;
                }
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                
                ImageBuilder.Current.Build(orginalFile, newFile, new ResizeSettings(GetImageSettings(context).ToString() + "&amp;format=jpg"));
                context.Response.WriteFile(newFile);
                return;
                
            }
            context.Response.StatusCode = 500;
            Error().Save(context.Response.OutputStream, ImageFormat.Jpeg);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
