using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Yummy.Common.Configuration.ImageHandler
{
    public class ImageHandlerSetting : ConfigurationSection
    {
        const string _source = "SourceDirectory", _default = "Default";
        private static ImageHandlerSetting settings = ConfigurationManager.GetSection("ImageHandlerSettings") as ImageHandlerSetting;

        public static ImageHandlerSetting Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty(_source, IsRequired = true)]
        public string SourceDirectory {
            get
            {
                return (string)base[_source];
            }
        }

        [ConfigurationProperty(_default, IsRequired = true)]
        public string Default { 
            get { 
                return (string)base[_default]; 
            } 
        }

        [ConfigurationProperty("ImageSetting", IsRequired = true)]
        public ImageSettingCollection ImageSettings
        {
            get
            {
                return base["ImageSetting"] as ImageSettingCollection;
            }
        }
    }
}
