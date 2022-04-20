using Docomotion.Shared.NumberUtils.Humanizer.Localisation.NumberToWords;

namespace Docomotion.Shared.NumberUtils.Humanizer.Configuration
{
    internal class NumberToWordsConverterRegistry : LocaliserRegistry<INumberToWordsConverter>
    {
        public NumberToWordsConverterRegistry()
            : base((culture) => new EnglishNumberToWordsConverter())
        {
            Register("en", new EnglishNumberToWordsConverter());
            Register("he", (culture) => new HebrewNumberToWordsConverter(culture));
        }
    }
}
