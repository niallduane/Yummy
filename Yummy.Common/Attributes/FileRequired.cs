using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Yummy.Common.Attributes
{
    public class FileRequired : ValidationAttribute
    {
        public string[] ContentType { get; set; }
        public string[] Extension { get; set; }
        public long MaxSize { get; set; }
        public string FieldName { get; private set; }

        public FileRequired(string FieldName)
            : base()
        {
            this.FieldName = FieldName;

        }
        public FileRequired()
            : base()
        {
            this.FieldName = base.ErrorMessageResourceName;

        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (typeof(HttpPostedFileBase).IsInstanceOfType(value))
            {
                HttpPostedFileBase file = (HttpPostedFileBase)value;
                if (MaxSize != 0)
                {
                    if (file.ContentLength > MaxSize)
                    {
                        ErrorMessage = string.Format("{0} size is too big, max size: {1}", FieldName,FormatFileSize(MaxSize));
                        return false;
                    }
                }
                if (ContentType != null)
                {
                    if (!ContentType.Contains(file.ContentType))
                    {
                        string accepted = "";
                        foreach (string s in ContentType) accepted += s + ",";
                        accepted = accepted.Remove(accepted.Length - 1);
                        ErrorMessage = string.Format("{0} format is wrong, Accepted formats are : {1}", FieldName, accepted);
                        return false;
                    }
                }
                if (Extension != null)
                {
                    if (!Extension.Contains(Path.GetExtension(file.FileName)))
                    {
                        string accepted = "";
                        foreach (string s in Extension) accepted += s + ",";
                        accepted = accepted.Remove(accepted.Length - 1);
                        ErrorMessage = string.Format("{0} extension is wrong, Accepted formats are : {1}", FieldName, accepted);
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private string FormatFileSize(long bytes)
        {
            const long kilobyte = 1024;
            const long megabyte = 1024 * kilobyte;
            const long gigabyte = 1024 * megabyte;
            const long terabyte = 1024 * gigabyte;

            if (bytes > terabyte)
            {
                return ((float)bytes / (float)terabyte).ToString("0.00 TB");
            }
            else if (bytes > gigabyte)
            {
                return ((float)bytes / (float)gigabyte).ToString("0.00 GB");
            }
            else if (bytes > megabyte)
            {
                return (((float)bytes / (float)megabyte)).ToString("0.00 MB");
            }
            else if (bytes > kilobyte)
            {
                return ((float)bytes / (float)kilobyte).ToString("0.00 KB");
            }
            else return bytes + " Bytes";
        }
    }
}
