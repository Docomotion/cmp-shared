using Docomotion.Shared.NumberUtils.Enums;

namespace Docomotion.Shared.NumberUtils.Localization
{
    internal class LocalizationFactory
    {
        public static ILocalization Crete(Language language){
            switch (language)
            {
                case Language.HEBREW:
                    return new HebrewLocalization();
                default:
                    return new DefaultLocalization();
            }
        }
    }
}
