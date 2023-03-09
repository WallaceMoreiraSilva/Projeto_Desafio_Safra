using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ContasBancarias.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static bool TryParseByDisplayNameOrDefault<TEnum>(string value, out TEnum result) where TEnum : struct
        {
            var byDisplayName = typeof(TEnum)
                .GetMembers()
                .FirstOrDefault(x => x.GetCustomAttribute<DisplayAttribute>()?.Name == value)?.Name;

            return string.IsNullOrEmpty(byDisplayName) ? Enum.TryParse(value, out result) :
                Enum.TryParse(byDisplayName, out result);
        }

        public static string GetDisplayValueOrDefault(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var value = enumValue.ToString();

            return GetDisplayName(enumType, value) ?? value;
        }

        private static string GetDisplayName(Type enumType, string enumValue)
            => enumType.GetMember(enumValue)
                .FirstOrDefault()
                ?.GetCustomAttribute<DisplayAttribute>()
                ?.GetName();
    }
}

