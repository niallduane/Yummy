using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Drawing;

namespace Yummy.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ImageRequired : FileRequired
    {
        public ImageRequired(string FieldName)
            : base(FieldName )
        {
        }

        public ImageRequired()
            : base()
        {
        }

        private int _width = 0;
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
            }
        }

        private int _height = 0;
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
            }
        }

        public override bool IsValid(object value)
        {
            if (base.IsValid(value) == false)
            {
                return false;
            }

            if (_width <= 0)
            {
                ErrorMessage = string.Format("{0}'s width must be set to a positive value", FieldName);
                return false;
            }

            if (_height <= 0)
            {
                ErrorMessage = string.Format("{0}'s height must be set to a positive value", FieldName);
                return false;
            }

            System.Web.HttpPostedFileBase file = (System.Web.HttpPostedFileBase)value;
            if (file != null)
            {
                try
                {
                    using (Image image = Image.FromStream(file.InputStream))
                    {
                        if (image.Width > Width || Height < image.Height)
                        {
                            ErrorMessage = string.Format("Maximum image size is {0}x{1} pixel", Width, Height);
                            return false;
                        }
                    }
                }
                catch
                {
                    ErrorMessage = string.Format("Please provide an image");
                    return false;
                }
            }
            return true;
        }
    }
}
