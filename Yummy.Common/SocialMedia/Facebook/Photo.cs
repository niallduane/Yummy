using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yummy.Common.SocialMedia.Facebook
{
    public class Photo
    {
        public string Id;
        public string Name;
        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Name))
                {
                    return (this.Name.Count() > 100) ? this.Name.Substring(0, 100) : this.Name;
                }
                return "";
            }
        }

        public string Thumbnail;
        public string Source;
    }
}
