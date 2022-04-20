using System.Globalization;

namespace Docomotion.Shared.NumberUtils.Humanizer.Transformer
{
    internal class ToSentenceCase : ICulturedStringTransformer
    {
        public string Transform(string input)
        {
            return Transform(input, null);
        }

        public string Transform(string input, CultureInfo culture)
        {
            culture = culture ?? CultureInfo.CurrentCulture;

            if (input.Length >= 1)
            {
                return culture.TextInfo.ToUpper(input[0]) + input.Substring(1);
            }

            return culture.TextInfo.ToUpper(input);
        }
    }
}