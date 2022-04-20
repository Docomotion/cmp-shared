using System.Globalization;
using Docomotion.Shared.NumberUtils.Enums;

namespace Docomotion.Shared.NumberUtils.Localization
{
    public class HebrewLocalization : ILocalization
    {
        /// <inheritdoc cref="ILocalization.GetUnitSingleName(Currency)"/>
        public string GetUnitSingleName(Currency currency)
        {
            switch (currency)
            {
                case Currency.EURO:
                    return "אירו";
                case Currency.NIS:
                    return "ש\"ח";
                case Currency.USD:
                    return "דולר";
                default:
                    return string.Empty;
            }
        }
        
        /// <inheritdoc cref="ILocalization.GetUnitPluralName(Currency)"/>
        public string GetUnitPluralName(Currency currency)
        {
            switch (currency)
            {
                case Currency.EURO:
                    return "אירו";
                case Currency.NIS:
                    return "ש\"ח";
                case Currency.USD:
                    return "דולרים";
                default:
                    return string.Empty;
            }
        }
        
        /// <inheritdoc cref="ILocalization.GetPieceSingleName(Currency)"/>
        public string GetPieceSingleName(Currency currency)
        {
            switch (currency)
            {

                case Currency.EURO:
                case Currency.USD:
                    return "סנט";
                case Currency.NIS:
                    return "אג'";
                default:
                    return string.Empty;
            }
        }
        
        /// <inheritdoc cref="ILocalization.GetPiecePluralName(Currency)"/>
        public string GetPiecePluralName(Currency currency)
        {
            switch (currency)
            {
                case Currency.EURO:
                case Currency.USD:
                    return "סנטים";
                case Currency.NIS:
                    return "אג'";
                default:
                    return string.Empty;
            }
        }

        
        /// <inheritdoc cref="ILocalization.GetSeparator()"/>
        public string GetSeparator()
        {
            return "+";
        }
        
        /// <inheritdoc cref="ILocalization.GetCultureInfo()"/>
        public CultureInfo GetCultureInfo()
        {
            return CultureInfo.CreateSpecificCulture("he-IL");
        }
    }
}
