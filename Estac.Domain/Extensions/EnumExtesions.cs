using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Estac.Domain.Extensions
{
    public static class EnumExtesions
    {

        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static TEnum ObterEnumPorDescricao<TEnum>(this string descricao) where TEnum : struct, Enum
        {
            foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();

                if (attribute != null && attribute.Description.Equals(descricao, StringComparison.OrdinalIgnoreCase))
                {
                    return (TEnum)field.GetValue(null);
                }
            }

            throw new ArgumentException($"Nenhum valor do enum {typeof(TEnum).Name} possui a descrição '{descricao}'.");
        }
    }
}
