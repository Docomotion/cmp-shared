using System.Globalization;
using Docomotion.Shared.NumberUtils.Enums;

namespace Docomotion.Shared.NumberUtils.Localization
{
    public class DefaultLocalization : ILocalization
    {
        /// <inheritdoc cref="ILocalization.GetUnitSingleName(Currency)"/>
        public string GetUnitSingleName(Currency currency)
        {
            switch (currency)
            {
                case Currency.EURO:
                    return "Euro";
                case Currency.NIS:
                    return "NIS";
                case Currency.USD:
                    return "Dollar";
                default:
                    return "Dollar";
            }
        }

        /// <inheritdoc cref="ILocalization.GetUnitPluralName(Currency)"/>
        public string GetUnitPluralName(Currency currency)
        {
            switch (currency)
            {
                case Currency.EURO:
                    return "Euro";
                case Currency.NIS:
                    return "NIS";
                case Currency.USD:
                    return "Dollars";
                default:
                    return "Dollars";
            }
        }

        /// <inheritdoc cref="ILocalization.GetPieceSingleName(Currency)"/>
        public string GetPieceSingleName(Currency currency)
        {
            switch (currency)
            {
                case Currency.EURO:
                case Currency.USD:
                    return "Cent";
                case Currency.NIS:
                    return "Agora";
                default:
                    return "Cent";
            }
        }

        /// <inheritdoc cref="ILocalization.GetPiecePluralName(Currency)"/>
        public string GetPiecePluralName(Currency currency)
        {
            switch (currency)
            {
                case Currency.EURO:
                case Currency.USD:
                    return "Cents";
                case Currency.NIS:
                    return "Agorot";
                default:
                    return "Cents";
            }
        }

        /// <inheritdoc cref="ILocalization.GetSeparator()"/>
        public string GetSeparator()
        {
            return "and";
        }

        /// <inheritdoc cref="ILocalization.GetCultureInfo()"/>
        public CultureInfo GetCultureInfo()
        {
            return new CultureInfo("en");
        }
    }
}
