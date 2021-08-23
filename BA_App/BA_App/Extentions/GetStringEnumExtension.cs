using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BA_App.Extentions
{
    public static class GetStringEnumExtension
    {
        public static string GetEnumDescription(this System.Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
