using System.ComponentModel;
using System.Reflection;


namespace Museum.Application.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute? attribute = null;

            if (fieldInfo != null )
            {
                attribute = Attribute.GetCustomAttribute(
                        fieldInfo, typeof(DescriptionAttribute)
                    ) as DescriptionAttribute;
            }
            if ( attribute != null )  return attribute.Description;
            return value.ToString();
        }
    }
}
