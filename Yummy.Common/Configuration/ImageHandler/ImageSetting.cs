using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Yummy.Common.Configuration.ImageHandler
{
    public class ImageSetting : ConfigurationElement
    {
        const string _name = "Name", _maxHeight = "MaxHeight", _maxWidth = "MaxWidth", _quality = "Quality";

        [ConfigurationProperty(_name, DefaultValue = "Thumbnail", IsRequired = true, IsKey = true)]
        public string Name { 
            get
            {
                return (string)base[_name];
            }
        }

        [ConfigurationProperty(_maxHeight, DefaultValue = 150)]
        public int MaxHeight
        {
            get
            {
                return (int)base[_maxHeight];
            }
        }

        [ConfigurationProperty(_maxWidth, DefaultValue = 150)]
        public int MaxWidth
        {
            get
            {
                return (int)base[_maxWidth];
            }
        }

        [ConfigurationProperty(_quality, DefaultValue = 90)]
        public int Quality
        {
            get
            {
                return (int)base[_quality];
            }
        }

        public string ToString()
        {
            return string.Format("maxheight={0}&amp;maxwidth={1}&amp;quality={2}", MaxHeight.ToString(), MaxWidth.ToString(), Quality.ToString());
        }
    }
}
