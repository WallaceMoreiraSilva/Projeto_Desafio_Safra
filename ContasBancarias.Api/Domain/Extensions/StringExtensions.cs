using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ContasBancarias.Domain.Extensions
{
    public static class StringExtensions
    {
        private const string XmlnsPattern = @"xmlns(:\w+)?=""([^""]+)""|xsi(:\w+)?=""([^""]+)""";

        public static string RemovexmlDefinition(this string xml)
        {
            if (string.IsNullOrEmpty(xml)) return xml;

            XDocument doc = XDocument.Parse(xml);
            doc.Declaration = null;
            return doc.ToString();
        }

        public static string RemoveXmlNamespace(this string xml)
        {
            if (string.IsNullOrEmpty(xml)) return xml;
            return Regex.Replace(xml, XmlnsPattern, string.Empty);
        }

        public static string RemoveXmlDefinitionAndNameSpaces(this string xml)
        {
            var result = xml.RemovexmlDefinition();
            result = result.RemoveXmlNamespace();
            return result;
        }

        public static string AplicarFormatacaoEmNumero(this string content, string pattern)
        {
            if (ulong.TryParse(content, out var numero))
                return numero.ToString(pattern);

            return content;
        }

        public static string AplicarMascara(this string content, int startingIndex, int endingIndex, char mask)
        {
            if (string.IsNullOrEmpty(content)) return content;

            int len = content.Length;

            if (startingIndex > len || endingIndex > len
                || startingIndex < 0 || endingIndex < 0) return content;

            int first, last;

            if (startingIndex > endingIndex)
            {
                first = endingIndex;
                last = startingIndex;
            }
            else
            {
                first = startingIndex;
                last = endingIndex;
            }

            var st1 = content[..first];
            var st2 = new string(mask, last - first + 1);
            var st3 = content.Substring(last + 1, len - last - 1);

            return $"{st1}{st2}{st3}";
        }

        public static string ReplacePatterns(this string content, Dictionary<string, string> patternValueDictionary)
        {
            var result = content;

            if (patternValueDictionary == default) return result;

            foreach (var item in patternValueDictionary)
                result = result.Replace(item.Key, item.Value);

            return result;
        }
    }
}
