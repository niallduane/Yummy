using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Yummy.Common.Configuration.ImageHandler
{
    [ConfigurationCollection(typeof(ImageSetting), AddItemName = "add", RemoveItemName = "remove")]
    public class ImageSettingCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ImageSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ImageSetting)element).Name;
        }

        public ImageSetting this[int index]
        {
            get { return (ImageSetting)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public ImageSetting GetSetting(string name)
        {
            foreach (ImageSetting type in this)
            {
                if (type.Name.ToLower() == name.ToLower()) return type;
            }
            throw new Exception(string.Format("Please add the ImageSetting for {0}", name));
        }
    }
}
